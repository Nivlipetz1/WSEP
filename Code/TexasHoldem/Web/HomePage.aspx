<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Web.LoginPage" %>

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
    </style>
</head>
<body onbeforeunload="HandleBack">
    <script lang="js" type="text/javascript">
        window.history.forward();
    </script>
    <form id="form1" runat="server" style="width:100%;height:auto; min-width:1000px; min-height:1000px;">
        <div style="text-align:center; margin-top:100px">
            <img style="width:400px; height:100px;" class="auto-style1" src="Images/LOGIN_LOGO_WEB_white.png" />
        </div>
        
        <div style="text-align:center; margin-top:20px; margin-right:10px">
            <asp:Label ID="usernameLabel" runat="server" Text="Username:" Font-Bold="True" ForeColor="White"></asp:Label>
            <asp:TextBox ID="usernameField" runat="server"></asp:TextBox>
        </div>
        
        <div style="text-align:center; margin-top:5px; margin-right:10px">
            <asp:Label ID="passwordLabel" runat="server" Text="Password :" Font-Bold="True" ForeColor="White"></asp:Label>
            <asp:TextBox TextMode="Password" runat="server" ID="passwordField"></asp:TextBox>
        </div>
        
        <div style="text-align:center; margin-top:10px; margin-right:10px">
            <asp:Button ID="LoginButton" runat="server" Height="30px" OnClick="Button1_Click" Text="Login" Width="133px" />
            <asp:Label ID="invalidLabel" runat="server" Font-Bold="True" ForeColor="Red" Text="Invalid username\password "></asp:Label>
        </div>
 </form> 
</body>
</html>