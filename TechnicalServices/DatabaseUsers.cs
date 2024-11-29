using System.Data;
using Microsoft.Data.SqlClient;
using ydai5.Domain;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ydai5.TechnicalServices
{
    public class DatabaseUsers
    {
        private string? _connectionString;
        public DatabaseUsers()
        {
            // constructor logic
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            _connectionString = DatabaseUsersConfiguration.GetConnectionString("BAIS3150");
        }
        public DatabaseUser GetDatabaseUser()
        {
            SqlConnection BAIS3150 = new();
            BAIS3150.ConnectionString = _connectionString;
            BAIS3150.Open();
            SqlCommand GetDatabaseUserCommand = new()
            {
                Connection = BAIS3150,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetDatabaseUser"
            };
            SqlDataReader DatabaseUserReader = GetDatabaseUserCommand.ExecuteReader();
            DatabaseUser CurrentDatabaseUser = new();
            if (DatabaseUserReader.HasRows)
            {
                DatabaseUserReader.Read();
                CurrentDatabaseUser.CurrentUser = (string)DatabaseUserReader["CurrentUser"];
                CurrentDatabaseUser.SystemUser = (string)DatabaseUserReader["SystemUser"];
                CurrentDatabaseUser.SessionUser = (string)DatabaseUserReader["SessionUser"];
            }
            DatabaseUserReader.Close();
            BAIS3150.Close();
            return CurrentDatabaseUser;
        }
    }
}