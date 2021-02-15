using System;

namespace Genealogy.Models
{
    public class BusinessObjectFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid MetatypeId { get; set; }
        public bool? IsRemoved { get; set; }
    }
}