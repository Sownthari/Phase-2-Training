using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace BookStoreApplication
{
    public partial class OrderAdmin : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckUserRole();
                BindOrdersGrid();
            }
        }

        private void CheckUserRole()
        {

            int userRoleId = Convert.ToInt32(Session["Role"]);

            if (userRoleId != 1)
            {

                Response.Redirect("404.aspx");
            }
        }

        private void BindOrdersGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
            SELECT o.OrderID, o.OrderDate, c.CustomerName, o.TotalAmount, o.OrderStatus 
            FROM Orders o
            INNER JOIN Customers c ON o.CustomerID = c.CustomerID", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                OrdersGridView.DataSource = dt;
                OrdersGridView.DataBind();
            }
        }

        
        private void BindOrderDetailsGrid(GridView gridView, int orderId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
            SELECT od.Quantity, b.Title AS BookTitle, od.Price 
            FROM OrderDetails od
            INNER JOIN Books b ON od.BookID = b.BookID
            WHERE od.OrderID = @OrderID", conn);

                da.SelectCommand.Parameters.AddWithValue("@OrderID", orderId);

                DataTable dt = new DataTable();
                da.Fill(dt);
                gridView.DataSource = dt;
                gridView.DataBind();
            }
        }

        protected void OrdersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int orderId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "OrderID"));
                BindOrderDetailsGrid((GridView)e.Row.FindControl("OrderDetailsGridView"), orderId);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "OrderStatus").ToString();
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("DropDownListStatus");
                if (ddlStatus != null)
                {
                    ddlStatus.SelectedValue = status;
                }
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string orderId = btn.CommandArgument;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            DropDownList ddlStatus = (DropDownList)row.FindControl("DropDownListStatus");
            string selectedStatus = ddlStatus.SelectedValue;
            UpdateOrderStatus(orderId, selectedStatus);
            BindOrdersGrid();
        }

        private void UpdateOrderStatus(string orderId, string status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Orders SET OrderStatus = @Status WHERE OrderID = @OrderID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
