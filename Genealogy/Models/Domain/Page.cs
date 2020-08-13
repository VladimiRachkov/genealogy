
using System;

namespace Genealogy.Models
{
    public class Page
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool isRemoved { get; set; }
        public bool isSection { get; set; }
    }
}