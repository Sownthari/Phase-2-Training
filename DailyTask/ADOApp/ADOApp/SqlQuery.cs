using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOApp
{
    public class SqlQuery
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

        public void Insert()
        {
            GetConnection();

            Console.WriteLine("Enter number of products to be inserted: ");
            int count = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                
                Console.WriteLine("Enter Id and Name: ");
                int id = Convert.ToInt32(Console.ReadLine());
                string name = Console.ReadLine();

                cmd = new SqlCommand("insert into Product values(@id,@name)", conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);

                cmd.ExecuteNonQuery();

            }

            Console.WriteLine($"{count} records Created");

            conn.Close();



        }

        public void Retrieve()
        {
            GetConnection();
            cmd = new SqlCommand("select * from Product", conn);

            cmd.ExecuteNonQuery();

            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Console.WriteLine(sdr[0].ToString() + " " + sdr[1].ToString());
            }

            conn.Close();
        }

        public void Update()
        {
            GetConnection();
            Console.WriteLine("Enter the product id to update: ");
            int id = Convert.ToInt32(Console.ReadLine());

            if (Search(id))
            {
                Console.WriteLine("Enter new product name: ");
                string name = Console.ReadLine();
                cmd = new SqlCommand("update Product set Name = @name where ProId = @id",conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Record updated");
            }
            else
            {
                Console.WriteLine($"No Product found with id {id}");
            }
        }

        public void Delete()
        {
            GetConnection();
            Console.WriteLine("Enter the product id to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            if (Search(id))
            {
                cmd = new SqlCommand("delete from Product where ProId = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record deleted");
            }
            else
            {
                Console.WriteLine($"No Product found with id {id}");
            }

            conn.Close();

        }

        public bool Search(int id)
        {
            GetConnection();

            cmd = new SqlCommand("select * from Product where Proid = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            SqlDataReader sdr = cmd.ExecuteReader();

            
            if (!sdr.HasRows)
            {
                conn.Close();
                return false;
            }
            conn.Close();
            return true;

        }

    }


}
