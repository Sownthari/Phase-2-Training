<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="BookStoreApplication._404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Page Not Found</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            color: #333;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            text-align: center;
        }

        .message {
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
            max-width: 400px;
            margin: 20px;
        }

        h1 {
            font-size: 36px;
            margin: 0;
        }

        p {
            font-size: 18px;
        }

        .smiley {
            font-size: 50px;
        }

        .link {
            display: inline-block;
            margin-top: 20px;
            text-decoration: none;
            color: #007bff;
            font-weight: bold;
        }

        .link:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="message">
            <h1>404 - Page Not Found</h1>
            <p>Oops! You don't have permission to access this page. :(</p>
            <a href="Login.aspx" class="link">Go to Login</a>
            <br />
            <a href="Book.aspx" class="link">Return to Home</a>
        </div>
    </form>
</body>
</html>
