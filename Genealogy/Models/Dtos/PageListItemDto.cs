
using System;

namespace Genealogy.Models
{
    public class PageListItemDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool? isRemoved { get; set; }
        public bool? isSection { get; set; }
    }
}