using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;

namespace GameSystem
{
    public class TexasHoldemSystem : SystemAPI
    {
        public class userSystemFactory
        {
            private static TexasHoldemSystem instance = null;

            public  static TexasHoldemSystem getInstance()
            {
                if (instance == null)
                    return instance=new TexasHoldemSystem();
                return instance;
            }
            public static void clean()
            {
                instance = null; ;
            }
        }
        private Dictionary<String, UserProfile> activeUsers;
        private Dictionary<String, UserProfile> users;
        private GameCenter gc;

        private TexasHoldemSystem()
        {
            activeUsers = new Dictionary<string, UserProfile>();
            users = new Dictionary<string, UserProfile>();
            gc = GameCenter.GameCenterFactory.getInstance();
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
            {
                UserProfile user = new UserProfile(userName, password);
                user.Credit = 200;
                users.Add(userName, user);
                user.League = gc.getLeagueByRank(0);
                user.League.addUser(user);
                gc.setUsers(users.Values);
            }
            else return false;

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

        public void notifyAllUsers(string message)
        {
            NotificationService.Instance.notifyAllUsers(message);
        }

        public void notify(string userName , string message)
        {
            NotificationService.Instance.notifyUser(userName , message);
        }

        public void clearUsers()
        {
            users.Clear();
            activeUsers.Clear();
        }
    }
}
