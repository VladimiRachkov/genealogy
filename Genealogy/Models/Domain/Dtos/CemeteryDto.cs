using System;

namespace Genealogy.Models.Domain.Dtos
{
    public class CemeteryDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool? Removed { get; set; }
    }
}