<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WebClient.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            width: 805px;
        }
        body { 
            background-image: url('images/background2.jpg');
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size:cover; 
            background-position:center;
        }
        .auto-style1 {
            width: 285px;
            height: 84px;
        }
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <div style="height: 274px; width: 297px; position:absolute; top: 123px; left: 519px;">
        <asp:TextBox ID="usernameField" runat="server" style="position:absolute; top: 87px; left: 126px;"></asp:TextBox>
        <asp:TextBox TextMode="Password" runat="server" ID="passwordField" style="position:absolute; top: 127px; left: 126px;"></asp:TextBox>
        <asp:Label ID="usernameLabel" runat="server" Text="Username:" style="position:absolute; top: 88px; left: 39px;" Font-Bold="True" ForeColor="White"></asp:Label>
        <asp:Label ID="passwordLabel" runat="server" Text="Password:" style="position:absolute; top: 128px; left: 40px;" Font-Bold="True" ForeColor="White"></asp:Label>
        <asp:Button ID="LoginButton" runat="server" Height="30px" style="position:absolute; top: 174px; left: 107px; bottom: 70px;" OnClick="Button1_Click" Text="Login" Width="133px" />
        <asp:Label ID="invalidLabel" style="position:absolute; top: 226px; left: 79px;" runat="server" Font-Bold="True" ForeColor="Red" Text="Invalid username\password "></asp:Label>
        </div>
    </form>
        <img alt="" style="position:absolute; background-size:auto; top: 74px; left: 559px;" class="auto-style1" src="Images/LOGIN_LOGO_WEB_white.png" />
</body>
</html>