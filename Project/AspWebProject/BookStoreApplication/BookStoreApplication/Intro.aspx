<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Intro.aspx.cs" Inherits="BookStoreApplication.Intro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to Book Store</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            color: #333;
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            height: 100vh;
            margin: 0;
        }

        .gif-container {
            margin-bottom: 20px;
            margin-left: 10%;
        }

        .gif-container img {
            max-width: 300px;
            border-radius: 10px;
        }

        .welcome-message {
            text-align: center;
        }

        .welcome-message h1 {
            font-size: 36px;
            margin-bottom: 20px;
        }

        .welcome-message a {
            display: inline-block;
            margin: 10px 20px;
            padding: 10px 20px;
            color: white;
            background-color: #120f0fff;
            text-decoration: none;
            border-radius: 5px;
            transition: background-color 0.3s ease;
        }

        .welcome-message a:hover {
            background-color: #333;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="gif-container">
            <img src="https://ugokawaii.com/wp-content/uploads/2023/03/reading.gif" alt="Welcome GIF" />
        </div>
        <div class="welcome-message">
            <h1>Welcome to Book Store</h1>
            <a href="Book.aspx">Explore</a>
            <a href="Register.aspx">Register</a>
        </div>
    </form>
</body>
</html>

