using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace BookStoreApplication
{
    public partial class BookAdmin : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BmsConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckUserRole();
                BindCategories();
                BindCategoriesToDropdown();
                BindBooks();
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

        private void BindCategories()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID, CategoryName FROM Categories";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewCategories.DataSource = dt;
                GridViewCategories.DataBind();
                                
            }
        }

        private void BindCategoriesToDropdown()
        {
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

                
            }
        }

        private void BindBooks()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT b.BookID, b.Title, b.Author, b.Price, c.CategoryName AS Category, b.BookImage, b.ISBN 
                    FROM Books b
                    INNER JOIN Categories c ON b.CategoryID = c.CategoryID";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewBooks.DataSource = dt;
                GridViewBooks.DataBind();
            }
        }



        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            int bookId = string.IsNullOrEmpty(HiddenFieldBookID.Value) ? 0 : Convert.ToInt32(HiddenFieldBookID.Value);
            string title = TextBoxTitle.Text.Trim();
            string author = TextBoxAuthor.Text.Trim();
            decimal price = Convert.ToDecimal(TextBoxPrice.Text.Trim());
            int categoryId = Convert.ToInt32(DropDownListCategories.SelectedValue);
            string isbn = TextBoxISBN.Text.Trim();
            
            int stock = Convert.ToInt32(TextBoxStock.Text.Trim());

            string uploadedImagePath = string.Empty;

            if (FileUploadImage.HasFile)
            {
                
                string fileName = Path.GetFileName(FileUploadImage.PostedFile.FileName);
                uploadedImagePath = "/Images/" + fileName;
                string physicalPath = Server.MapPath(uploadedImagePath);
                FileUploadImage.SaveAs(physicalPath);
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = bookId == 0 ?
                    "INSERT INTO Books (Title, Author, Price, CategoryID, BookImage, ISBN, Stock) VALUES (@Title, @Author, @Price, @CategoryID, @Image, @ISBN, @stock)" :
                    "UPDATE Books SET Title = @Title, Author = @Author, Price = @Price, CategoryID = @CategoryID, BookImage = @Image, ISBN = @ISBN, Stock = @Stock WHERE BookID = @BookID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@Image", uploadedImagePath);
                cmd.Parameters.AddWithValue("@ISBN", isbn);
                cmd.Parameters.AddWithValue("@Stock", stock);

                if (bookId > 0)
                {
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            ClearBookFields();
            BindBooks();
        }
                              
        private void LoadBookDetails(int bookId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                string query = "SELECT Title, Author, ISBN, Price, Stock, CategoryID FROM Books WHERE BookID = @BookID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookID", bookId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    
                    HiddenFieldBookID.Value = bookId.ToString();

                    
                    TextBoxTitle.Text = reader["Title"].ToString();
                    TextBoxAuthor.Text = reader["Author"].ToString();
                    TextBoxISBN.Text = reader["ISBN"].ToString();
                    TextBoxPrice.Text = reader["Price"].ToString();
                    TextBoxStock.Text = reader["Stock"].ToString();


                    
                    int categoryId = Convert.ToInt32(reader["CategoryID"]);
                    DropDownListCategories.SelectedValue = categoryId.ToString();

                    
                }
            }
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            
            Button btnEdit = (Button)sender;
            int bookId = Convert.ToInt32(btnEdit.CommandArgument);

            
            LoadBookDetails(bookId);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
            Button btnDelete = (Button)sender;
            int bookId = Convert.ToInt32(btnDelete.CommandArgument);

            
            DeleteBook(bookId);
            BindBooks();
        }

        private void UpdateBook(int bookId, string title, string author, string ISBN, decimal price, string category)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Books SET Title = @Title, Author = @Author, ISBN = @ISBN, Price = @Price, Category = @Category WHERE BookID = @BookID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookID", bookId);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@ISBN", ISBN);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteBook(int bookId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Books WHERE BookID = @BookID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookID", bookId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private int GetCategoryId(string categoryName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID FROM Categories WHERE CategoryName = @CategoryName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
                
        private void ClearBookFields()
        {
            HiddenFieldBookID.Value = string.Empty;
            TextBoxTitle.Text = string.Empty;
            TextBoxAuthor.Text = string.Empty;
            TextBoxPrice.Text = string.Empty;
            TextBoxISBN.Text = string.Empty;
            TextBoxStock.Text = string.Empty;
            DropDownListCategories.SelectedIndex = 0;
        }

        protected void ButtonSaveCategory_Click(object sender, EventArgs e)
        {
            if (int.TryParse(HiddenFieldCategoryID.Value, out int categoryId) && categoryId > 0)
            {
                UpdateCategory(categoryId, TextBoxCategoryName.Text);
            }
            else
            {
                AddCategory(TextBoxCategoryName.Text);
            }
            BindCategories();
            BindCategoriesToDropdown();
        }

        private void AddCategory(string categoryName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            ClearCategoryFields();
        }

        private void UpdateCategory(int categoryId, string categoryName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            ClearCategoryFields();
        }

        protected void btnEditCategory_Click(object sender, EventArgs e)
        {

            Button btnEditCategory = (Button)sender;
            int categoryId = Convert.ToInt32(btnEditCategory.CommandArgument);


            LoadCategoryDetails(categoryId);
        }

        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {

            Button btnDeleteCategory = (Button)sender;
            int categoryId = Convert.ToInt32(btnDeleteCategory.CommandArgument);


            DeleteCategory(categoryId);
            BindCategories();
        }

        private void LoadCategoryDetails(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string query = "SELECT CategoryID, CategoryName from Categories WHERE CategoryID = @CategoryID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    HiddenFieldCategoryID.Value = reader["CategoryID"].ToString();
                    TextBoxCategoryName.Text = reader["CategoryName"].ToString();
                                        
                }
            }
        }


        

        private void DeleteCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Categories WHERE CategoryID = @CategoryID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            BindCategories();
            BindCategoriesToDropdown();
        }

        private void ClearCategoryFields()
        {
            HiddenFieldCategoryID.Value = string.Empty;
            TextBoxCategoryName.Text = string.Empty;
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            ClearBookFields();
        }

        protected void ButtonCancelCategory_Click(object sender, EventArgs e)
        {
            ClearCategoryFields();
        }
    }
}
