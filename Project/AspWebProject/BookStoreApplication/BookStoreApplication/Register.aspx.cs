using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookStoreApplication
{
    public partial class Register : System.Web.UI.Page
    {
        string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;
            int role = 0;
                        
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Customers (CustomerName, Username, Password, Email, Role) VALUES (@Name, @Username, @Password, @Email, @role)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@role",role);

                conn.Open();
                cmd.ExecuteNonQuery();

                string script = "alert('Registered successfully!');";
                ClientScript.RegisterStartupScript(this.GetType(), "RegisterSuccessMessage", script, true);

                Response.Redirect("Login.aspx");

                conn.Close();

            }

        }

        
    }
}