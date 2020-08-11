using System;
using System.Collections.Generic;

namespace Genealogy.Models
{
    public class LinkDto
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public Guid PageId { get; set; }
        public Guid TargetPageId { get; set; }
        public int Order { get; set; }
    }

    public class LinkListDto
    {
        public IEnumerable<LinkDto> links { get; set; }
    }

    public class ShortLinkDto
    {
        public string Route { get; set; }
        public string Caption { get; set; }
        public int Order { get; set; }
    }
}