using System;

namespace Genealogy.Models
{
    public class PersonFilter : PaginatorInDto
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; }
        public string Fio { get; set; }
        public Guid CemeteryId { get; set; }
        public Guid CountyId { get; set; }
    }
}