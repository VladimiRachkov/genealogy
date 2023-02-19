using System;

namespace Genealogy.Models
{
    public class Cemetery
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual County County { get; set; }
        public bool isRemoved { get; set; }
        public Guid CountyId { get; set; }
    }
}