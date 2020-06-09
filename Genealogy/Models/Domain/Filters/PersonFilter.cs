using System;

namespace Genealogy.Models.Domain.Filters
{
    public class PersonFilter
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; }
        public string Fio { get; set; }
    }
}