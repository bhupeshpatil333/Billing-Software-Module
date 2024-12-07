using My_Task1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace My_Task1.DAL
{
    public class DatabaseHelper
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BillingDB"].ConnectionString;

        public int GetTotalSales()
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Billings", conn);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public decimal GetTotalRevenue()
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT SUM(TotalAmount) FROM Billings", conn);
                conn.Open();
                return (decimal)(cmd.ExecuteScalar() ?? 0);
            }
        }
        public void AddCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Customers (Name, Gender, Contact, Email) VALUES (@Name, @Gender, @Contact, @Email)", conn);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Gender", customer.Gender);
                cmd.Parameters.AddWithValue("@Contact", customer.Contact);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }
            return customers;
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE CustomerID = @CustomerID", conn);
                cmd.Parameters.AddWithValue("@CustomerID", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
            }
            return customer;
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Customers SET Name = @Name, Gender = @Gender, Contact = @Contact, Email = @Email WHERE CustomerID = @CustomerID", conn);
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Gender", customer.Gender);
                cmd.Parameters.AddWithValue("@Contact", customer.Contact);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerID = @CustomerID", conn);
                cmd.Parameters.AddWithValue("@CustomerID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Product Methods
        public void AddProduct(Product product)
        {
            product.TotalAmount = product.ProductPrice * product.ProductQuantity;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Products (ProductName, ProductPrice, ProductQuantity, ProductBrand, ProductSupplier, OldStock, ProductCategory,TotalAmount) VALUES (@ProductName, @ProductPrice, @ProductQuantity, @ProductBrand, @ProductSupplier, @OldStock, @ProductCategory,@TotalAmount)", conn);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                cmd.Parameters.AddWithValue("@ProductQuantity", product.ProductQuantity);
                cmd.Parameters.AddWithValue("@ProductBrand", product.ProductBrand);
                cmd.Parameters.AddWithValue("@ProductSupplier", product.ProductSupplier);
                cmd.Parameters.AddWithValue("@OldStock", product.OldStock);
                cmd.Parameters.AddWithValue("@ProductCategory", product.ProductCategory);
                cmd.Parameters.AddWithValue("@TotalAmount", product.TotalAmount); // Store TotalAmount
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products", conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = reader["ProductName"].ToString(),
                            ProductPrice = (decimal)reader["ProductPrice"],
                            ProductQuantity = (int)reader["ProductQuantity"],
                            ProductBrand = reader["ProductBrand"].ToString(),
                            ProductSupplier = reader["ProductSupplier"].ToString(),
                            OldStock = (int)reader["OldStock"],
                            ProductCategory = reader["ProductCategory"].ToString(),
                            // Calculate TotalAmount
                            TotalAmount = (decimal)reader["ProductPrice"] * (int)reader["ProductQuantity"] // Assuming the total amount can be calculated this way
                        });
                    }
                }
            }
            return products;
        }



        public Product GetProductById(int id)
        {
            Product product = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE ProductID = @ProductID", conn);
                cmd.Parameters.AddWithValue("@ProductID", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = reader["ProductName"].ToString(),
                            ProductPrice = (decimal)reader["ProductPrice"],
                            ProductQuantity = (int)reader["ProductQuantity"],
                            ProductBrand = reader["ProductBrand"].ToString(),
                            ProductSupplier = reader["ProductSupplier"].ToString(),
                            OldStock = (int)reader["OldStock"],
                            ProductCategory = reader["ProductCategory"].ToString()

                        };
                    }
                }
            }
            return product;
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Products SET ProductName = @ProductName, ProductPrice = @ProductPrice, ProductQuantity = @ProductQuantity, ProductBrand = @ProductBrand, ProductSupplier = @ProductSupplier, OldStock = @OldStock, ProductCategory = @ProductCategory WHERE ProductID = @ProductID", conn);
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                cmd.Parameters.AddWithValue("@ProductQuantity", product.ProductQuantity);
                cmd.Parameters.AddWithValue("@ProductBrand", product.ProductBrand);
                cmd.Parameters.AddWithValue("@ProductSupplier", product.ProductSupplier);
                cmd.Parameters.AddWithValue("@OldStock", product.OldStock);
                cmd.Parameters.AddWithValue("@ProductCategory", product.ProductCategory);
                cmd.Parameters.AddWithValue("@TotalAmount", product.TotalAmount);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ProductID = @ProductID", conn);
                cmd.Parameters.AddWithValue("@ProductID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Billing Methods
        public void AddBilling(Billing billing)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Billings (CustomerID, TotalAmount, BillingDate) VALUES (@CustomerID, @TotalAmount, @BillingDate)", conn);
                cmd.Parameters.AddWithValue("@CustomerID", billing.CustomerID);
                cmd.Parameters.AddWithValue("@TotalAmount", billing.TotalAmount);
                cmd.Parameters.AddWithValue("@BillingDate", billing.BillingDate);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Billing> GetBillings()
        {
            List<Billing> billings = new List<Billing>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //SqlCommand cmd = new SqlCommand("SELECT * FROM Billings", conn);
                SqlCommand cmd = new SqlCommand(
            "SELECT b.BillingID, b.CustomerID, b.TotalAmount, b.BillingDate, c.Name AS CustomerName " +
            "FROM Billings b " +
            "JOIN Customers c ON b.CustomerID = c.CustomerID", conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billings.Add(new Billing
                        {
                            BillingID = (int)reader["BillingID"],
                            CustomerID = (int)reader["CustomerID"],
                            TotalAmount = (decimal)reader["TotalAmount"],
                            BillingDate = reader["BillingDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["BillingDate"],
                            CustomerName = reader["CustomerName"].ToString()
                        });
                    }
                }
            }
            return billings;
        }

        public Billing GetBillingById(int id)
        {
            Billing billing = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Billings WHERE BillingID = @BillingID", conn);
                cmd.Parameters.AddWithValue("@BillingID", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        billing = new Billing
                        {
                            BillingID = (int)reader["BillingID"],
                            CustomerID = (int)reader["CustomerID"],
                            TotalAmount = (decimal)reader["TotalAmount"],
                            BillingDate = (DateTime)reader["BillingDate"]
                        };
                    }
                }
            }
            return billing;
        }

        public void UpdateBilling(Billing billing)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Billings SET CustomerID = @CustomerID, TotalAmount = @TotalAmount, BillingDate = @BillingDate WHERE BillingID = @BillingID", conn);
                cmd.Parameters.AddWithValue("@BillingID", billing.BillingID);
                cmd.Parameters.AddWithValue("@CustomerID", billing.CustomerID);
                cmd.Parameters.AddWithValue("@TotalAmount", billing.TotalAmount);
                cmd.Parameters.AddWithValue("@BillingDate", billing.BillingDate);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteBilling(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Billings WHERE BillingID = @BillingID", conn);
                cmd.Parameters.AddWithValue("@BillingID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}