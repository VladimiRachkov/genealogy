using System;

namespace Genealogy.Models
{
    public class PageFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? isRemoved { get; set; }
        public bool? isSection { get; set; }
    }
}