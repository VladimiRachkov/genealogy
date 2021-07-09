using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Genealogy.Models;
using Genealogy.Data;
using System.Linq;
using Newtonsoft.Json;
using Yandex.Checkout.V3;
using System;
using Genealogy.Service.Astract;
using Genealogy.Repository.Abstract;
using Microsoft.Extensions.Configuration;

public class PurchaseManageService : BackgroundService
{
    private readonly ILogger<PurchaseManageService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly GenealogyContext _genealogyContext;
    private readonly IServiceScope _scope;
    private readonly IGenealogyService _service;
    private readonly IConfiguration _configuration;
    private readonly int timeout = 10;

    public PurchaseManageService(ILogger<PurchaseManageService> logger, IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        _logger = logger;
        _serviceScopeFactory = scopeFactory;
        _scope = serviceProvider.CreateScope();
        _genealogyContext = _scope.ServiceProvider.GetRequiredService<GenealogyContext>();
        _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        _service = _scope.ServiceProvider.GetRequiredService<IGenealogyService>();
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.Register(() =>
            _logger.LogDebug($"PurchaseManageService background task is stopping."));

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogDebug($"PurchaseManageService task doing background work.");

                BusinessObjectFilter filter = new BusinessObjectFilter()
                {
                    MetatypeId = MetatypeData.Purchase.Id
                };

                var purchases = _service.GetBusinessObjects(filter).ToList();

                foreach (var purchase in purchases)
                {
                    var purchaseProps = JsonConvert.DeserializeObject<CustomProps.Purchase>(purchase.Data);

                    if (purchaseProps.status == PurchaseStatus.Succeeded)
                    {
                        continue;
                    }

                    var settings = _configuration.GetSection("AppSettings").GetSection("Yookassa");
                    var shopId = settings.GetValue<string>("shopId");
                    var secretKey = settings.GetValue<string>("secretKey");

                    var client = new Yandex.Checkout.V3.Client(shopId, secretKey);
                    var asyncClient = client.MakeAsync();
                    
                    var response = await asyncClient.GetPaymentAsync(purchaseProps.paymentId);

                    switch (purchaseProps.status)
                    {
                        case PurchaseStatus.Pending:
                            var time1 = DateTime.Now;
                            var time2 = purchase.StartDate;
                            var span = time1.Subtract(time2).TotalMinutes;

                            if (DateTime.Now.Subtract(purchase.StartDate).TotalMinutes > timeout)
                            {
                                await removePurchase(purchase, @"Timeout {timeout} minutes.");
                            }
                            break;
                    }

                    switch (response.Status)
                    {
                        case PaymentStatus.Succeeded:
                            purchaseProps.status = PurchaseStatus.Succeeded;
                            purchase.Data = JsonConvert.SerializeObject(purchaseProps);

                            await updatePurchase(purchase, "Payment successed.");
                            await productAction(Guid.Parse(purchaseProps.productId), purchase.UserId);

                            break;

                        case PaymentStatus.Canceled:
                            await removePurchase(purchase, "Canceled.");
                            break;
                    }

                    _logger.LogDebug($"{purchase.Id} {purchase.Title}");
                };

                await Task.Delay(60000, stoppingToken);
            }

            _logger.LogDebug($"PurchaseManageService background task is stopping.");
        }
        catch (ApplicationException e)
        {
            _logger.LogError(e.ToString());
            //throw e;
        }
    }

    private async Task<int> removePurchase(BusinessObject purchase, string reason)
    {
        _logger.LogDebug($"PurchaseManageService removes purchase {purchase.Id}. Reason: {reason}");
        try
        {
            _genealogyContext.BusinessObjects.Remove(purchase);
            return await _genealogyContext.SaveChangesAsync();
        }
        catch (ApplicationException e)
        {
            _logger.LogError($"PurchaseManageService removing has error. Reason: {e.ToString()}");
            throw e;
        }
    }
    private async Task<int> updatePurchase(BusinessObject purchase, string reason)
    {
        _logger.LogDebug($"PurchaseManageService updates purchase {purchase.Id}. Reason: {reason}");
        try
        {
            _genealogyContext.BusinessObjects.Update(purchase);
            return await _genealogyContext.SaveChangesAsync();
        }
        catch (ApplicationException e)
        {
            _logger.LogError($"PurchaseManageService updating has error. Reason: {e.ToString()}");
            throw e;
        }
    }

    private async Task<int> productAction(Guid productId, Guid userId)
    {
        var product = _service.GetBusinessObjects(new BusinessObjectFilter() { Id = productId }).FirstOrDefault();
        var bookProps = JsonConvert.DeserializeObject<CustomProps.Product>(product.Data);

        if (!String.IsNullOrEmpty(bookProps.message))
        {
            var user = _service.GetUserById(userId);
            await _service.SendEmailToUser(product.Title, user.Email, bookProps.message);
        }

        if (productId == ProductData.Subscribe.Id)
        {
            var subscribeMetatype = _genealogyContext.Metatypes.Where(metatype => metatype.Id == MetatypeData.Subscribe.Id).FirstOrDefault();
            var subscribe = new BusinessObject()
            {
                Id = Guid.NewGuid(),
                StartDate = DateTime.Now,
                FinishDate = DateTime.Now.AddMonths(1),
                UserId = userId,
                MetatypeId = ProductData.Subscribe.Id,
                Metatype = subscribeMetatype,
                IsRemoved = false,
                Name = "SUBSCRIBLE",
                Title = "Подписка"
            };

            try
            {
                _genealogyContext.BusinessObjects.Add(subscribe);
                return await _genealogyContext.SaveChangesAsync();
            }
            catch (ApplicationException e)
            {
                _logger.LogError($"PurchaseManageService has error. Reason: {e.ToString()}");
                throw e;
            }
        }
        return 0;
    }
}