using System;

namespace Genealogy.Models
{
    public class Cemetery
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool isRemoved { get; set; }
    }
}