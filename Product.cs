using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace MEGAVENTORY
{
    // Product class to represent a product entity
    public class Product
    {
        public string SKU { get; set; } // Stock Keeping Unit
        public string Description { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal PurchasePrice { get; set; }

        // Constructor
        public Product(string sku, string description, decimal salesPrice, decimal purchasePrice)
        {
            SKU = sku;
            Description = description;
            SalesPrice = salesPrice;
            PurchasePrice = purchasePrice;
        }

        // Method to convert Product object to JSON string
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

