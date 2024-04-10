using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace MEGAVENTORY
{
    // InventoryLocation class to represent an inventory location entity
    public class InventoryLocation
    {
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        // Constructor
        public InventoryLocation(string abbreviation, string name, string address)
        {
            Abbreviation = abbreviation;
            Name = name;
            Address = address;
        }

        // Method to convert InventoryLocation object to JSON string
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
