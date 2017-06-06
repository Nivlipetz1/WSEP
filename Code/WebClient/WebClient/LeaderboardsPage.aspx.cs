using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class LeaderBoards : System.Web.UI.Page
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
            List<Pair<string, int>> games = getGamesRecords();
            for (int i = 0; i < 20; i++)
            {
                TableRow newRow = new TableRow();
                TableCell indexCell = new TableCell();
                TableCell firstCell = new TableCell();
                TableCell secondCell = new TableCell();
                indexCell.Text = "" + (i + 1);
                firstCell.Text = games[i].getUsername();
                secondCell.Text = games[i].getValue().ToString();
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

        private List<Pair<string, int>> getGamesRecords()
        {
            List<Pair<string, int>> list = new List<Pair<string,int>>();
            for (int i = 0; i < 20; i++)
                list.Add(new Pair<string,int>("Koren", 2222220 - i));
            return list;
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

        private bool userExist(string p)
        {
            return true;
        }

        private double getCashGainPerGameAverage(string p)
        {
            return 0;
        }

        private double getGrossProfitAverage(string p)
        {
            return 0;
        }
    }
}