using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using System.Text;

namespace FitBody
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public class DatabaseHelper
        {
            //Note that the values of the Server, Port, Database, Uid, and Pwd variables are hard-coded. It's generally not a good practice to hard-code sensitive information in code files, and this information should ideally be stored securely in configuration files or environment variables.
            private static string Server = "github";
            private static string Port = "github";
            private static string Database = "github";
            private static string Uid = "github";
            private static string Pwd = "github";
            private static string connectionString = "Server='" + Server + "';Port='" + Port + "';Database='" + Database + "';Uid='" + Uid + "';Pwd='" + Pwd + "';";

            public static MySqlConnection GetConnection()
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                return connection;
            }


            public void ExecuteNonQuery(string query)
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            }

            public MySqlDataReader ExecuteQuery(string query)
            {
                MySqlConnection connection = GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                return command.ExecuteReader();
            }
        }

        public class Encrypt
        {
            public static string HashString(string passwordString)
            {
                StringBuilder sb = new StringBuilder();
                foreach (byte b in GetHash(passwordString))
                    sb.Append(b.ToString("X2"));
                return sb.ToString();
            }

            public static byte[] GetHash(string passwordString)
            {
                using (HashAlgorithm algorithm = SHA256.Create())
                    return algorithm.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
            }
        }
    }  
}
