﻿using GameSystem;
using Gaming;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.Data_Layer
{
    public class DBConnection
    {
        private static Lazy<DBConnection> LazyInstance = new Lazy<DBConnection>(() => new DBConnection() , true);
        internal IMongoCollection<GameLogger> Replayes { get; set; }
        internal IMongoCollection<UserProfile> Users { get; set; }
        internal IMongoCollection<League> Leagues { get; set; }

        private DBConnection()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("pokerDB_test");
            Users =  db.GetCollection<UserProfile>("Users");
            Replayes = db.GetCollection<GameLogger>("Replayes");
            Leagues = db.GetCollection<League>("Leagues");

            NotificationService.saveReplayEvt += saveReplay;
        }

        public static DBConnection Instance
        {
            get { return LazyInstance.Value; }
        }

        public List<UserProfile> GetUsers()
        {
            return Users.AsQueryable().ToList();
        }


        public void updateUserProfile(UserProfile user)
        {
            Users.ReplaceOne(
                u => u.Id == user.Id,
                user,
                new UpdateOptions { IsUpsert = true });
        }

        public List<League> getLeagues()
        {
            Leagues.AsQueryable().ForEachAsync(league => league.addUsers(Users.Find(user => user.LeagueId == league.minimumRank).ToList()));
            
            return Leagues.AsQueryable().ToList();
        }

        public int getNewGameId()
        {
            return Replayes.AsQueryable().Select(repl => repl.gameID).AsEnumerable().DefaultIfEmpty(0).Max() + 1;
        }

        public void saveReplay(GameLogger replay)
        {
            if (Replayes.Find(repl => repl.gameID == replay.gameID).ToList().Count > 0)
            {
                Replayes.ReplaceOne(
                    repl => repl.gameID == replay.gameID,
                    replay,
                    new UpdateOptions { IsUpsert = true });
            }
            else
                Replayes.InsertOne(replay);
        }

        public GameLogger getReplayById(int gameId)
        {
            return (from logger in Replayes.AsQueryable()
            where logger.gameID == gameId
            select logger).First();
        }
        public List<Tuple<string,int>> getTop20Users(string criterion)
        {
            switch (criterion)
            {
                case "gamesPlayed":
                        return (from user in Users.AsQueryable()
                            orderby user.UserStat.TotalGames descending
                            select new Tuple<string,int>(user.Username , user.UserStat.Losses + user.UserStat.Winnings)).Take(20).ToList();
                case "cashGain":
                        return (from user in Users.AsQueryable()
                                orderby user.UserStat.BiggestWin descending
                                select new Tuple<string, int>(user.Username, user.UserStat.BiggestWin)).Take(20).ToList();
                case "totalGrossProfit":
                        return (from user in Users.AsQueryable()
                            orderby user.UserStat.TotalGrossProfit descending
                            select new Tuple<string, int>(user.Username , user.UserStat.TotalGrossProfit)).Take(20).ToList();
            }
            return null;
        }

        public double getCashGain(string name)
        {
            return Users.Find(user => user.Username == name).First().UserStat.AvgCashGain;
        }

        public double getGrossProfit(string name)
        {
            return Users.Find(user => user.Username == name).First().UserStat.AvgGrossProfit;
        }

        public bool isUserExist(string userName)
        {
            return Users.Find(user => user.Username == userName).Count() > 0;
        }

        public bool checkUserDetails(string userName, string password)
        {
            return Users.Find(user => user.Username == userName && user.Password == password).Count() > 0;
        }

        public List<int> getAllAvailableReplayes()
        {
            return (from replay in Replayes.AsQueryable()
                    select replay.gameID).ToList();
        }
    }

    public static class DbExtenstion
    {
        public static Boolean DeleteReplay(int replayID)
        {
            foreach (int p in DBConnection.Instance.getAllAvailableReplayes())
            {
                if (p == replayID)
                {
                    DBConnection.Instance.Replayes.DeleteOne(i => i.gameID == replayID);
                    return true;
                }

            }
            return false;
        }


        public static Boolean DeleteUser(this List<UserProfile> list, string element)
        {
            foreach (UserProfile p in DBConnection.Instance.GetUsers())
            {
                if (p.Username.Equals(element))
                {
                    DBConnection.Instance.Users.DeleteOne(u => u.Username.Equals(element));
                    return true;
                }

            }
            return false;
        }


        public static void AddUser(this List<UserProfile> list, UserProfile element)
        {
            DBConnection.Instance.Users.InsertOne(element);
        }

        public static bool ContainsKey(this List<UserProfile> list , string key)
        {
            IMongoCollection<UserProfile> coll = DBConnection.Instance.Users;
            return coll.Find(user => user.Username == key).ToList().Count == 1;
        }

        public static UserProfile GetByName(this List<UserProfile> list, string name)
        {
            IMongoCollection<UserProfile> coll = DBConnection.Instance.Users;
            return coll.Find(user => user.Username == name).First();
        }

        public static void Save(this League league)
        {
            DBConnection.Instance.Leagues.InsertOne(league);
        }
    }
}
