using System;
using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<PageDto> GetPage(PageFilter filter);
        PageDto AddPage(PageDto newPage);
        PageDto RemovePage(Guid id);
        PageDto ChangePage(PageDto pageDto);
        List<PageListItemDto> GetPages(PageFilter filter);
        List<PageListItemDto> GetFreePages();
        PageWithLinksDto GetPageWithLinks(PageFilter filter);
    }
}