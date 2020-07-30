using System;

namespace Genealogy.Models
{
    public class LinkFilter
    {
        public Guid PageId { get; set; }
        public bool? isRemoved { get; set; }
    }
}