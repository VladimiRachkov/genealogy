using System;

namespace Genealogy.Models
{
    public class BusinessObjectFilter : PaginatorInDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? MetatypeId { get; set; }
        public bool? IsRemoved { get; set; }
        public Guid? UserId { get; set; }
    }
}