using System;
using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<LinkDto> GetLinks(LinkFilter filter);
        List<LinkDto> AddLink(LinkDto link);
        List<LinkDto> UpdateLinks(IEnumerable<LinkDto> links);
    }
}