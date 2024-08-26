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
        public void Create()
        {
            SqlConnection conn = new SqlConnection("data source = PTSQLTESTDB01; database = Sports_Sownthari; integrated security = true");

            conn.Open();

            SqlCommand cmd = new SqlCommand("create table Product(ProId int primary key, ProName varchar(50))", conn);

            cmd.ExecuteNonQuery();

            conn.Close();


        }

        public void Insert()
        {
            SqlConnection conn = new SqlConnection("data source = PTSQLTESTDB01; database = Sports_Sownthari; integrated security = true");

            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into Product values (1,'Laptop')", conn);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Table created successfully");

            conn.Close();

        }

        public void Retrieve()
        {
            SqlConnection conn = new SqlConnection("data source = PTSQLTESTDB01; database = Sports_Sownthari; integrated security = true");

            conn.Open();

            SqlCommand cmd = new SqlCommand("select * from Product", conn);

            cmd.ExecuteNonQuery();

            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Console.WriteLine(sdr[0].ToString() + " " + sdr[1].ToString());
            }

            conn.Close();
        }

    }
}
