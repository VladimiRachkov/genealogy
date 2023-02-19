using System;
using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<CountyDto> GetCounty(CountyFilter filter);
        CountyDto AddCounty(CountyDto newCounty);
        List<CountyDto> GetCountyList();
        CountyDto RemoveCounty(Guid county);
        CountyDto ChangeCounty(CountyDto countyDto);
        CountyDto RestoreCounty(Guid countyId);

    }
}