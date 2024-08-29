using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookStoreApplication
{
    public partial class Login : System.Web.UI.Page
    {
        string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblError.Visible = false;
            }
            connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CustomerID, Role FROM Customers WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int userId = Convert.ToInt32(reader["CustomerID"]);
                    int role = Convert.ToInt32(reader["Role"]);

                    
                    Session["UserID"] = userId;
                    Session["Role"] = role;
                    Session["UserName"] = username;

                    
                    if (role == 1)
                    {
                        Response.Redirect("BookAdmin.aspx");
                    }
                    else if (role == 0)
                    {
                        Response.Redirect("Book.aspx");
                    }
                    
                }
                else
                {
                    lblError.Text = "Invalid Username or Password.";
                    lblError.Visible = true;
                }

                conn.Close();
            }
        }
    }
}