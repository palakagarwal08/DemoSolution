using DemoSolution.Models;
using System.Data.SqlClient;

namespace DemoSolution.Services
{
    public class ProductService
    {
        private static string db_source = "appserver0810.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "Honey@well123";
        private static string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection( _builder.ConnectionString);

        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> products = new List<Product>();

            string statement = "Select ProductId, ProductName, Quantity from Products";

            conn.Open();

            SqlCommand cmd = new SqlCommand( statement, conn );

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while ( reader.Read())
                {
                    products.Add(new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    });
                }
            }

            conn.Close();
            return products;
        }
    }
}

