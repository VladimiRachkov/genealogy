using System;

namespace Genealogy.Models
{
    public class BusinessObjectOutDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public Guid Metatype { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsRemoved { get; set; }
        public string Data { get; set; }
    }
}