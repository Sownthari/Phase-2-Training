using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace BookStoreApplication
{
    public partial class Order : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                
                Response.Redirect("Login.aspx");
            }
            else
            {
                
                DisplayOrders(Convert.ToInt32(Session["UserID"]));
            }
        }

        private void DisplayOrders(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Get the orders placed by the user
                string ordersQuery = @"
                    SELECT o.OrderID, o.OrderDate, o.OrderStatus, o.TotalAmount, 
                           od.BookID, b.Title, od.Quantity, od.Price 
                    FROM Orders o
                    JOIN OrderDetails od ON o.OrderID = od.OrderID
                    JOIN Books b ON od.BookID = b.BookID
                    WHERE o.CustomerID = @CustomerID
                    ORDER BY o.OrderDate DESC";

                SqlCommand cmd = new SqlCommand(ordersQuery, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                SqlDataReader reader = cmd.ExecuteReader();

                // Create a DataTable to hold the order details for binding to a grid
                DataTable ordersTable = new DataTable();
                ordersTable.Columns.Add("OrderID");
                ordersTable.Columns.Add("OrderDate");
                ordersTable.Columns.Add("OrderStatus");
                ordersTable.Columns.Add("TotalAmount");
                ordersTable.Columns.Add("BookID");
                ordersTable.Columns.Add("Title");
                ordersTable.Columns.Add("Quantity");
                ordersTable.Columns.Add("Price");

                while (reader.Read())
                {
                    // Add rows to the DataTable for each order detail
                    ordersTable.Rows.Add(
                        reader["OrderID"],
                        reader["OrderDate"],
                        reader["OrderStatus"],
                        reader["TotalAmount"],
                        reader["BookID"],
                        reader["Title"],
                        reader["Quantity"],
                        reader["Price"]);
                }

                reader.Close();

                // Bind the DataTable to a GridView or similar control in the ASPX page
                OrdersGridView.DataSource = ordersTable;
                OrdersGridView.DataBind();
            }
        }
    }
}
