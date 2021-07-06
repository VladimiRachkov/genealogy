using System;

namespace Genealogy.Models
{
    public class BusinessObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public Guid MetatypeId { get; set; }
        public Metatype Metatype { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsRemoved { get; set; }
        public string Data { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}