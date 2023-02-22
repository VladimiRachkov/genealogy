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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public async Task<string> DoPayment(PaymentInDto payment)
        {
            string result = null;
            NewPayment newPayment = null;
            BusinessObject purchase = null;

            try
            {
                var settings = _configuration.GetSection("AppSettings").GetSection("Yookassa");
                var shopId = settings.GetValue<string>("shopId");
                var secretKey = settings.GetValue<string>("secretKey");

                var client = new Yandex.Checkout.V3.Client(shopId, secretKey);
                AsyncClient asyncClient = client.MakeAsync();
                
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
                    Description = $"({user.Email}) оплатил {product.Title}",
                    Metadata = metadata,
                    Capture = true,
                    Confirmation = new Confirmation
                    {
                        Type = ConfirmationType.Redirect,
                        ReturnUrl = $"{payment.ReturnUrl}?purchaseId={purchase.Id}"
                    }
                };

                Payment paymentResult = await asyncClient.CreatePaymentAsync(newPayment);

                if (paymentResult.Status == PaymentStatus.Pending)
                {
                    result = paymentResult.Confirmation.ConfirmationUrl;
                }

                if (purchase != null)
                {
                    var purchaseProps = JsonConvert.DeserializeObject<CustomProps.Purchase>(purchase.Data);

                    purchaseProps.paymentId = paymentResult.Id;
                    purchase.Data = JsonConvert.SerializeObject(purchaseProps);

                    UpdateBusinessObject(purchase);
                }


            }
            catch (AppException ex)
            {
                //throw ex;
            }

            return result;
        }

        public BusinessObjectOutDto ConfirmPurchaseByPayment(Payment payment)
        {
            Guid purchaseId;
            BusinessObjectOutDto result = null;

            if (payment != null)
            {
                _logger.LogDebug("ConfirmPurchase", payment.Id);

                if (payment.Status == PaymentStatus.Succeeded)
                {
                    string value = "";
                    payment.Metadata.TryGetValue("purchaseId", out value);
                    purchaseId = Guid.Parse(value);

                    var confirmedPurchase = СonfirmPurchase(purchaseId);

                    result = _mapper.Map<BusinessObjectOutDto>(confirmedPurchase);
                }
            }
            else
            {
                _logger.LogDebug("Payment is null");
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
            purchase.Data = JsonConvert.SerializeObject(new CustomProps.Purchase(product.Title, username, user.Email, new Guid().ToString(), product.Id.ToString()));

            var result = createBusinessObject(purchase);
            return result;
        }

        public BusinessObject СonfirmPurchase(Guid purchaseId)
        {
            try
            {
                var purchase = _unitOfWork.BusinessObjectRepository.GetByID((purchaseId));
                BusinessObject result = null;

                if (purchase != null)
                {
                    var purchaseProps = JsonConvert.DeserializeObject<CustomProps.Purchase>(purchase.Data);

                    purchaseProps.status = PurchaseStatus.Succeeded;
                    purchase.IsRemoved = true;
                    purchase.Data = JsonConvert.SerializeObject(purchaseProps);

                    result = UpdateBusinessObject(purchase);
                }
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public void CheckPayments()
        {
            var filter = new BusinessObjectFilter() { MetatypeId = MetatypeData.Purchase.Id };
            var purchases = GetBusinessObjectsDto(filter);


        }
    }

}