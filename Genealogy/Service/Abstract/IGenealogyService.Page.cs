using System;
using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<PageDto> GetPage(PageFilter filter);
        PageDto AddPage(PageDto newPage);
        PageDto MarkAsRemovedPage(Guid id);
        PageDto ChangePage(PageDto pageDto);
    }
}