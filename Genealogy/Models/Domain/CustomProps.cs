using System;
using Genealogy.Data;
using Yandex.Checkout.V3;

namespace Genealogy.Models
{
    public class CustomProps
    {
        public class Product
        {
            public int price { get; set; }
            public string imageUrl { get; set; }
            public string description { get; set; }
            public Product(int price, string imageUrl, string description)
            {
                this.price = price;
                this.imageUrl = imageUrl;
                this.description = description;
            }
        }
        public class Purchase
        {
            public string product { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public PurchaseStatus status { get; set; }
            public Purchase(string product, string username, string email, PurchaseStatus status = PurchaseStatus.Pending)
            {
                this.product = product;
                this.username = username;
                this.email = email;
                this.status = status;
            }
        }
    }
}