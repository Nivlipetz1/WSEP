﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameUtilities;

namespace TexasHoldemSystem
{
    interface System
    {
        bool register(string userName,string password);
        bool login(string userName, string password);
        bool logout(UserProfile u);
        bool editUserName(string userName,UserProfile u);
        bool editPassword(string password,UserProfile u);
        bool editAvatar(Image avatar,UserProfile u);
        bool isConnected(string username);
        UserProfile getUser(string username);
    }
}
