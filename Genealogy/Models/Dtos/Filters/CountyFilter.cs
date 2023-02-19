using System;

namespace Genealogy.Models
{
    public class CountyFilter
    {
        public Guid Id { get; set; }
        public bool isRemoved { get; set; }
        public string Name { get; set; }

    }
}