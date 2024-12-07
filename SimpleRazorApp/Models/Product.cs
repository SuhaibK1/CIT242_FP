using System.Text.Json.Serialization;

namespace SimpleRazorApp.Models{
    public class Product
    {
        [JsonPropertyName("productId")]
        public int ProductID { get; set; }
        
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }
        
        [JsonPropertyName("supplierId")]
        public int SupplierID { get; set; }
        
        [JsonPropertyName("categoryId")]
        public int CategoryID { get; set; }
        
        [JsonPropertyName("quantityPerUnit")]
        public string QuantityPerUnit { get; set; }
        
        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }
        
        [JsonPropertyName("unitsInStock")]
        public int UnitsInStock { get; set; }
        
        [JsonPropertyName("unitsOnOrder")]
        public int UnitsOnOrder { get; set; }
        
        [JsonPropertyName("reorderLevel")]
        public int ReorderLevel { get; set; }
        
        [JsonPropertyName("discontinued")]
        public int Discontinued { get; set; }
    }

}