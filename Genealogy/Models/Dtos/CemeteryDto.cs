using System;

namespace Genealogy.Models
{
    public class CemeteryDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public County County { get; set; }
        public bool? isRemoved { get; set; }
        public Guid CountyId { get; set; }
    }
}