using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace BookStoreApplication
{
    public partial class Cart : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCartItems();
            }
        }

        private void BindCartItems()
        {
            if (Session["UserID"] == null)
            {
                
                Response.Redirect("Login.aspx");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT ci.CartItemID, b.Title, ci.Quantity, ci.Price, 
                           (ci.Quantity * ci.Price) AS TotalPrice 
                    FROM CartItems ci
                    INNER JOIN Books b ON ci.BookID = b.BookID
                    WHERE ci.CustomerID = @CustomerID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", Session["UserID"]); 

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                RepeaterCartItems.DataSource = dt;
                RepeaterCartItems.DataBind();

                if (dt.Rows.Count == 0)
                {
                    ButtonOrderNow.Enabled = false;
                }
                else
                {
                    ButtonOrderNow.Enabled = true;
                }

            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int cartItemId = Convert.ToInt32(btn.CommandArgument);

            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            TextBox quantityTextBox = (TextBox)item.FindControl("TextBoxQuantity");
            int quantity = Convert.ToInt32(quantityTextBox.Text);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE CartItems SET Quantity = @Quantity WHERE CartItemID = @CartItemID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@CartItemID", cartItemId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            BindCartItems(); 
        }

        protected void ButtonRemove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int cartItemId = Convert.ToInt32(btn.CommandArgument);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM CartItems WHERE CartItemID = @CartItemID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CartItemID", cartItemId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            BindCartItems(); 
        }

        protected void ButtonOrderNow_Click(object sender, EventArgs e)
        {
            int customerId = Convert.ToInt32(Session["UserID"]);
            decimal totalAmount = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                
                string insertOrderQuery = "INSERT INTO Orders (CustomerID, OrderDate, OrderStatus, TotalAmount) " +
                                          "OUTPUT INSERTED.OrderID " +
                                          "VALUES (@CustomerID, @OrderDate, @OrderStatus, @TotalAmount)";
                SqlCommand insertOrderCmd = new SqlCommand(insertOrderQuery, conn);
                insertOrderCmd.Parameters.AddWithValue("@CustomerID", customerId);
                insertOrderCmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                insertOrderCmd.Parameters.AddWithValue("@OrderStatus", "Pending");
                insertOrderCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);

                int orderId = (int)insertOrderCmd.ExecuteScalar();

                
                List<Tuple<int, int>> cartItems = new List<Tuple<int, int>>();

                
                string selectQuery = "SELECT BookID, Quantity FROM CartItems WHERE CustomerID = @CustomerID";
                SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                selectCmd.Parameters.AddWithValue("@CustomerID", customerId);
                SqlDataReader reader = selectCmd.ExecuteReader();

                while (reader.Read())
                {
                    int bookId = Convert.ToInt32(reader["BookID"]);
                    int quantity = Convert.ToInt32(reader["Quantity"]);
                    cartItems.Add(new Tuple<int, int>(bookId, quantity));
                }

                reader.Close(); 

                
                foreach (var item in cartItems)
                {
                    int bookId = item.Item1;
                    int quantity = item.Item2;

                    
                    string priceQuery = "SELECT Price FROM Books WHERE BookID = @BookID";
                    SqlCommand priceCmd = new SqlCommand(priceQuery, conn);
                    priceCmd.Parameters.AddWithValue("@BookID", bookId);
                    decimal price = (decimal)priceCmd.ExecuteScalar();

                    
                    totalAmount += price * quantity;

                    
                    string insertOrderDetailsQuery = "INSERT INTO OrderDetails (OrderID, BookID, Quantity, Price) " +
                                                     "VALUES (@OrderID, @BookID, @Quantity, @Price)";
                    SqlCommand insertOrderDetailsCmd = new SqlCommand(insertOrderDetailsQuery, conn);
                    insertOrderDetailsCmd.Parameters.AddWithValue("@OrderID", orderId);
                    insertOrderDetailsCmd.Parameters.AddWithValue("@BookID", bookId);
                    insertOrderDetailsCmd.Parameters.AddWithValue("@Quantity", quantity);
                    insertOrderDetailsCmd.Parameters.AddWithValue("@Price", price);
                    insertOrderDetailsCmd.ExecuteNonQuery();

                    
                    string updateQuery = "UPDATE Books SET Stock = Stock - @Quantity WHERE BookID = @BookID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@Quantity", quantity);
                    updateCmd.Parameters.AddWithValue("@BookID", bookId);
                    updateCmd.ExecuteNonQuery();
                }

                
                string updateTotalQuery = "UPDATE Orders SET TotalAmount = @TotalAmount WHERE OrderID = @OrderID";
                SqlCommand updateTotalCmd = new SqlCommand(updateTotalQuery, conn);
                updateTotalCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                updateTotalCmd.Parameters.AddWithValue("@OrderID", orderId);
                updateTotalCmd.ExecuteNonQuery();

                
                string deleteQuery = "DELETE FROM CartItems WHERE CustomerID = @CustomerID";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@CustomerID", customerId);
                deleteCmd.ExecuteNonQuery();
            }

            BindCartItems();

            string script = "alert('Order placed successfully!');";
            ClientScript.RegisterStartupScript(this.GetType(), "OrderSuccessMessage", script, true);


        }


    }
}
