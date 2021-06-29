
using System;
using System.Collections.Generic;

namespace Genealogy.Models
{
    public class PersonGroup
    {
        public Guid Id { get; set; }
        public PersonGroup(Guid id)
        {
            Id = id;
        }

    }
}