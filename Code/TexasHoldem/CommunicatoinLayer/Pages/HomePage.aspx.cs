using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunicatoinLayer
{
    public partial class LoginPage : System.Web.UI.Page
    {
        public static bool loggedIn = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            loggedIn = false;
            invalidLabel.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (checkUserDetails(usernameField.Text, passwordField.Text))
            {
                loggedIn = true;
                invalidLabel.Visible = false;
                //How to call js function
                //ScriptManager.RegisterStartupScript(this, GetType(), "key", "myFunc()", true); 
                Response.Redirect("LeaderboardsPage.aspx");

            }
            else
                invalidLabel.Visible = true;
        }

        private bool checkUserDetails(string userName, string password)
        {
            SystemService userService = new SystemService();
            return userService.checkUserDetails(userName, Encryption.CalculateMD5Hash(password));
        }

    }
}