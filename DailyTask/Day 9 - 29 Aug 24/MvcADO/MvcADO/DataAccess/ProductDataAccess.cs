using MvcADO.Models;
using System.Data.SqlClient;

namespace MvcADO.DataAccess
{
    public class ProductDataAccess
    {

        static SqlConnection conn;

        static SqlCommand cmd;

        public static void GetConnection()
        {
            conn = new SqlConnection("data source = PTSQLTESTDB01; database = Sports_Sownthari; integrated security = true");

            conn.Open();

        }
        public void Create()
        {
            GetConnection();

            cmd = new SqlCommand("create table Product(ProId int primary key, ProName varchar(50))", conn);

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public  Product Insert(Product product)
        {
            GetConnection();

            cmd = new SqlCommand("insert into Product values(@id,@name)", conn);

            cmd.Parameters.AddWithValue("@id", product.ProId);
            cmd.Parameters.AddWithValue("@name", product.ProName);

            cmd.ExecuteNonQuery();
                        
            conn.Close();

            return product;

        }

        public  IEnumerable<Product> Retrieve()
        {
            List<Product> productList = new List<Product>();

            GetConnection();
            cmd = new SqlCommand("select * from Product", conn);

            cmd.ExecuteNonQuery();

            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                //Console.WriteLine(sdr[0].ToString() + " " + sdr[1].ToString());
                productList.Add(new Product() { ProId = Convert.ToInt32(sdr[0].ToString()), ProName = sdr[1].ToString() });
            }
                        
            conn.Close();

            return productList;
        }

        public Product Update(Product product)
        {
            GetConnection();
                        
            cmd = new SqlCommand("update Product set ProName = @name where ProId = @id", conn);
            cmd.Parameters.AddWithValue("@name", product.ProName);
            cmd.Parameters.AddWithValue("@id", product.ProId);
            cmd.ExecuteNonQuery();
            conn.Close();
            return product;
        }

        public void Delete(int id)
        {

            GetConnection();
            cmd = new SqlCommand("delete from Product where ProId = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();             

            conn.Close();

        }

        public Product Search(int id)
        {
            Product product = null;

            GetConnection();

            cmd = new SqlCommand("select * from Product where Proid = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            SqlDataReader sdr = cmd.ExecuteReader();

            while(sdr.Read())
            {
                product = new Product() { ProId = Convert.ToInt32(sdr[0].ToString()), ProName = sdr[1].ToString() };
            }

            return product;

        }

    }
}
