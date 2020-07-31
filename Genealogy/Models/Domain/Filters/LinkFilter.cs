using System;

namespace Genealogy.Models
{
    public class LinkFilter
    {
        public Guid? PageId { get; set; }
        public Guid? TargetPageId { get; set; }
        public bool? isRemoved { get; set; }
    }
}