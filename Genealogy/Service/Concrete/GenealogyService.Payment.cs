using System.Threading.Tasks;
using Genealogy.Models;
using Genealogy.Service.Astract;
using Newtonsoft.Json;
using Yandex.Checkout.V3;
using System.Linq;
using Genealogy.Service.Helpers;
using Genealogy.Data;
using System.Collections.Generic;
using System;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        private Client client = new Yandex.Checkout.V3.Client(shopId: "739084", secretKey: "test_5LLiubI6pAXxCt-13sfn9WymESZgeE9Z30BrZIB3BAQ");
        public async Task<string> DoPayment(PaymentInDto payment)
        {
            NewPayment newPayment = null;
            BusinessObject purchase = null;

            AsyncClient asyncClient = client.MakeAsync();

            try
            {
                var product = _unitOfWork.BusinessObjectRepository.GetByID(payment.ProductId);
                var productProps = JsonConvert.DeserializeObject<CustomProps.Product>(product.Data);

                var userFilter = new UserFilter();
                userFilter.Id = payment.UserId;

                var user = GetUserById(payment.UserId);


                purchase = createPurchase(product, user);

                var metadata = new Dictionary<string, string>();
                metadata.Add("purchaseId", purchase.Id.ToString());

                newPayment = new NewPayment
                {
                    Amount = new Amount { Value = productProps.price, Currency = "RUB" },
                    Description = $"Пользователь {user.LastName} {user.FirstName} ({user.Email}) оплатил {product.Title}",
                    Metadata = metadata,
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

        public BusinessObjectOutDto ConfirmPurchase(Payment payment)
        {
            Guid purchaseId;
            BusinessObjectOutDto result = null;

            if (payment != null)
            {
                string value = "";
                payment.Metadata.TryGetValue("purchaseId", out value);
                purchaseId = Guid.Parse(value);
                result = confirmPurchase(purchaseId);

            }
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

        private BusinessObjectOutDto confirmPurchase(Guid purchaseId)
        {
            var purchase = _unitOfWork.BusinessObjectRepository.GetByID((purchaseId));
            BusinessObjectOutDto result = null;

            if (purchase != null)
            {
                var purchaseProps = JsonConvert.DeserializeObject<CustomProps.Purchase>(purchase.Data);

                purchaseProps.status = PurchaseStatus.Succeeded;

                var updatedBO = new BusinessObjectInDto();
                updatedBO.Id = purchase.Id;
                updatedBO.Data = JsonConvert.SerializeObject(purchaseProps);

                result = UpdateBusinessObject(updatedBO);
            }
            return result;
        }
    }

}