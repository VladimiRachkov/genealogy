
using System;

namespace Genealogy.Models
{
    public class PurchaseDto
    {
        public Guid? UserId { get; set; }
        public Guid? ProductId { get; set; }
    }
}