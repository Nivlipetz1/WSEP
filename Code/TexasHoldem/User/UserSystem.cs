using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    class UserSystem : Users
    {
        private user activeUser;
        private Dictionary<String, user> users;

        public UserSystem()
        {
            activeUser = null;
            users = new Dictionary<string, user>();
        }

        public user getActiveUser()
        {
            return activeUser;
        }

        public bool editAvatar(Image avatar)
        {
            if (activeUser == null)
                return false;
            activeUser.Avatar = avatar;
            return true;
        }

        public bool editPassword(string password)
        {
            if (activeUser == null)
                return false;
            activeUser.Password = password;
            return true;
        }

        public bool editUserName(string userName)
        {
            if (activeUser == null)
                return false;
            activeUser.Username = userName;
            return true;
        }

        public bool login(string userName, string password)
        {
            if (activeUser == null)
            {
                try
                {
                    activeUser = getUser(userName, password);
                }
                catch
                {
                    return false;
                }
            }
            else return false;
            return true;
        }

        public bool logout()
        {
            if (activeUser == null)
                return false;
            activeUser = null;
            return true;
        }

        public bool register(string userName, string password)
        {
            if (!users.ContainsKey(userName))
                users.Add(userName, new user(userName, password));
            else return false;
            return true;
        }

        private user getUser(string username, string password)
        {
            return users[username];
        }
    }
}
