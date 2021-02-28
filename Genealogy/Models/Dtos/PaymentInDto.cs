using System;

namespace Genealogy.Models
{
    public class PaymentInDto
    {
        public string ReturnUrl;
        public Guid ProductId;
        public Guid UserId;
    }

    public class PurchaseInDto { 
        public Guid Id;
    }
}