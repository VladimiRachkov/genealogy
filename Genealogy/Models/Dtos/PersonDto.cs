
using System;

namespace Genealogy.Models
{
    public class PersonDto
    {
        public Guid? Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public Cemetery Cemetery { get; set; }
        public string Source { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public bool? isRemoved { get; set; }
        public Guid CemeteryId { get; set; }
        public string Comment { get; set; }
        public PersonGroup PersonGroup { get; set; }
    }
}