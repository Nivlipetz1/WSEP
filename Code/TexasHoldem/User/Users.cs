using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace User
{
    interface Users
    {
        bool register(string userName,string password);
        bool login(string userName, string password);
        bool logout(user u);
        bool editUserName(string userName,user u);
        bool editPassword(string password,user u);
        bool editAvatar(Image avatar,user u);
    }
}
