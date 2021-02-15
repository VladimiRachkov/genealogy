using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<BusinessObjectOutDto> GetBusinessObjects(BusinessObjectFilter filter);
        BusinessObjectOutDto CreateBusinessObject(BusinessObjectInDto boDto);
        BusinessObjectOutDto UpdateBusinessObject(BusinessObjectInDto boDto);
    }
}