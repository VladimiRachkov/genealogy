using System;
using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<CemeteryDto> GetCemetery(CemeteryFilter filter);
        CemeteryDto AddCemetery(CemeteryDto newCemetery);
        List<CemeteryDto> GetCemeteryList();
        CemeteryDto RemoveCemetery(Guid cemetery);
        CemeteryDto ChangeCemetery(CemeteryDto cemeteryDto);
        CemeteryDto RestoreCemetery(Guid cemeteryId);

    }
}