<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaderboardsPage.aspx.cs" Inherits="LeaderboardsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            width: 805px;
        }
        body { 
            background-image: url('images/background3.jpg');
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size:cover; 
            background-position:center;
        }
        .auto-style5 {
            width: 500px;
            height: 500px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">

    <img src="Images/Leaderboards%20logo.png" style="position:absolute; top: -13px; left: 418px;"  />

    <asp:Label ID="GrossProfitLabel" Text="Gross profit" style="position:absolute; font-size:xx-large; top: 201px; left: 940px; width: 197px; height: 41px;"  runat="server"  Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Font-Names="Comic Sans MS"></asp:Label>
    <asp:Label ID="CashGainLabel" Text="Cash gain" style="position:absolute; font-size:xx-large; top: 198px; left: 558px;"  runat="server"  Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Font-Names="Comic Sans MS"></asp:Label>
    <asp:Label ID="GamesLabel" Text="Games" style="position:absolute; font-size:xx-large; top: 200px; left: 175px; direction: ltr;" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Font-Names="Comic Sans MS"></asp:Label>

    <asp:ImageButton src="Images/BackButton.png" OnClick="Back_Click" runat="server" style="position:absolute; top: -24px; left: -39px; height: 80px; width: 300px; margin-left: 2px;"  />
    <asp:ImageButton src="Images/HomeLable.png" OnClick="Back_Click" runat="server"  style="position:absolute; top: 11px; left: 127px; height: 38px; width: 95px;"/>         
    
    <img src="Images/table.png" style="position:absolute; top: 158px; left: 370px;" class="auto-style5" />
    <img src="Images/table.png" style="position:absolute; top: 157px; left: 777px;" class="auto-style5" />
    <img src="Images/table.png" style="position:absolute; top: 158px; left: -9px;" class="auto-style5" />
    
    <img src="Images/Statistics%20logo.png" style="position:absolute; top: 622px; left: 417px;"  />;

    <asp:Label ID="WinRateLabel" Text="Win rate:" runat="server" style="position:absolute; font-size:large; top: 894px; left: 512px; width: 197px; height: 41px;" Font-Bold="True" ForeColor="White" Font-Names="Comic Sans MS"></asp:Label>   
    <asp:Label ID="ProfitRateLabel" Text="Profit rate:" style="position:absolute; font-size:large; top: 858px; left: 514px; width: 194px; height: 50px;" runat="server" Font-Bold="True" ForeColor="White" Font-Names="Comic Sans MS" Font-Size="Medium"></asp:Label>
    <asp:Label ID="UsernameLabel" Text="Username:" style="position:absolute; font-size:large; top: 804px; left: 484px; width: 194px; height: 50px;" runat="server" Font-Bold="True" ForeColor="White" Font-Names="Comic Sans MS" Font-Size="Medium"></asp:Label>
    <asp:Label ID="invalidLabel" style="position:absolute; top: 862px; left: 597px;" runat="server" Font-Bold="True" ForeColor="Red" Text="Invalid username"></asp:Label>

    <asp:Label ID="WinRateValueLabel" Text="W" runat="server" style="position:absolute; font-size:large; top: 894px; left: 627px; width: 197px; height: 41px;" Font-Bold="True" ForeColor="White" Font-Names="Comic Sans MS"></asp:Label>   
    <asp:Label ID="ProfitRateValueLabel" Text="P" style="position:absolute; font-size:large; top: 858px; left: 627px; width: 194px; height: 50px;" runat="server" Font-Bold="True" ForeColor="White" Font-Names="Comic Sans MS" Font-Size="Medium"></asp:Label>
    
    <asp:TextBox ID="userNameText" runat="server" style="position:absolute; top: 808px; left: 591px;"></asp:TextBox>
    <asp:Button OnClick="searchButton_Click" ID="searchButton" Text="Search" runat="server"  style="position:absolute; top: 808px; left: 776px;"/>
</form>
    </body>
</html>
