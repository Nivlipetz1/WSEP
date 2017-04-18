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
        public class userSystemFactory
        {
            private static UserSystem instance = null;
            public  static UserSystem getInstance()
            {
                if (instance == null)
                    return new UserSystem();
                return instance;
            }
        }
        private Dictionary<String, user> activeUsers;
        private Dictionary<String, user> users;
        private UserSystem()
        {
            activeUsers = new Dictionary<string, user>();
            users = new Dictionary<string, user>();
        }

        public bool login(string userName, string password)
        {
            if (!activeUsers.ContainsKey(userName))
            {
                try
                {
                    activeUsers.Add(userName,getUser(userName, password));
                }
                catch
                {
                    return false;
                }
            }
            else return false;
            return true;
        }


        public bool register(string userName, string password)
        {
            if (!users.ContainsKey(userName))
                users.Add(userName, new user(userName, password));
            else return false;
            return true;
        }

        public user getUser(string username, string password)
        {
            user u = users[username];
            if (u.Password.Equals(password))
                return users[username];
            else
                throw new Exception("Wrong password");
        }
        public user getUser(string username)
        {
            return users[username];
        }

        public bool editUserName(string userName, user u)
        {
            if (activeUsers.ContainsKey(u.Username))
                u.Username = userName;
            else return false;
            return true;
        }

        public bool editPassword(string password, user u)
        {
            if (activeUsers.ContainsKey(u.Username))
                u.Password = password;
            else return false;
            return true;
        }

        public bool editAvatar(Image avatar, user u)
        {
            if (activeUsers.ContainsKey(u.Username))
                u.Avatar = avatar;
            else return false;
            return true;
        }

        public bool logout(user u)
        {
            if (activeUsers.ContainsKey(u.Username))
                activeUsers.Remove(u.Username);
            else return false;
            return true;
        }
    }
}
