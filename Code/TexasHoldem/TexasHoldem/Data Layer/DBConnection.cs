using GameSystem;
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
            var db = client.GetDatabase("pokerDB");
            Users =  db.GetCollection<UserProfile>("Users");
            Replayes = db.GetCollection<GameLogger>("Replayes");
            Leagues = db.GetCollection<League>("Leagues");

            NotificationService.saveReplayEvt += saveReplay;
        }

        public static DBConnection Instance
        {
            get { return LazyInstance.Value; }
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
        public List<UserProfile> getTop20Users(string criterion)
        {
            switch (criterion)
            {
                case "gamesPlayed":
                        return (from user in Users.AsQueryable()
                            let gamesPlayed = user.UserStat.Losses + user.UserStat.Winnings
                            orderby gamesPlayed
                            select user).Take(20).ToList();
                case "CashGain":
                        return (from user in Users.AsQueryable()
                                orderby user.UserStat.BiggestWin
                                select user).Take(20).ToList();
                case "totalGrossProfit":
                        return (from user in Users.AsQueryable()
                            orderby user.UserStat.TotalGrossProfit
                            select user).Take(20).ToList();
            }
            return null;
        }
    }

    public static class DbExtenstion
    {
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
