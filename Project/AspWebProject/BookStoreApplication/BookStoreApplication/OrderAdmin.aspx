<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderAdmin.aspx.cs" Inherits="BookStoreApplication.OrderAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Orders</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            color: #333;
        }

        h1 {
            text-align: center;
        }

        #orderContainer {
            width: 80%;
            margin: 20px auto;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }

        th, td {
            padding: 10px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        th {
            background-color: #f2f2f2;
        }

        .status-dropdown {
            padding: 5px;
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        .update-button {
            padding: 5px 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .update-button:hover {
            background-color: #45a049;
        }

        .order-details {
            margin-top: 10px;
            padding: 10px;
            background-color: #f9f9f9;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .order-details-table {
            width: 100%;
            margin-top: 10px;
            border-collapse: collapse;
        }

        .order-details-table th, .order-details-table td {
            padding: 8px;
            border-bottom: 1px solid #ddd;
        }

        .order-details-table th {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">
            <h1>Manage Orders</h1>
        </div>
        
        <div id="orderContainer">
            <asp:GridView ID="OrdersGridView" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowDataBound="OrdersGridView_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                    <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownListStatus" runat="server" CssClass="status-dropdown">
                                <asp:ListItem Text="Pending" Value="Pending" />
                                <asp:ListItem Text="Shipped" Value="Shipped" />
                                <asp:ListItem Text="Delivered" Value="Delivered" />
                                <asp:ListItem Text="Cancelled" Value="Cancelled" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Details">
                        <ItemTemplate>
                            <div class="order-details">
                                <asp:GridView ID="OrderDetailsGridView" runat="server" AutoGenerateColumns="False" CssClass="order-details-table">
                                    <Columns>
                                        <asp:BoundField DataField="BookTitle" HeaderText="Book Title" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" 
                                OnClick="ButtonUpdate_Click" 
                                CommandArgument='<%# Eval("OrderID") %>' 
                                CssClass="update-button" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
