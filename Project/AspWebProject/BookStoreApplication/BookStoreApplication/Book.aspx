<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Book.aspx.cs" Inherits="BookStoreApplication.Book" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Store</title>
    <style>
        
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5; 
            color: #333; 
            margin: 0 !important;
        }

        #header {
            width: 100%;
            padding: 10px 20px;
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

        #cart, #Orders, #name {
            /*margin-left: 20px;*/
            font-size: 16px;
            font-weight: 700;
        }

        #cart a, #Orders a {
            color: white;
            text-decoration: none;
            transition: color 0.3s;
        }
                
        #searchBar {
            clear: both;
            margin: 20px;
            text-align: center;
        }

        #searchBar input[type="text"] {
            width: 50%;
            padding: 10px;
            font-size: 16px;
            border: 1px solid #ccc; 
            border-radius: 5px; 
        }

        #searchBar input[type="submit"] {
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            background-color: #4a4a4a; 
            color: white;
            border: none;
            border-radius: 5px; 
            transition: background-color 0.3s;
        }

        #searchBar input[type="submit"]:hover {
            background-color: #333; 
        }

        #filters {
            display: flex;
            justify-content: center;
            margin: 20px;
        }

        #filters select {
            padding: 10px;
            font-size: 16px;
            margin-right: 10px;
            border: 1px solid #ccc; 
            border-radius: 5px; 
        }

        #bookList {
            display: flex;
            flex-wrap: wrap;
            
            margin: 20px;
            
        }

        .bookItem {
            width: 28%;
            margin: 15px;
            padding: 10px;
            background-color: #fff; 
            border: 1px solid #ddd; 
            border-radius: 10px; 
            text-align: center;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); 
            transition: transform 0.3s, box-shadow 0.3s; 
        }

        .bookItem:hover {
            transform: scale(1.05); 
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2); 
        }

        .bookItem img {
            max-width: 100%;
            height: auto;
            border-radius: 5px; 
            margin-bottom: 10px;
        }

        .bookItem h2 {
            font-size: 18px;
            margin-bottom: 10px;
            color: #2c3e50; 
        }

        .bookItem p {
            margin-bottom: 10px;
            color: #666; 
        }

        .bookItem input[type="submit"] {
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            background-color: rgb(20, 17, 17); 
            color: white;
            border: none;
            border-radius: 5px; 
            transition: background-color 0.3s, transform 0.3s; 
        }

        .bookItem input[type="submit"]:hover {
            background-color: #4a4a4a; 
            transform: translateY(-2px); 
        }

        .bookItem input[type="submit"]:disabled {
            background-color: #ccc; 
            cursor: not-allowed;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">
            <h1>Book Store</h1>
            <div id ="nav">
                <div id="name">
                   <p>Welcome, <%= Session["UserName"] != null ? Session["UserName"].ToString() : "Guest" %>!</p>
                </div>
                <div id="cart">
                    <a href="Cart.aspx">Cart</a>
                </div>
                <div id="Orders">
                    <a href="Order.aspx">Orders</a>
                </div>
            </div>
        </div>

        <div id="searchBar">
            <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="Search books..." />
            <asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click" />
        </div>

        <div id="filters">
            <asp:DropDownList ID="DropDownListAuthors" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterBooks">
                <asp:ListItem Text="All Authors" Value="" />
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownListCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterBooks">
                <asp:ListItem Text="All Categories" Value="" />
            </asp:DropDownList>
        </div>

        <div id="bookList">
            <asp:Repeater ID="RepeaterBooks" runat="server">
                <ItemTemplate>
                    <div class="bookItem">
                        
                        <asp:Image ID="BookImage" runat="server"
                        ImageUrl='<%# Eval("BookImage") %>' Width="100px" Height="100px" />
                        
                        <h2><%# Eval("Title") %></h2>
                        <p>Author: <%# Eval("Author") %></p>
                        <p>Category: <%# Eval("Category") %></p>
                        <p>Price: Rs.<%# Eval("Price") %></p>
                        <p>Stock:<%# Eval("Stock") %></p>
                        <asp:Button ID="ButtonAddToCart" runat="server" Text="Add to Cart" 
                            OnClick="ButtonAddToCart_Click" 
                            CommandArgument='<%# Eval("BookID") %>' 
                            Enabled='<%# Convert.ToInt32(Eval("Stock")) > 0 %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
