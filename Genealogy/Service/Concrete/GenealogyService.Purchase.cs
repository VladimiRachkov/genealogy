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
            try {
                var product = GetBusinessObjects(new BusinessObjectFilter() { Id = productId }).FirstOrDefault();
                var props = JsonConvert.DeserializeObject<CustomProps.Product>(product.Data);
                Metatype metatype = null;
                String name = "";

                if (productId == ProductData.Subscription.Id) {
                    metatype = _unitOfWork.MetatypeRepository.GetByID(MetatypeData.Subscription.Id);
                    name = ProductData.Subscription.Name;
                }

                if (productId == ProductData.Book.Id) {
                    metatype = _unitOfWork.MetatypeRepository.GetByID(MetatypeData.Book.Id);
                    name = ProductData.Book.Name;
                }

                if (metatype == null) {
                    return false;
                }

                var bo = new BusinessObject()
                {
                    Id = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    FinishDate = DateTime.Now.AddMonths(1),
                    UserId = userId,
                    MetatypeId = metatype.Id,
                    Metatype = metatype,
                    IsRemoved = false,
                    Name = name,
                    Title = "",
                    Data = props.message
                };

                _unitOfWork.BusinessObjectRepository.Add(bo);
                _unitOfWork.Save();

                if (!String.IsNullOrEmpty(props.message))
                {
                    var user = GetUserById(userId);
                    await SendEmailToUser(product.Title, user.Email, props.message);
                }
            }

            catch (Exception e)
            { 
                Console.WriteLine(e.ToString());
            }
        
            return true;
        }

        public async Task<BusinessObjectOutDto> ActivatePurchase(Guid purchaseId)
        {
            BusinessObject result = null;

            try {
                var purchase = GetBusinessObjects(new BusinessObjectFilter() { Id = purchaseId }).FirstOrDefault();
                var purchaseProps = JsonConvert.DeserializeObject<CustomProps.Purchase>(purchase.Data);
                var productId = Guid.Parse(purchaseProps.productId);

                if (purchase != null)
                {
                    purchaseProps.status = PurchaseStatus.Succeeded;
                    purchase.Data = JsonConvert.SerializeObject(purchaseProps);
                    purchase.IsRemoved = true;
                    result = UpdateBusinessObject(purchase);
                    await ProductAction(productId, purchase.UserId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _mapper.Map<BusinessObjectOutDto>(result);;
        }
    }
}