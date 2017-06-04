<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainForm.aspx.cs" Inherits="CommunicatoinLayer.mainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script src="Scripts/jquery-3.1.1.min.js"></script>
    <script src="Scripts/jquery.signalR-2.2.2.min.js"></script>
    <script src="/signalr/hubs"></script>
    <script src="serverInit.js"></script>
    <script src="authHub.js"></script>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <p>
            &nbsp;</p>
        <p>
            user name
            <asp:TextBox ID="userNameText" runat="server" style="margin-bottom: 0px"></asp:TextBox>
        </p>
        <p>
            password
            <asp:TextBox ID="passwordText" runat="server" style="margin-bottom: 5px" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            &nbsp;</p>
        <asp:Button ID="loginBtn" runat="server" Height="34px" OnClientClick="return false;" Text="Login" Width="103px"  />
    </form>
</body>
</html>
