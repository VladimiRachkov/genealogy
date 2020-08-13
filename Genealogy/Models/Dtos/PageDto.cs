using System;
using System.Collections.Generic;

namespace Genealogy.Models
{
    public class PageDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool? isRemoved { get; set; }
        public bool? IsSection { get; set; }
    }

    public class PageWithLinksDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<ShortLinkDto> Links { get; set; }
        public bool isSection { get; set; }
    }
}