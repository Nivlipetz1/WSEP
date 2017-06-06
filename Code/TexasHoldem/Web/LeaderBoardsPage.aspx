<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaderBoardsPage.aspx.cs" Inherits="Web.LeaderBoards" %>

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
    <form id="form1" runat="server" style="width:100%;height:auto; min-width:1000px; min-height:1000px;">
        <div>
            <asp:ImageButton src="Images/BackButton.png" OnClick="Back_Click" runat="server" style="position:absolute; top: -24px; left: -39px; height: 80px; width: 300px; margin-left: 2px;"  />
            <asp:ImageButton src="Images/HomeLable.png" OnClick="Back_Click" runat="server"  style="position:absolute; top: 11px; left: 127px; height: 38px; width: 95px;"/>         
        </div>

        <div style="text-align:center">
            <asp:Image ID="Image1" runat="server" src="Images/Leaderboards%20logo.png"/>
        </div>
         
        <asp:Table runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <div style="text-align:center">
                        <asp:Label ID="GrossProfitLabel" Text="Gross profit" style="font-size:xx-large;" runat="server"  Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Font-Names="Comic Sans MS" />
                    </div>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="........................................" />
                </asp:TableCell>
                <asp:TableCell>
                    <div style="text-align:center;">
                        <asp:Label ID="CashGainLabel" Text="Cash gain" style=" font-size:xx-large" runat="server"  Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Font-Names="Comic Sans MS"></asp:Label>
                    </div>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="........................................" />
                </asp:TableCell>
                <asp:TableCell>
                    <div style="text-align:center">
                        <asp:Label ID="GamesLabel" Text="Games" style="font-size:xx-large;" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Font-Names="Comic Sans MS"></asp:Label>
                    </div>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <div style="text-align:center"> 
                        <asp:Table style="position:inherit; z-index:2" ID="grossPorfitTable" HorizontalAlign="Center" runat="server" Font-Bold="True" Font-Names="Dubai" Font-Size="Medium" ForeColor="White" />
                    </div>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <div style="text-align:center">
                        <asp:Table ID="cashGainTable"  runat="server" Font-Bold="True" Font-Names="Dubai" Font-Size="Medium" ForeColor="White" />
                    </div>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <div style="text-align:center">
                        <asp:Table ID="gamesTable" HorizontalAlign="Center" runat="server" Font-Bold="True" Font-Names="Dubai" Font-Size="Medium" ForeColor="White"></asp:Table>
                    </div>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
     
        <div style="text-align:center; margin-top:50px;">
            <img src="Images/Statistics%20logo1.png"/>
         </div>

        <div style="text-align:center">
            <asp:Label ID="UsernameLabel" Text="Username:" style="font-size:large; width: 194px; height: 50px;" runat="server" Font-Bold="True" ForeColor="White" Font-Names="Comic Sans MS" Font-Size="Medium"></asp:Label>
            <asp:TextBox ID="userNameText" runat="server" ></asp:TextBox>
            <asp:Button OnClick="searchButton_Click" ID="searchButton"  Text="Search" runat="server" />
        </div>
        
        <div style="text-align:center; margin-top:10px">
            <asp:Label ID="WinRateLabel" Text="Win rate:" runat="server" style="font-size:large; width: 197px; height: 41px;" Font-Bold="True" ForeColor="White" Font-Names="Comic Sans MS"></asp:Label>   
            <asp:Label ID="WinRateValueLabel" Text="W" runat="server" style=" font-size:large; width: 197px; height: 41px;" Font-Bold="True" ForeColor="White" Font-Names="Comic Sans MS"></asp:Label>   
            <asp:Label ID="invalidLabel" runat="server" Font-Bold="True" ForeColor="Red" Text="Invalid username"></asp:Label>
        </div>

        <div style="text-align:center; height:60px">
            <asp:Label ID="ProfitRateLabel" Text="Profit rate:" style="font-size:large; width: 194px; height: 50px;" runat="server" Font-Bold="True" ForeColor="White" Font-Names="Comic Sans MS" Font-Size="Medium"></asp:Label>
            <asp:Label ID="ProfitRateValueLabel" Text="P" style=" font-size:large; width: 194px; height: 50px;" runat="server" Font-Bold="True" ForeColor="White" Font-Names="Comic Sans MS" Font-Size="Medium"></asp:Label>
        </div>    
</form>
    </body>
</html>