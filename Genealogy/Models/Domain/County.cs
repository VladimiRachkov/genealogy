using System;

namespace Genealogy.Models
{
    public class County
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Coords { get; set; }
        public bool isRemoved { get; set; }
    }
}