
using System;
using System.Collections.Generic;

namespace Genealogy.Models
{
    public class PersonGroup
    {
        public Guid Id { get; set; }
        public List<Person> Persons { get; set; }
        PersonGroup()
        {
            Persons = new List<Person>();
        }

    }
}