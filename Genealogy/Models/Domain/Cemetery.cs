using System;

namespace Genealogy.Models
{
    public class Cemetery
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public County County { get; set; }
        public bool isRemoved { get; set; }
    }
}