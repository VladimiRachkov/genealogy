using System;
using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<BusinessObjectOutDto> GetBusinessObjectsDto(BusinessObjectFilter filter);
        IEnumerable<BusinessObject> GetBusinessObjects(BusinessObjectFilter filter);
        BusinessObjectOutDto CreateBusinessObjectsFromDto(BusinessObjectInDto boDto);
        BusinessObjectOutDto UpdateBusinessObjectDto(BusinessObjectInDto boDto);
        BusinessObject UpdateBusinessObject(BusinessObject changedBO);
        BusinessObjectsCountOutDto GetBusinessObjectsCount(BusinessObjectFilter filter);
        BusinessObjectOutDto RemoveBusinessObject(Guid id);
    }
}