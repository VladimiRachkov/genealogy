
using System.Threading.Tasks;
using Genealogy.Models;
using Yandex.Checkout.V3;
using System;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        Task<string> DoPayment(PaymentInDto payment);
        BusinessObjectOutDto ConfirmPurchaseByPayment(Payment payment);
        BusinessObject СonfirmPurchase(Guid purchaseId);

        void CheckPayments();
    }
}