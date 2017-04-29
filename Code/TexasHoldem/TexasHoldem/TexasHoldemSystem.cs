﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameUtilities;

namespace TexasHoldemSystem
{
    class TexasHoldemSystem : System
    {
        public delegate void Notify(string message);
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
        private Dictionary<String, UserProfile> activeUsers;
        private Dictionary<String, UserProfile> users;
        public static event Notify evt;
        private GameCenter gc;

        private TexasHoldemSystem()
        {
            activeUsers = new Dictionary<string, UserProfile>();
            users = new Dictionary<string, UserProfile>();
            gc = new GameCenter();
            gc.setUsers(users.Values);
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
                users.Add(userName, new UserProfile(userName, password));
            else return false;

            evt += users[userName].addNotify;
            return true;
        }


        public UserProfile getUser(string username, string password)
        {
            UserProfile u = users[username];
            if (u.Password.Equals(password))
                return users[username];
            else
                throw new Exception("Wrong password");
        }
        public UserProfile getUser(string username)
        {
            return users[username];
        }

        public bool editUserName(string userName, UserProfile u)
        {
            if (activeUsers.ContainsKey(u.Username))
                u.Username = userName;
            else return false;
            return true;
        }

        public bool editPassword(string password, UserProfile u)
        {
            if (activeUsers.ContainsKey(u.Username))
                u.Password = password;
            else return false;
            return true;
        }

        public bool editAvatar(Image avatar, UserProfile u)
        {
            if (activeUsers.ContainsKey(u.Username))
                u.Avatar = avatar;
            else return false;
            return true;
        }

        public bool logout(UserProfile u)
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

        public void notifyAllUsers(String message)
        {
            var e = evt;
            if (e != null)
                e(message);
        }
    }
}
