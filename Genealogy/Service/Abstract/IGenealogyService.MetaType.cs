using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<MetatypeOutDto> GetMetatypes(MetatypeFilter filter);
    }
}