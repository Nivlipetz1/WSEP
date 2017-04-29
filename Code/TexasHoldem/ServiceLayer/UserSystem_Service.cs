using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Services;
using GameUtilities;
using GameSystem;
using System.Text.RegularExpressions;

namespace ServiceLayer
{
    public class UserSystem_Service : SystemAPI
    {
        private SystemAPI system;
        public UserSystem_Service()
        {
            system = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();
        }

        public bool editAvatar(Image avatar, UserProfile u)
        {
            return system.editAvatar(avatar, u);
        }

        public bool editPassword(string password, UserProfile u)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            return system.editPassword(password, u);
        }

        public bool editUserName(string userName, UserProfile u)
        {
            if(string.IsNullOrWhiteSpace(userName))
                return false;
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (!r.IsMatch(userName))
            {
                return false;
            }
            r = new Regex("^[0-9]*$");
            if (r.IsMatch(""+userName[0]))
                return false;
            return system.editUserName(userName, u);
        }

        public UserProfile getUser(string username)
        {
            return system.getUser(username);
        }

        public UserProfile getUser(string username, string password)
        {
            return system.getUser(username, password);
        }

        public bool isConnected(string username)
        {
            return system.isConnected(username);
        }

        public bool login(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return false;
            return system.login(userName, password);
        }

        public bool logout(UserProfile u)
        {
            return system.logout(u);
        }

        public bool register(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (!r.IsMatch(userName))
            {
                return false;
            }
            r = new Regex("^[0-9]*$");
            if (r.IsMatch("" + userName[0]))
                return false;
            if (string.IsNullOrWhiteSpace(password))
                return false;
            return system.register(userName, password);
        }
    }
}
