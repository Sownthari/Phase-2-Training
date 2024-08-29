using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookStoreApplication
{
    public partial class Book : System.Web.UI.Page
    {
        string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;
            if (!IsPostBack)
            {
                BindBooks();
                BindAuthors();
                BindCategories();
            }

        }

        private void BindAuthors()
        {
            connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT Author FROM Books";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DropDownListAuthors.DataSource = dt;
                DropDownListAuthors.DataTextField = "Author";
                DropDownListAuthors.DataValueField = "Author";
                DropDownListAuthors.DataBind();
                DropDownListAuthors.Items.Insert(0, new ListItem("All Authors", ""));
            }
        }

        private void BindCategories()
        {
            connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID, CategoryName FROM Categories";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                DropDownListCategories.DataSource = reader;
                DropDownListCategories.DataTextField = "CategoryName";
                DropDownListCategories.DataValueField = "CategoryID";
                DropDownListCategories.DataBind();


                DropDownListCategories.Items.Insert(0, new ListItem("All Categories", ""));
            }
        }

        private void BindBooks()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;
            string query = @"
            SELECT b.BookID, b.Title, b.Author, c.CategoryName AS Category, b.Price, b.BookImage, b.Stock 
            FROM Books b
            INNER JOIN Categories c ON b.CategoryID = c.CategoryID
            WHERE 1=1";

            if (!string.IsNullOrEmpty(TextBoxSearch.Text))
            {
                query += " AND (b.Title LIKE @SearchText OR b.Author LIKE @SearchText)";
            }

            if (!string.IsNullOrEmpty(DropDownListAuthors.SelectedValue))
            {
                query += " AND b.Author = @Author";
            }

            if (!string.IsNullOrEmpty(DropDownListCategories.SelectedValue))
            {
                query += " AND c.CategoryID = @CategoryID";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrEmpty(TextBoxSearch.Text))
                {
                    cmd.Parameters.AddWithValue("@SearchText", "%" + TextBoxSearch.Text + "%");
                }

                if (!string.IsNullOrEmpty(DropDownListAuthors.SelectedValue))
                {
                    cmd.Parameters.AddWithValue("@Author", DropDownListAuthors.SelectedValue);
                }

                if (!string.IsNullOrEmpty(DropDownListCategories.SelectedValue))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", DropDownListCategories.SelectedValue);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                RepeaterBooks.DataSource = dt;
                RepeaterBooks.DataBind();
            }
        }


        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindBooks();
        }

        protected void FilterBooks(object sender, EventArgs e)
        {
            BindBooks();
        }

        //public string GetImageUrl(object imageData)
        //{
        //    if (imageData != DBNull.Value)
        //    {
        //        byte[] bytes = (byte[])imageData;
        //        string base64String = Convert.ToBase64String(bytes);
        //        return "data:image/jpeg;base64," + base64String;
        //    }
        //    return string.Empty;
        //}
        protected void ButtonAddToCart_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int bookId = Convert.ToInt32(button.CommandArgument);
            int quantity = 1;

            
            if (Session["UserID"] != null)
            {
                int customerId = Convert.ToInt32(Session["UserID"]);
                AddToCart(customerId, bookId, quantity);
                Response.Redirect("Cart.aspx");
            }
            else
            {
               
                Response.Redirect("Login.aspx");
            }
        }

        private void AddToCart(int customerId, int bookId, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                string checkQuery = "SELECT COUNT(1) FROM CartItems WHERE CustomerID = @CustomerID AND BookID = @BookID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@CustomerID", customerId);
                checkCmd.Parameters.AddWithValue("@BookID", bookId);

                conn.Open();
                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (exists > 0)
                {
                    
                    string updateQuery = "UPDATE CartItems SET Quantity += @Quantity WHERE CustomerID = @CustomerID AND BookID = @BookID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@Quantity", quantity);
                    updateCmd.Parameters.AddWithValue("@CustomerID", customerId);
                    updateCmd.Parameters.AddWithValue("@BookID", bookId);
                    updateCmd.ExecuteNonQuery();
                }
                else
                {
                    
                    string insertQuery = "INSERT INTO CartItems (CustomerID, BookID, Quantity, Price) SELECT @CustomerID, @BookID, @Quantity, Price FROM Books WHERE BookID = @BookID";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@CustomerID", customerId);
                    insertCmd.Parameters.AddWithValue("@BookID", bookId);
                    insertCmd.Parameters.AddWithValue("@Quantity", quantity);
                    insertCmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}