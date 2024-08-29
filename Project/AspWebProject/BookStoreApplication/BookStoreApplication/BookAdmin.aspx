<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookAdmin.aspx.cs" Inherits="BookStoreApplication.BookAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Administration</title>
    <style>
        
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5; 
            color: #333;
            margin: 0;
            
        }

        #header {
            width: 100%;
            padding: 20px;
            margin: 0 !important;
            background-color: #4a4a4a; 
            color: white;
            display: flex;
            align-items: center;
            justify-content: space-between;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); 
        }

        #header h1 {
            font-size: 24px;
            margin: 0;
        }

        #header div {
            display: flex;
            align-items: center;
            margin-right: 25px;
        }

        #Orders {
            
            font-size: 16px;
            font-weight: 700;
        }

        #Orders a {
            color: white;
            text-decoration: none;
            transition: color 0.3s;
        }

        h1 {
            text-align: center;
        }

        
        .grid-container {
            width: 80%;
            margin: 20px auto;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 50px;
            display: flex;
            flex-direction:column;
        }

        .grid-title {
            text-align: center;
            font-size: 24px;
            margin-bottom: 20px;
            color: #333;
            padding: 10px;
            border-bottom: 2px solid #ccc;
        }

        
        .form-control {
            margin-bottom: 15px;
            margin: 15px;
            padding: 12px;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 4px;
            width: 90%;
            transition: border-color 0.3s;
        }

        .form-control:focus {
            border-color: #4CAF50; 
            box-shadow: 0 0 5px rgba(76, 175, 80, 0.5);
            outline: none;
        }

        #lblBook{
            margin-left: 15px;
            color: #1a1111;
            font-weight: 700;
        }

        .btnGroup{
            display: flex;
            margin-left: 40%;

        }

        
        .btn {
            padding: 12px 20px;
            background-color: #0d0303; 
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s;
            width: 20%;
            margin: 10px;
            
        }

        .btn-danger {
            background-color: #f44336; 
        }

        .btn:hover {
            background-color: #1f1919; 
        }

        
        table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }

        th, td {
            padding: 12px;
            border: 1px solid #ccc;
            text-align: left;
        }

        th {
            background-color: #e9ecef; 
            color: #333;
        }

        
        .action-button {
            padding: 8px 12px;
            margin-right: 5px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s, transform 0.2s;
        }

        .action-button.edit {
            background-color: #007bff; 
            color: white;
        }

        .action-button.delete {
            background-color: #dc3545; 
            color: white;
        }

        .action-button:hover {
            opacity: 0.8; 
            transform: translateY(-1px);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <div id="header">
            <h1>Book Administration</h1>
            <div id ="nav">
               
                <div id="Orders">
                    <a href="OrderAdmin.aspx">Orders</a>
                </div>
            </div>
        </div>

        <div class="grid-container">
            <div class="grid-title">Add / Edit Book</div>
            <asp:HiddenField ID="HiddenFieldBookID" runat="server" />
            <asp:TextBox ID="TextBoxTitle" runat="server" CssClass="form-control" Placeholder="Title"></asp:TextBox>
            <asp:TextBox ID="TextBoxAuthor" runat="server" CssClass="form-control" Placeholder="Author"></asp:TextBox>
            <asp:TextBox ID="TextBoxISBN" runat="server" CssClass="form-control" Placeholder="ISBN"></asp:TextBox>
            <asp:TextBox ID="TextBoxPrice" runat="server" CssClass="form-control" Placeholder="Price"></asp:TextBox>
            <asp:TextBox ID="TextBoxStock" runat="server" CssClass="form-control" Placeholder="Stock"></asp:TextBox>
            <asp:Label ID="lblBook" runat="server" Text="Category" CssClass="label"></asp:Label>
            <asp:DropDownList ID="DropDownListCategories" runat="server" CssClass="form-control"></asp:DropDownList>
            <asp:FileUpload ID="FileUploadImage" runat="server" CssClass="form-control" />
            <div class="btnGroup">
                <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="btn" OnClick="ButtonSave_Click" />
                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="ButtonCancel_Click" />
            </div>    

        </div>

        <div class="grid-container">
            <div class="grid-title">Books List</div>
            <asp:GridView ID="GridViewBooks" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="BookID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Author" HeaderText="Author" />
                    <asp:BoundField DataField="Price" HeaderText="Price" />
                    <asp:BoundField DataField="Category" HeaderText="Category" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" 
                                CssClass="action-button edit" 
                                OnClick="btnEdit_Click" CommandArgument='<%# Eval("BookID") %>' />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                                CssClass="action-button delete" 
                                OnClick="btnDelete_Click" CommandArgument='<%# Eval("BookID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="grid-container">
            <div class="grid-title">Add / Edit Category</div>
            <asp:HiddenField ID="HiddenFieldCategoryID" runat="server" />
            <asp:TextBox ID="TextBoxCategoryName" runat="server" CssClass="form-control" Placeholder="Category Name"></asp:TextBox>
            <div class="btnGroup">
                <asp:Button ID="ButtonSaveCategory" runat="server" Text="Save Category" CssClass="btn" OnClick="ButtonSaveCategory_Click" />
                <asp:Button ID="ButtonCancelCategory" runat="server" Text="Cancel Category" CssClass="btn" OnClick="ButtonCancelCategory_Click" />      

            </div>
        </div>

        <div class="grid-container">
            <div class="grid-title">Categories List</div>
            <asp:GridView ID="GridViewCategories" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="CategoryID" HeaderText="Category ID" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Category Name">
                        <ItemTemplate>
                            <%# Eval("CategoryName") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxCategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnEditCategory" runat="server" Text="Edit" 
                                CssClass="action-button edit" 
                                OnClick="btnEditCategory_Click" CommandArgument='<%# Eval("CategoryID") %>' />
                            <asp:Button ID="btnDeleteCategory" runat="server" Text="Delete" 
                                CssClass="action-button delete" 
                                OnClick="btnDeleteCategory_Click" CommandArgument='<%# Eval("CategoryID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>
