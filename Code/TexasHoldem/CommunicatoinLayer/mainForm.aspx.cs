using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommunicatoinLayer
{
    public partial class mainForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /*protected void loginBtn_Click(object sender, EventArgs e)
        {
            string userName = userNameText.Text;
            string password = passwordText.Text;
            string func = "login("+userName+","+password+")";
            ScriptManager.RegisterStartupScript(this , GetType(), "LoginFunction", "login()", true);
        }*/
    }
}