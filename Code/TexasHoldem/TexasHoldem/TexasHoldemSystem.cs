using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;

namespace TexasHoldemSystem
{
    class TexasHoldemSystem : System
    {
        public class userSystemFactory
        {
            private static TexasHoldemSystem instance = null;
            public  static TexasHoldemSystem getInstance()
            {
                if (instance == null)
                    return new TexasHoldemSystem();
                return instance;
            }
        }
        private Dictionary<String, user> activeUsers;
        private Dictionary<String, user> users;

        private TexasHoldemSystem()
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

        public bool isConnected(string username)
        {
            return activeUsers.ContainsKey(username);
        }
        
        
    }
}
