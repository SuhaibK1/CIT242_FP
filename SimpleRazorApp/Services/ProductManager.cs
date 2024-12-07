using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using SimpleRazorApp.Models;

namespace SimpleRazorApp.Services
{
public class ProductManager
{
    private readonly HttpClient _client;

    public ProductManager(HttpClient client)
    {
        // Disable SSL validation for development purposes
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
        
        _client = new HttpClient(handler); // Use the handler to bypass SSL validation
    }

    public async Task<IEnumerable<Product>> GetProductList()
    {
        var response = await _client.GetAsync("http://localhost:5266/Products");
    response.EnsureSuccessStatusCode();

    var stream = await response.Content.ReadAsStreamAsync();
    return await

    JsonSerializer.DeserializeAsync<IEnumerable<Product>>(stream);
    }

    public async Task SaveProductInfo(Product Product)
    {
    var json = JsonSerializer.Serialize(Product);
    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
    var response = await _client.PostAsync("http://localhost:5266/Products", content);
    response.EnsureSuccessStatusCode();
    }

    public async Task UpdateProductInfo(Product product)
    {
        // Logic to update the product in the database
        var connectionString = "Data Source=Northwind.db";
        using (var connection = new SqliteConnection(connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE Products SET ProductName = @ProductName, SupplierID = @SupplierId, CategoryID = @CategoryId, 
                QuantityPerUnit = @QuantityPerUnit, UnitPrice = @UnitPrice, UnitsInStock = @UnitsInStock, 
                UnitsOnOrder = @UnitsOnOrder, ReorderLevel = @ReorderLevel WHERE ProductID = @ProductId;";

            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@SupplierId", product.SupplierID);
            command.Parameters.AddWithValue("@CategoryId", product.CategoryID);
            command.Parameters.AddWithValue("@QuantityPerUnit", product.QuantityPerUnit);
            command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock);
            command.Parameters.AddWithValue("@UnitsOnOrder", product.UnitsOnOrder);
            command.Parameters.AddWithValue("@ReorderLevel", product.ReorderLevel);
            command.Parameters.AddWithValue("@ProductId", product.ProductID);

            await command.ExecuteNonQueryAsync();
        }
    }

}
}