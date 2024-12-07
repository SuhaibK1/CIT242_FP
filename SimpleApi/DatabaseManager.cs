using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class DatabaseManager
{
    public static List<Product> GetProducts()
    {
        var products = new List<Product>();
        string connectionString = "Data Source=Northwind.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                @"SELECT ProductID, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued 
                  FROM Products;";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = int.Parse(reader["ProductID"].ToString()),
                        ProductName = reader["ProductName"].ToString(),
                        SupplierId = int.Parse(reader["SupplierID"].ToString()),
                        CategoryId = int.Parse(reader["CategoryID"].ToString()),
                        QuantityPerUnit = reader["QuantityPerUnit"].ToString(),
                        UnitPrice = decimal.Parse(reader["UnitPrice"].ToString()),
                        UnitsInStock = int.Parse(reader["UnitsInStock"].ToString()),
                        UnitsOnOrder = int.Parse(reader["UnitsOnOrder"].ToString()),
                        ReorderLevel = int.Parse(reader["ReorderLevel"].ToString()),
                        Discontinued = int.Parse(reader["Discontinued"].ToString())
                    };
                    products.Add(product);
                }
            }
        }
        return products;
    }

    public static void CreateProduct(Product product)
    {
        string connectionString = "Data Source=Northwind.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                @"INSERT INTO Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued) 
                  VALUES (@ProductName, @SupplierId, @CategoryId, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued);";

            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@SupplierId", product.SupplierId);
            command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
            command.Parameters.AddWithValue("@QuantityPerUnit", product.QuantityPerUnit);
            command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock);
            command.Parameters.AddWithValue("@UnitsOnOrder", product.UnitsOnOrder);
            command.Parameters.AddWithValue("@ReorderLevel", product.ReorderLevel);
            command.Parameters.AddWithValue("@Discontinued", product.Discontinued);

            command.ExecuteNonQuery();
        }
    }
}
