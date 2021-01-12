
using System;
using System.Collections.Generic;

namespace Genealogy.Models
{
    public class PersonGroupDto
    {
        public Guid? Id { get; set; }
        public List<Person> Persons { get; set; }
    }
}