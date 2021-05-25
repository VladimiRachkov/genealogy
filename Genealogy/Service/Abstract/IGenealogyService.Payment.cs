
using System.Threading.Tasks;
using Genealogy.Models;
using Yandex.Checkout.V3;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        Task<string> DoPayment(PaymentInDto payment);
        BusinessObjectOutDto ConfirmPurchase(Payment payment);
    }
}