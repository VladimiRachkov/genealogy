using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Genealogy;
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
    private readonly int delay = 60000;
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
        stoppingToken.Register(() => {
            _logger.LogDebug($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}] PurchaseManageService background task is stopping.");
            updateLastLog("PurchaseManageService background task is stopping.");
        });

        try {
            while (!stoppingToken.IsCancellationRequested)
            {   updateLastLog("PurchaseManageService task doing background work.");
                try {
                    _logger.LogDebug($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}] PurchaseManageService task doing background work.");

                    BusinessObjectFilter filter = new BusinessObjectFilter()
                    {
                        MetatypeId = MetatypeData.Purchase.Id,
                        IsRemoved = false
                    };
                    var purchases = _service.GetBusinessObjects(filter).ToList();
                    foreach (var purchase in purchases)
                    {

                        _logger.LogDebug($"{purchase.Id} {purchase.Title}");

                        var purchaseProps = JsonConvert.DeserializeObject<CustomProps.Purchase>(purchase.Data);

                        if (purchaseProps.status == PurchaseStatus.Succeeded) {
                            continue;
                        } 

                        if(Guid.Parse(purchaseProps.paymentId) == Guid.Empty || DateTime.Now > purchase.StartDate.AddHours(1)) {
                            _service.RemoveBusinessObject(purchase.Id);
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
                                    await removePurchase(purchase, $"Timeout {timeout} minutes.");
                                }
                                break;
                        }

                        switch (response.Status)
                        {
                            case PaymentStatus.Succeeded:
                                purchaseProps.status = PurchaseStatus.Succeeded;
                                purchase.Data = JsonConvert.SerializeObject(purchaseProps);
                                purchase.IsRemoved = true;
                                await updatePurchase(purchase, "Payment successed.");
                                await _service.ProductAction(Guid.Parse(purchaseProps.productId), purchase.UserId);
                                break;

                            case PaymentStatus.Canceled:
                                await removePurchase(purchase, "Canceled.");
                                break;
                        }
                    } 
                    await Task.Delay(delay, stoppingToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.ToString());
                }
            }
        }
        catch (Exception e) when (stoppingToken.IsCancellationRequested)
        {
            _logger.LogError(e.ToString(), "Execution Cancelled");
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }

        _logger.LogDebug($"PurchaseManageService background task is stopping.");
        updateLastLog("PurchaseManageService background task is stopping.");
    }

    private async Task<int> removePurchase(BusinessObject purchase, string reason)
    {
        _logger.LogDebug($"PurchaseManageService removes purchase {purchase.Id}. Reason: {reason}");
        try
        {
            _genealogyContext.BusinessObjects.Remove(purchase);
            return await _genealogyContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"PurchaseManageService removing has error. Reason: {e.ToString()}");
        }
        return 0;
    }
    private async Task<int> updatePurchase(BusinessObject purchase, string reason)
    {
        _logger.LogDebug($"PurchaseManageService updates purchase {purchase.Id}. Reason: {reason}");
        try
        {
            _genealogyContext.BusinessObjects.Update(purchase);
            return await _genealogyContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"PurchaseManageService updating has error. Reason: {e.ToString()}");
        }
        return 0;
    }

    //private async Task<int> productAction(Guid productId, Guid userId)
    //{
        // try {
        //     var product = _service.GetBusinessObjects(new BusinessObjectFilter() { Id = productId }).FirstOrDefault();
        //     var props = JsonConvert.DeserializeObject<CustomProps.Product>(product.Data);
        //     Metatype metatype = null;
        //     String name = "";

        //     if (productId == ProductData.Subscription.Id) {
        //         metatype = _genealogyContext.Metatypes.Where(mt => mt.Id == MetatypeData.Subscription.Id).FirstOrDefault();
        //         name = ProductData.Subscription.Name;
        //     }

        //     if (productId == ProductData.Book.Id) {
        //         metatype = _genealogyContext.Metatypes.Where(mt => mt.Id == MetatypeData.Book.Id).FirstOrDefault();
        //         name = ProductData.Book.Name;
        //     }

        //     if (metatype == null) {
        //         return 0;
        //     }

        //     var bo = new BusinessObject()
        //     {
        //         Id = Guid.NewGuid(),
        //         StartDate = DateTime.Now,
        //         FinishDate = DateTime.Now.AddMonths(1),
        //         UserId = userId,
        //         MetatypeId = metatype.Id,
        //         Metatype = metatype,
        //         IsRemoved = false,
        //         Name = name,
        //         Title = "",
        //         Data = props.message
        //     };

        //     _genealogyContext.BusinessObjects.Add(bo);
        //     await _genealogyContext.SaveChangesAsync();

        //     if (!String.IsNullOrEmpty(props.message))
        //     {
        //         var user = _service.GetUserById(userId);
        //         await _service.SendEmailToUser(product.Title, user.Email, props.message);
        //     }
        // }

        // catch (Exception e)
        // {
        //     _logger.LogError($"PurchaseManageService has error. Reason: {e.ToString()}");
        // }
        
        // return 0;
    //}

    private void updateLastLog(String message) {
        try {
            var lastLog = _service.GetBusinessObjects(new BusinessObjectFilter() { MetatypeId = Logs.LastLog.Id }).FirstOrDefault();

            if (lastLog == null) {
                var lastLogMetatype = _genealogyContext.Metatypes.Where(metatype => metatype.Id == Logs.LastLog.Id).FirstOrDefault();

                if (lastLogMetatype == null) {
                    return;
                }

                lastLog = new BusinessObject() {
                    Id = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    FinishDate = DateTime.Now,
                    UserId = Users.Admin.Id,
                    Metatype = lastLogMetatype,
                    MetatypeId = Logs.LastLog.Id,
                    IsRemoved = false,
                    Name = Logs.LastLog.Name,
                    Title = message
                };

                _genealogyContext.BusinessObjects.Add(lastLog);
                _genealogyContext.SaveChangesAsync();
            } else {
                lastLog.StartDate = DateTime.Now;
                lastLog.FinishDate = DateTime.Now;
                lastLog.Title = message;
                _service.UpdateBusinessObject(lastLog);
            }
        }
        catch (Exception e) {
            _logger.LogError($"updateLastLog. Reason: {e.ToString()}");
        }
    }
}