<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BookStoreApplication.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            /*background: linear-gradient(to right, #828282, #48494B);*/
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        form {
            background: #ffffff;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 450px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }
               
        h2 {
            margin-top: 0;
            font-size: 28px;
            color: black;
            text-align: center;
            font-weight: 600;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .label {
            display: block;
            font-weight: 600;
            margin-bottom: 8px;
            color: #1c0f13;
        }

        input[type="text"],
        input[type="email"],
        input[type="password"] {
            width: 100%;
            padding: 12px;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-sizing: border-box;
            font-size: 16px;
            color: #222021;
            transition: border-color 0.3s ease, box-shadow 0.3s ease;
        }

        input[type="text"]:focus,
        input[type="email"]:focus,
        input[type="password"]:focus {
            border-color: #363636;
            box-shadow: 0 0 8px rgba(0, 114, 255, 0.2);
            outline: none;
        }

        input[type="submit"] {
            background-color: #404048;
            color: #ffffff;
            border: none;
            padding: 14px;
            font-size: 18px;
            border-radius: 8px;
            cursor: pointer;
            width: 100%;
            transition: background-color 0.3s ease, transform 0.3s ease;
        }

        input[type="submit"]:hover {
            background-color: #3a3c40;
            transform: translateY(-2px);
        }
                
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Register</h2>
            <div class="form-group">
                <asp:Label ID="lblName" runat="server" Text="Name" class ="label"></asp:Label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblUsername" runat="server" Text="Username" class ="label"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblEmail" runat="server" Text="Email" class ="label"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblPassword" runat="server" Text="Password" class ="label"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
           <div class="form-group">
                <label>Already a user?</label>
                <a href="Login.aspx">Login</a>
            </div>

            <asp:Button ID="btnSignup" runat="server" Text="Register" OnClick ="btnSignup_Click"/>
        </div>
    </form>
</body>
</html>
