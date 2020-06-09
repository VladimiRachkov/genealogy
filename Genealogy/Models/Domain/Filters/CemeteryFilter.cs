using System;

namespace Genealogy.Models.Domain.Filters
{
    public class CemeteryFilter
    {
        public Guid Id { get; set; }
        public bool Removed { get; set; }

    }
}