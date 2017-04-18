using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using User;

namespace TexasHoldemSystem
{
    interface System
    {
        bool register(string userName,string password);
        bool login(string userName, string password);
        bool logout(user u);
        bool editUserName(string userName,user u);
        bool editPassword(string password,user u);
        bool editAvatar(Image avatar,user u);
        bool isConnected(string username);
        user getUser(string username);
    }
}
