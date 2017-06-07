using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;
using GameSystem.Data_Layer;

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

        private List<UserProfile> users;
        private GameCenter gc;

        private TexasHoldemSystem()
        {
            activeUsers = new Dictionary<string, UserProfile>();
            users = new List<UserProfile>();
            gc = GameCenter.GameCenterFactory.getInstance();
            gc.setUsers(activeUsers.Values);
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
                user.League = gc.getLeagueByRank(0);
                user.League.addUser(user);
                users.AddUser(user);
                //gc.setUsers(users.Values);
            }
            else return false;

            return true;
        }


        public UserProfile getUser(string username, string password)
        {
            UserProfile u = users.GetByName(username);
            u.League = gc.getLeagueByRank(u.LeagueId);
            if (u.Password.Equals(password))
                return u;
            else
                throw new Exception("Wrong password");
        }
        public UserProfile getUser(string username)
        {
            UserProfile u= users.GetByName(username); 
            u.League = gc.getLeagueByRank(u.LeagueId);
            return u;
        }

        public bool editUserName(string userName, UserProfile u)
        {
            if (activeUsers.ContainsKey(u.Username))
                u.Username = userName;
            else return false;

            DBConnection.Instance.updateUserProfile(u);
            return true;
        }

        public bool editPassword(string password, UserProfile u)
        {
            if (activeUsers.ContainsKey(u.Username))
                u.Password = password;
            else return false;

            DBConnection.Instance.updateUserProfile(u);
            return true;
        }

        public bool editAvatar(Image avatar, UserProfile u)
        {
            if (activeUsers.ContainsKey(u.Username))
                u.Avatar = avatar;
            else return false;

            DBConnection.Instance.updateUserProfile(u);
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

        public List<UserProfile> getTop20(string criterion)
        {
            return DBConnection.Instance.getTop20Users(criterion);   
        }

        public void clearUsers()
        {
            users.Clear();
            activeUsers.Clear();
        }
    }
}
