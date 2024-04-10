using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MEGAVENTORY
{
    // SupplierClient class to represent a supplier or client entity
    public class SupplierClient
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public string Phone { get; set; }

        // Constructor
        public SupplierClient(string name, string email, string shippingAddress, string phone)
        {
            Name = name;
            Email = email;
            ShippingAddress = shippingAddress;
            Phone = phone;
        }

        // Method to convert SupplierClient object to JSON string
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
