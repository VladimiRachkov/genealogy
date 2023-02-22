using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genealogy.Data;
using Genealogy.Models;
using Genealogy.Repository.Concrete;
using Genealogy.Service.Astract;
using Newtonsoft.Json;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public async Task<bool> ProductAction(Guid productId, Guid userId)
        {
            var product = GetBusinessObjects(new BusinessObjectFilter() { Id = productId }).FirstOrDefault();
            var bookProps = JsonConvert.DeserializeObject<CustomProps.Product>(product.Data);

            if (!String.IsNullOrEmpty(bookProps.message))
            {
                try {
                    var user = GetUserById(userId);
                    await SendEmailToUser(product.Title, user.Email, bookProps.message);
                    return true;
                }
                catch(ApplicationException e) {
                    Console.WriteLine(e.ToString());
                    //_logger.LogError();
                }
            }

            if (productId == ProductData.Subscribe.Id)
            {
                var subscribeMetatype = _unitOfWork.MetatypeRepository.GetByID(MetatypeData.Subscribe.Id);
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
                    createBusinessObject(subscribe, userId);
                    return true;
                }
                catch (ApplicationException e)
                {
                    Console.WriteLine(e.ToString());
                    //_logger.LogError($"PurchaseManageService has error. Reason: {e.ToString()}");
                }
            }
            return false;
        }
        public async Task<BusinessObject> ActivatePurchase(Guid purchaseId)
        {
            BusinessObject result = null;

            var purchase = GetBusinessObjects(new BusinessObjectFilter() { Id = purchaseId }).FirstOrDefault();
            var purchaseProps = JsonConvert.DeserializeObject<CustomProps.Purchase>(purchase.Data);
            var productId = Guid.Parse(purchaseProps.productId);

            if ( purchase != null)
            {
                try {
                    await ProductAction(productId, purchase.UserId);
                    // purchaseProps.status = PurchaseStatus.Succeeded;
                    // purchase.Data = JsonConvert.SerializeObject(purchaseProps);
                    // purchase.IsRemoved = true;
                    // result = UpdateBusinessObject(purchase);
                    СonfirmPurchase(purchase.Id);
                }
                catch (ApplicationException e)
                {
                    Console.WriteLine(e.ToString());
                    //_logger.LogError($"PurchaseManageService has error. Reason: {e.ToString()}");
                }
            }

            return result;
        }
    }
}