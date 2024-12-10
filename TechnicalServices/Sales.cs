using System.Data;
using Microsoft.Data.SqlClient;
using ydai5.Domain;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ydai5.TechnicalServices
{
    public class Sales
    {
        private string? _connectionString;
        public Sales()
        {
            // constructor logic
            ConfigurationBuilder Builder = new();
            Builder.SetBasePath(Directory.GetCurrentDirectory());
            Builder.AddJsonFile("appsettings.json");
            IConfiguration Configuration = Builder.Build();
            _connectionString = Configuration.GetConnectionString("BAIS3150");
        }
        public bool AddItem(string itemCode, string description, decimal unitPrice)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand addItemCommand = new SqlCommand("AddItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                addItemCommand.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.Char, 6) { Value = itemCode });
                addItemCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 255) { Value = description });
                addItemCommand.Parameters.Add(new SqlParameter("@UnitPrice", SqlDbType.Decimal) { Value = unitPrice });

                addItemCommand.ExecuteNonQuery();
                success = true;
            }

            return success;
        }

        public bool UpdateItem(string itemCode, string description, decimal unitPrice)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand updateItemCommand = new SqlCommand("UpdateItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                updateItemCommand.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.Char, 6) { Value = itemCode });
                updateItemCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 255) { Value = description });
                updateItemCommand.Parameters.Add(new SqlParameter("@UnitPrice", SqlDbType.Decimal) { Value = unitPrice });

                updateItemCommand.ExecuteNonQuery();
                success = true;
            }

            return success;
        }

        public bool DeleteItem(string itemCode)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand deleteItemCommand = new SqlCommand("DeleteItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                deleteItemCommand.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.Char, 6) { Value = itemCode });

                deleteItemCommand.ExecuteNonQuery();
                success = true;
            }

            return success;
        }

        public List<Item> GetItems()
        {
            List<Item> items = new();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand getItemsCommand = new SqlCommand("GetItems", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (SqlDataReader reader = getItemsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Item item = new Item
                        {
                            ItemCode = reader["ItemCode"].ToString(),
                            Description = reader["Description"].ToString(),
                            UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                            Status = reader["Status"].ToString()
                        };

                        items.Add(item);
                    }
                }
            }

            return items;
        }

        public Item FindItem(string itemCode)
        {
            Item item = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand findItemCommand = new SqlCommand("FindItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                findItemCommand.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.Char, 6) { Value = itemCode });

                using (SqlDataReader reader = findItemCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new Item
                        {
                            ItemCode = reader["ItemCode"].ToString(),
                            Description = reader["Description"].ToString(),
                            UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                            Status = reader["Status"].ToString()
                        };
                    }
                }
            }

            return item;
        }

        public bool AddCustomer(int customerId, string customerName, string address, string city, string province, string postalCode)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand addCustomerCommand = new SqlCommand("AddCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                addCustomerCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = customerId });
                addCustomerCommand.Parameters.Add(new SqlParameter("@CustomerName", SqlDbType.VarChar, 25) { Value = customerName });
                addCustomerCommand.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar, 50) { Value = address });
                addCustomerCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 25) { Value = city });
                addCustomerCommand.Parameters.Add(new SqlParameter("@Province", SqlDbType.VarChar, 25) { Value = province });
                addCustomerCommand.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar, 7) { Value = postalCode });

                addCustomerCommand.ExecuteNonQuery();
                success = true;
            }

            return success;
        }

        public bool UpdateCustomer(int customerId, string customerName, string address, string city, string province, string postalCode)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand updateCustomerCommand = new SqlCommand("UpdateCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                updateCustomerCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = customerId });
                updateCustomerCommand.Parameters.Add(new SqlParameter("@CustomerName", SqlDbType.VarChar, 25) { Value = customerName });
                updateCustomerCommand.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar, 50) { Value = address });
                updateCustomerCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 25) { Value = city });
                updateCustomerCommand.Parameters.Add(new SqlParameter("@Province", SqlDbType.VarChar, 25) { Value = province });
                updateCustomerCommand.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar, 7) { Value = postalCode });

                updateCustomerCommand.ExecuteNonQuery();
                success = true;
            }

            return success;
        }

        public Customer FindCustomer(int customerId)
        {
            Customer? customer = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand findCustomerCommand = new SqlCommand("FindCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                findCustomerCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = customerId });

                using (SqlDataReader reader = findCustomerCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            CustomerName = reader["CustomerName"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            Province = reader["Province"].ToString(),
                            PostalCode = reader["PostalCode"].ToString()
                        };
                    }
                }
            }

            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand getAllCustomersCommand = new SqlCommand("GetAllCustomers", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (SqlDataReader reader = getAllCustomersCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            CustomerName = reader["CustomerName"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            Province = reader["Province"].ToString(),
                            PostalCode = reader["PostalCode"].ToString()
                        };

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }

        public bool DeleteCustomer(int customerId)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand deleteCustomerCommand = new SqlCommand("DeleteCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                deleteCustomerCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = customerId });

                deleteCustomerCommand.ExecuteNonQuery();
                success = true;
            }

            return success;
        }

        public int AddSale(Sale ABCSale)
        {
            Console.WriteLine("------------SaleNumber--------------");
            Console.WriteLine(ABCSale.SaleNumber);
            Console.WriteLine("------------SaleNumber--------------");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    SqlCommand addSaleCommand = new SqlCommand("AddSale", connection, transaction)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addSaleCommand.Parameters.AddWithValue("@SaleNumber", ABCSale.SaleNumber);
                    addSaleCommand.Parameters.AddWithValue("@SaleDate", ABCSale.SaleDate);
                    addSaleCommand.Parameters.AddWithValue("@SalesPerson", ABCSale.SalesPerson);
                    addSaleCommand.Parameters.AddWithValue("@SubTotal", ABCSale.SubTotal);
                    addSaleCommand.Parameters.AddWithValue("@GST", ABCSale.GST);
                    addSaleCommand.Parameters.AddWithValue("@SaleTotal", ABCSale.SaleTotal);
                    addSaleCommand.Parameters.AddWithValue("@CustomerID", ABCSale.CustomerID);

                    addSaleCommand.ExecuteNonQuery();

                    foreach (var item in ABCSale.SaleItems)
                    {
                        SqlCommand addSaleItemCommand = new SqlCommand("AddSaleItem", connection, transaction)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        addSaleItemCommand.Parameters.AddWithValue("@SaleNumber", ABCSale.SaleNumber);
                        addSaleItemCommand.Parameters.AddWithValue("@ItemCode", item.ItemCode);
                        addSaleItemCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                        addSaleItemCommand.Parameters.AddWithValue("@ItemTotal", item.ItemTotal);

                        addSaleItemCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }

            return ABCSale.SaleNumber;
        }

    }
}