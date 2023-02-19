using System;

namespace Genealogy.Models
{
    public class CemeteryFilter
    {
        public Guid Id { get; set; }
        public bool isRemoved { get; set; }
        public Guid CountyId { get; set; }

    }
}