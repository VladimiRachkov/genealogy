using System;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        BusinessObjectOutDto GetActiveSubscription();
    }
}