
using System;
using System.Threading.Tasks;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        Task<string> DoPayment(PaymentInDto payment);
        BusinessObjectOutDto ConfirmPurchase(PurchaseInDto purchaseDto);
    }
}