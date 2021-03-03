using System.Threading.Tasks;
using Genealogy.Models;
using Genealogy.Service.Astract;
using Newtonsoft.Json;
using Yandex.Checkout.V3;
using System.Linq;
using Genealogy.Service.Helpers;
using Genealogy.Data;
using System;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public async Task<string> DoPayment(PaymentInDto payment)
        {
            NewPayment newPayment = null;
            BusinessObject purchase = null;

            var client = new Yandex.Checkout.V3.Client(shopId: "739084", secretKey: "test_5LLiubI6pAXxCt-13sfn9WymESZgeE9Z30BrZIB3BAQ");
            AsyncClient asyncClient = client.MakeAsync();

            try
            {
                var product = _unitOfWork.BusinessObjectRepository.GetByID(payment.ProductId);
                var productProps = JsonConvert.DeserializeObject<CustomProps.Product>(product.Data);

                var userFilter = new UserFilter();
                userFilter.Id = payment.UserId;

                var user = GetUserById(payment.UserId);


                purchase = createPurchase(product, user);

                newPayment = new NewPayment
                {
                    Amount = new Amount { Value = productProps.price, Currency = "RUB" },
                    Description = $"Пользователь {user.LastName} {user.FirstName} ({user.Email}) оплатил {product.Title}",
                    Confirmation = new Confirmation
                    {
                        Type = ConfirmationType.Redirect,
                        ReturnUrl = $"{payment.ReturnUrl}?purchaseId={purchase.Id}"
                    }
                };

            }
            catch (AppException ex)
            {
                throw ex;
            }
            Payment paymentResult = await asyncClient.CreatePaymentAsync(newPayment);

            if (paymentResult.Status == PaymentStatus.Pending)
            {
                string url = paymentResult.Confirmation.ConfirmationUrl;
                return url;
            }

            return null;
        }

        public BusinessObjectOutDto ConfirmPurchase(PurchaseInDto purchaseDto)
        {
            var purchase = _unitOfWork.BusinessObjectRepository.GetByID((purchaseDto.Id));
            var purchaseProps = JsonConvert.DeserializeObject<CustomProps.Purchase>(purchase.Data);

            purchaseProps.status = PurchaseStatus.Succeeded;

            var updatedBO = new BusinessObjectInDto();
            updatedBO.Id = purchase.Id;
            updatedBO.Data = JsonConvert.SerializeObject(purchaseProps);

            var result = UpdateBusinessObject(updatedBO);
            return result;
        }

        private BusinessObject createPurchase(BusinessObject product, User user)
        {
            var purchase = new BusinessObject();
            var username = $"{user.LastName} {user.FirstName}";

            purchase.Title = product.Title;
            purchase.Name = product.Name;
            purchase.MetatypeId = MetatypeData.Purchase.Id;
            purchase.Data = JsonConvert.SerializeObject(new CustomProps.Purchase(product.Title, username, user.Email));

            var result = createBusinessObject(purchase);
            return result;
        }
    }

}