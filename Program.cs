using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "d6370673e447cd03@m146554";

        // Initialize HttpClient
        using (HttpClient client = new HttpClient())
        {
            // Set API key in request headers
            client.DefaultRequestHeaders.Add("APIKEY", apiKey);

            // Insert products
            await InsertProduct(client, "1112256", "Nike shoes", 99.99m, 44.99m);
            await InsertProduct(client, "1112248", "Adidas shoes", 99.99m, 44.99m);
         
            // Insert inventory location
            await InsertInventoryLocation(client, "Test", "Test Project Location", "Example 20, Athens");

            await InsertSupplierAndClient(client);

            // Establish relationships
            await EstablishProductClientRelationship(client, "1112256", "babis");
            await EstablishProductSupplierRelationship(client, "1112248", "odysseus");

            // Update product availability
            await UpdateProductAvailability(client, "1112256", 5, 44.99m);
        }
    }

    static async Task InsertProduct(HttpClient client, string sku, string description, decimal salesPrice, decimal purchasePrice)
    {
        string apiUrl = "https://api.megaventory.com/v2017a/Product/ProductUpdate";

        var product = new
        {
            SKU = sku,
            Description = description,
            SellingPrice = salesPrice,
            PurchasePrice = purchasePrice
        };

        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(apiUrl, content);
        var result = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Product inserted: " + result);
    }

    static async Task InsertSupplierAndClient(HttpClient client)
    {
        string apiUrl = "https://api.megaventory.com/v2017a/SupplierClient/SupplierClientUpdate";

        var supplierData = new
        {
            mvSupplierClient = new
            {
                SupplierClientType = "Supplier",
                SupplierClientName = "odysseus",
                mvContacts = new[]
                {
                new
                {
                    ContactName = "odysseus",
                    ContactDepartment = "General",
                    ContactEmail = "odysseus@exampletest.com",
                    ContantIsPrimary = true
                }
            },
                SupplierClientBillingAddress = "Example 10, Athens",
                SupplierClientShippingAddress = "Example 10, Athens"
            },
            mvGrantPermissionsToAllUser = true,
            mvRecordAction = "Insert",
            mvInsertUpdateDeleteSourceApplication = "Magento"
        };

        var clientData = new
        {
            mvSupplierClient = new
            {
                SupplierClientType = "Client",
                SupplierClientName = "babis",
                mvContacts = new[]
                {
                new
                {
                    ContactName = "babis",
                    ContactDepartment = "General",
                    ContactEmail = "babis@exampletest.com",
                    ContantIsPrimary = true
                }
            },
                SupplierClientBillingAddress = "Example 8, Athens",
                SupplierClientShippingAddress = "Example 8, Athens"
            },
            mvGrantPermissionsToAllUser = true,
            mvRecordAction = "Insert",
            mvInsertUpdateDeleteSourceApplication = "Magento"
        };

        var combinedData = new[] { supplierData, clientData };

        var json = JsonConvert.SerializeObject(combinedData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(apiUrl, content);
        var result = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Supplier and Client inserted: " + result);
    }


    static async Task InsertInventoryLocation(HttpClient client, string abbreviation, string name, string address)
    {
        string apiUrl = "https://api.megaventory.com/v2017a/InventoryLocationStock/InventoryLocationStockUpdate";

        var locationData = new
        {
            Abbreviation = abbreviation,
            Name = name,
            Address = address
        };

        var json = JsonConvert.SerializeObject(locationData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(apiUrl, content);
        var result = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Inventory location inserted: " + result);
    }

    static async Task EstablishProductClientRelationship(HttpClient client, string sku, string clientName)
    {
        string apiUrl = "https://api.megaventory.com/v2017a/InsertProductClient";

        var relationshipData = new
        {
            ProductSKU = sku,
            ClientName = clientName
        };

        var json = JsonConvert.SerializeObject(relationshipData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(apiUrl, content);
        var result = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Product-Client relationship established: " + result);
    }

    static async Task EstablishProductSupplierRelationship(HttpClient client, string sku, string supplierName)
    {
        string apiUrl = "https://api.megaventory.com/v2017a/ProductSupplier/ProductSupplierUpdate";

        var relationshipData = new
        {
            ProductSKU = sku,
            SupplierName = supplierName
        };

        var json = JsonConvert.SerializeObject(relationshipData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(apiUrl, content);
        var result = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Product-Supplier relationship established: " + result);
    }

    static async Task UpdateProductAvailability(HttpClient client, string sku, int quantity, decimal cost)
    {
        string apiUrl = "https://api.megaventory.com/v2017a/Product/ProductUpdate";

        var availabilityData = new
        {
            ProductSKU = sku,
            Quantity = quantity,
            Cost = cost
        };

        var json = JsonConvert.SerializeObject(availabilityData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync(apiUrl, content);
        var result = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Product availability updated: " + result);
    }
}
