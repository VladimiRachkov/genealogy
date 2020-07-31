
using System;

namespace Genealogy.Models
{
    public class Link
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public Guid PageId { get; set; }
        public Guid TargetPageId { get; set; }
        public int Order { get; set; }
    }
}