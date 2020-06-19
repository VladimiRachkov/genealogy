
using System;

namespace Genealogy.Models
{
    public class PageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Removed { get; set; }
    }
}