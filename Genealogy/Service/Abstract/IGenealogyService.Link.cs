using System;
using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<LinkDto> GetLinks(LinkFilter filter);
        LinkDto AddLink(LinkDto link);
    }
}