using SqlLibrary.Dataverse;
using SqlLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLibrary.Queries
{
    public class UserQuery
    {
        public UserQuery()
        {
            //Verify Database Configuration
            if (string.IsNullOrWhiteSpace(DatabaseConfig.ConnectionString))
            {
                throw new Exception("Database configuration not found.");
            }
        }

        public IEnumerable<User> GetUsers()
        {
            var users = new List<User>();

            try
            {
                DatabaseConfig.LogDatabaseOperation("Starting to select users.");

                using (var connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT * FROM TestUsers", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? null : reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? null : reader.GetString(reader.GetOrdinal("LastName")),
                                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                            };
                            users.Add(user);
                        }
                    }
                }

                DatabaseConfig.LogDatabaseOperation("Successfully completed user selection.");
            }
            catch (Exception ex)
            {
                DatabaseConfig.LogError("An error occurred while selecting users.", ex);
                throw;
            }

            return users;
        }
    }
}