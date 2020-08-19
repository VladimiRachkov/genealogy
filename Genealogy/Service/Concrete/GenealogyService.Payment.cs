using System.Threading.Tasks;
using Genealogy.Service.Astract;
using Yandex.Checkout.V3;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public async Task<string> DoPayment(string returnUrl)
        {
            var client = new Yandex.Checkout.V3.Client(shopId: "739084", secretKey: "test_5LLiubI6pAXxCt-13sfn9WymESZgeE9Z30BrZIB3BAQ");
            AsyncClient asyncClient = client.MakeAsync();

            var newPayment = new NewPayment
            {
                Amount = new Amount { Value = 100.00m, Currency = "RUB" },
                Confirmation = new Confirmation
                {
                    Type = ConfirmationType.Redirect,
                    ReturnUrl = returnUrl
                }
            };
            Payment payment = await asyncClient.CreatePaymentAsync(newPayment);
            string url = payment.Confirmation.ConfirmationUrl;
            return url;
        }
    }

}