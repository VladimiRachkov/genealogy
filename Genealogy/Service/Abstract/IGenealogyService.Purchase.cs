using System;
using System.Threading.Tasks;
using Genealogy.Models;
using Microsoft.AspNetCore.Http;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        Task<bool> ProductAction(Guid productId, Guid userId);
        Task<BusinessObject> ActivatePurchase(Guid purchaseId);
    }
}