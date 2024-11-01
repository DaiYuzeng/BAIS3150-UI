using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ydai.Domain;

namespace ydai.Categories
{
    public class ydai5CategoriesModel : PageModel
    {

        public List<Category> Categories { get; set; } = new();

        static string ydai5ConnectionString = "Server=dev1.baist.ca;Database=Northwind;User Id=ydai5;Password=Xiaodai0712@;Encrypt=true;TrustServerCertificate=true;";

        public void OnGet()
        {
            using (SqlConnection connection = new SqlConnection(ydai5ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("ydai5.GetNorthwindCategories", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Category category = new Category
                    {
                        CategoryName = reader["CategoryName"].ToString(),
                        Description = reader["Description"].ToString()
                    };

                    if (reader["Picture"] != DBNull.Value)
                    {
                        byte[] imageData = (byte[])reader["Picture"];
                        // Remove OLE header for BMP files (Northwind uses OLE embedded bitmaps)
                        const int OleHeaderLength = 78;
                        byte[] cleanImageData = imageData.Skip(OleHeaderLength).ToArray();

                        category.Picture = Convert.ToBase64String(cleanImageData);
                    }

                    Categories.Add(category);
                }
            }
        }
    }
}
