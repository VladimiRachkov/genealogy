using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Genealogy.Models;
using Genealogy.Data;
using System.Linq;
using Newtonsoft.Json;
using Yandex.Checkout.V3;

public class PurchaseManageService : BackgroundService
{
    private readonly ILogger<PurchaseManageService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Client client = new Yandex.Checkout.V3.Client(shopId: "739084", secretKey: "test_5LLiubI6pAXxCt-13sfn9WymESZgeE9Z30BrZIB3BAQ");

    public PurchaseManageService(ILogger<PurchaseManageService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            AsyncClient asyncClient = client.MakeAsync();

            stoppingToken.Register(() =>
                _logger.LogDebug($" PurchaseManageService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"PurchaseManageService task doing background work.");

                // var context = scope.ServiceProvider.GetRequiredService<GenealogyContext>();
                // var purchases = context.BusinessObjects.Where(bo => bo.MetatypeId == MetatypeData.Purchase.Id);

                // purchases.ToList().ForEach(purchase =>
                // {
                //     var purchaseProps = JsonConvert.DeserializeObject<CustomProps.Purchase>(purchase.Data);

                //     purchaseProps.status = PurchaseStatus.Succeeded;
                //     _logger.LogDebug($"{purchase.Id} {purchase.Title}");
                // });

                await Task.Delay(5000, stoppingToken);
            }

            _logger.LogDebug($"PurchaseManageService background task is stopping.");
        }
    }
}