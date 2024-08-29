<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="BookStoreApplication.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cart</title>
    <style>
        
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 20px;
        }

        #cartContainer {
            max-width: 800px;
            margin: 20px auto;
            background: white;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }

        h2 {
            color: #333;
            text-align: center;
            margin-bottom: 20px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        th, td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        th {
            background-color: #1c0f13;
            color: white;
        }

        #cartContainer input[type="number"] {
            width: 60px;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .cartButtons {
            margin-top: 20px;
            text-align: right;
        }

        .cartButtons input[type="submit"] {
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            transition: background-color 0.3s;
        }

        .cartButtons input[type="submit"] {
            background-color: #45a049;
        }

        .cartButtons input[type="submit"]:disabled {
            background-color: #ccc; 
            cursor: not-allowed; 
            color: #999; 
        }

        
        @media (max-width: 600px) {
            #cartContainer {
                width: 95%;
            }

            #cartContainer input[type="number"] {
                width: 50px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="cartContainer">
            <h2>Your Cart</h2>
            <asp:Repeater ID="RepeaterCartItems" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th>Title</th>
                            <th>Quantity</th>
                            <th>Price</th>
                            <th>Total</th>
                            <th>Actions</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Title") %></td>
                        <td>
                            <asp:TextBox ID="TextBoxQuantity" runat="server" Text='<%# Eval("Quantity") %>' Width="50px"></asp:TextBox>
                        </td>
                        <td>$<%# Eval("Price") %></td>
                        <td>$<%# Eval("TotalPrice") %></td>
                        <td>
                            <asp:Button ID="ButtonUpdate" runat="server" Text="Update" 
                                OnClick="ButtonUpdate_Click" 
                                CommandArgument='<%# Eval("CartItemID") %>' />

                            <asp:Button ID="ButtonRemove" runat="server" Text="Remove" 
                                OnClick="ButtonRemove_Click" 
                                CommandArgument='<%# Eval("CartItemID") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <div class="cartButtons">
                <asp:Button ID="ButtonOrderNow" runat="server" Text="Order Now" OnClick="ButtonOrderNow_Click" />
            </div>

        </div>
    </form>
</body>
</html>
