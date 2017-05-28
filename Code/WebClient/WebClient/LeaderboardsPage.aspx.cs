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
            WinRateLabel.Visible = false;
            ProfitRateLabel.Visible = false;
            invalidLabel.Visible = false;
            WinRateValueLabel.Visible = false;
            ProfitRateValueLabel.Visible = false;
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