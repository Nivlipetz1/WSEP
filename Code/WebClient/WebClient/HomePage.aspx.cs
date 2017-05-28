using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            invalidLabel.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkUserDetails(usernameField.Text, passwordField.Text))
            {
                invalidLabel.Visible = false;
                Response.Redirect("LeaderboardsPage.aspx");
            }
            else
                invalidLabel.Visible = true;
        }

        private bool checkUserDetails(string p1, string p2)
        {
            return true;
        }
    }
}