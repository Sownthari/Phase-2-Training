using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADOWebApp
{
    public partial class Form1 : Form
    {
        static SqlConnection conn;

        static SqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
        }

        public static void GetConnection()
        {
            conn = new SqlConnection("data source = PTSQLTESTDB01; database = Sports_Sownthari; integrated security = true");

            conn.Open();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetConnection();
            cmd = new SqlCommand("select * from Product", conn);

            cmd.ExecuteNonQuery();

            SqlDataReader sdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(sdr);

            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            GetConnection();
            cmd = new SqlCommand("select * from Product", conn);

            cmd.ExecuteNonQuery();

            SqlDataReader sdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(sdr);

            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);

            GetConnection();
            cmd = new SqlCommand("select * from Product where ProId = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                txtName.Text = sdr[1].ToString();
            }

            if (!sdr.HasRows)
            {
                MessageBox.Show("Id not found");
            }


            //DataTable dt = new DataTable();
            //dt.Load(sdr);

            //dataGridView1.DataSource = dt;

            conn.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GetConnection();

            int id = Convert.ToInt32(txtId.Text);
            string name = txtName.Text;

            cmd = new SqlCommand("insert into Product values(@id, @name)", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Record Inserted");
                        
            conn.Close() ;
            txtId.Text = null;
            txtName.Text = null;

        }

        private void btnTotal_Click(object sender, EventArgs e)
        {
            GetConnection();

            cmd = new SqlCommand("select count(*) from Product", conn);

            string count = cmd.ExecuteScalar().ToString();

            LCount.Text = count;

            conn.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            GetConnection();

            int id = Convert.ToInt32(txtId.Text);
            string name = txtName.Text;

            cmd = new SqlCommand("update Product set ProName = @name where ProId = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Record Updated");

            conn.Close();

            txtId.Text = null;
            txtName.Text = null;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            GetConnection();

            int id = Convert.ToInt32(txtId.Text);
            
            cmd = new SqlCommand("delete Product where ProId = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
          
            cmd.ExecuteNonQuery();

            MessageBox.Show("Record Deleted");

            conn.Close();

            txtId.Text = null;
            txtName.Text = null;

        }
    }
}
