using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace GameSystem
{
    public class League
    {
        [BsonId]
        public int minimumRank {set;get;}
        public string name { set; get; }
        [BsonIgnore]
        public HashSet<UserProfile> users { set; get; }
        [BsonIgnore]
        public Game[] games { set; get; }

        public League(int minimumRank,string name)
        {
            this.name = name;
            this.minimumRank = minimumRank;
            users = new HashSet<UserProfile>();
            games = new Game[50];
        }
        public int MinimumRank
        {
            set { minimumRank = value; }
            get { return minimumRank; }
        }

        public bool addUser(UserProfile user)
        {
            if (users == null)
                users = new HashSet<UserProfile>();
            if (users.Contains(user))
                return false;
            users.Add(user);
            user.League = this;
            //TexasHoldemSystem.userSystemFactory.getInstance().notify(user.Username, "You Added to League " + name + " with rank " + minimumRank + " !");
            return true;
        }

        public bool removeUser(UserProfile user)
        {
            if (users == null)
                users = new HashSet<UserProfile>();
            if (!users.Contains(user))
                return false;
            users.Remove(user);
            return true;
        }

        public void addUsers(List<UserProfile> users)
        {
            if (this.users == null)
                this.users = new HashSet<UserProfile>();
            foreach (UserProfile user in users)
            {
                addUser(user);
            }
        }

        public void removeUsers(List<UserProfile> u)
        {
            foreach(UserProfile user in u)
            {
                if (users.Contains(user))
                    users.Remove(user);
            }
        }

        public List<UserProfile> update(int newRank)
        {
            List<UserProfile> UserToRemove = new List<UserProfile>();
            foreach(UserProfile user in users)
            {
                if (user.Credit < newRank)
                    UserToRemove.Add(user);
            }
            foreach(UserProfile user in UserToRemove)
            {
                users.Remove(user);
            }
            minimumRank = newRank;
            return UserToRemove;
        }
        public bool isUser(UserProfile user)
        {
            return users.Contains(user);
        }

        public Game[] getGames()
        {
            if (games == null)
                games = new Game[50];
            return games;
        }

        public void addGame(Game g)
        {
            if (games == null)
                games = new Game[50];
            for (int i = 0; i < 50; i++)
            {
                if(games[i] == null)
                {
                    g.setID(i);
                    games[i] = g;    
                    break;
                }
            }
        }

        public void removeGame(int gameID)
        {
            if (games == null)
                games = new Game[50];
            games[gameID] = null;
        }
    }
}
