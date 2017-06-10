using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunicatoinLayer
{
    public partial class LeaderBoardsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!LoginPage.loggedIn)
                Response.Redirect("HomePage.aspx");
            WinRateLabel.Visible = false;
            ProfitRateLabel.Visible = false;
            invalidLabel.Visible = false;
            WinRateValueLabel.Visible = false;
            ProfitRateValueLabel.Visible = false;

            loadTables();

        }

        private void loadTables()
        {
            loadTable(cashGainTable.ID);
            loadTable(grossPorfitTable.ID);
            loadTable(gamesTable.ID);
        }

        private void loadTable(string tableID)
        {
            Table table = (Table)FindControl(tableID);
            List<Tuple<string, int>> games = getGamesRecords(getPropertyByTable(table.ID));
            for (int i = 0; i < 20; i++)
            {
                TableRow newRow = new TableRow();
                TableCell indexCell = new TableCell();
                TableCell firstCell = new TableCell();
                TableCell secondCell = new TableCell();
                indexCell.Text = "" + (i + 1);
                firstCell.Text = (i < games.Count) ? games[i].Item1 : "";
                secondCell.Text = (i < games.Count) ?  games[i].Item2.ToString() : "";
                newRow.Cells.Add(indexCell);
                newRow.Cells.Add(new TableCell());
                newRow.Cells.Add(new TableCell());
                newRow.Cells.Add(new TableCell());
                newRow.Cells.Add(new TableCell());
                newRow.Cells.Add(firstCell);
                newRow.Cells.Add(new TableCell());
                newRow.Cells.Add(new TableCell());
                newRow.Cells.Add(new TableCell());
                newRow.Cells.Add(new TableCell());
                newRow.Cells.Add(secondCell);
                newRow.Font.Bold = true;
                table.Rows.Add(newRow);
            }

            table.Rows[0].ForeColor = System.Drawing.Color.Gold;
            table.Rows[1].ForeColor = System.Drawing.Color.Silver;
            table.Rows[2].ForeColor = System.Drawing.Color.SandyBrown;
        }

        private string getPropertyByTable(string tableId)
        {
            switch(tableId)
            {
                case "cashGainTable": return "cashGain";
                case "grossPorfitTable": return "totalGrossProfit";
                case "gamesTable": return "gamesPlayed";
                default: return "";
            }
        }

        private List<Tuple<string, int>> getGamesRecords(string name)
        {
            SystemService userService = new SystemService();
            return userService.getTop20(name);
        }


        protected void Back_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            if (userExist(userNameText.Text))
            {
                double cashGainPerGameAverage = getCashGainPerGameAverage(userNameText.Text);
                double grossProfitAverage = getGrossProfitAverage(userNameText.Text);
                WinRateLabel.Visible = true;
                ProfitRateLabel.Visible = true;
                WinRateValueLabel.Visible = true;
                ProfitRateValueLabel.Visible = true;
                WinRateValueLabel.Text = cashGainPerGameAverage.ToString();
                ProfitRateValueLabel.Text = grossProfitAverage.ToString();
                invalidLabel.Visible = false;
            }
            else
            {
                WinRateLabel.Visible = false;
                ProfitRateLabel.Visible = false;
                invalidLabel.Visible = true;
                WinRateValueLabel.Visible = false;
                ProfitRateValueLabel.Visible = false;
            }

        }

        private bool userExist(string name)
        {
            SystemService userService = new SystemService();
            return userService.isUserExist(name);
        }

        private double getCashGainPerGameAverage(string name)
        {
            SystemService userService = new SystemService();
            return userService.getCashGain(name);
        }

        private double getGrossProfitAverage(string name)
        {
            SystemService userService = new SystemService();
            return userService.getGrossProfit(name);
        }
    }
}