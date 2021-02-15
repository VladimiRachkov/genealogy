using System;

namespace Genealogy.Models
{
    public class LinkRelation
    {
        public Guid Id { get; set; }
        public Guid Metatype { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}