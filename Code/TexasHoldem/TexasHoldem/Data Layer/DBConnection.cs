using GameSystem;
using Gaming;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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
        internal IMongoCollection<UserCreditInGame> usersCredit { get; set; }

        private MongoClient client;

        private DBConnection()
        {
            connect();

            NotificationService.saveReplayEvt += saveReplay;
            NotificationService.updateCreditEvt += updateUserCredit;
        }

        private void connect()
        {
            client = new MongoClient();
            var db = client.GetDatabase("pokerDB2");
            Users = db.GetCollection<UserProfile>("Users");
            Replayes = db.GetCollection<GameLogger>("Replayes");
            Leagues = db.GetCollection<League>("Leagues");
            usersCredit = db.GetCollection<UserCreditInGame>("usersCredit");
            /*var filter = Builders<UserProfile>.Filter.Or(Builders<UserProfile>.Filter.Eq("LeagueId", "1000") , Builders<UserProfile>.Filter.Eq("LeagueId", "2000"),
                Builders<UserProfile>.Filter.Eq("LeagueId", "3000"));
            var update = Builders<UserProfile>.Update.Set("LeagueId", "0");
            Users.UpdateMany(filter, update);*/
        }

        public T GlobalTryCatch<T>(Func<T> action)
        {
            try
            {
                return action.Invoke();
            }
            catch (MongoConnectionException)
            {
                NotificationService.Instance.dbDown();
                client.ListDatabases();   //block until db is up
                NotificationService.Instance.dbUp();
                return GlobalTryCatch(() => { return action.Invoke(); }); ;
            }
            catch (Exception e)
            {
                return default(T);
            }
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
            GlobalTryCatch<object>(() =>
            {
                Users.ReplaceOne(
                    u => u.Id == user.Id,
                    user,
                    new UpdateOptions { IsUpsert = true });
                return null;
            });

        }

        public List<League> getLeagues()
        {
            List<League> leagues = Leagues.AsQueryable().ToList();
            leagues.ForEach(league => league.addUsers(Users.Find(user => user.LeagueId == league.minimumRank).ToList()));

            return leagues;
        }

        public int getNewGameId()
        {
            return Replayes.AsQueryable().Select(repl => repl.gameID).AsEnumerable().DefaultIfEmpty(0).Max() + 1;
        }

        public void saveReplay(GameLogger replay)
        {
            GlobalTryCatch<object>(() =>
            {
                if (Replayes.Find(repl => repl.gameID == replay.gameID).Count() > 0)
                {
                    Replayes.ReplaceOne(
                        repl => repl.gameID == replay.gameID,
                        replay,
                        new UpdateOptions { IsUpsert = true });
                }
                else
                    Replayes.InsertOne(replay);
                return null;
            });
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
            return GlobalTryCatch<double>(() =>
            {
                return Users.Find(user => user.Username == name).First().UserStat.AvgCashGain;
            });
        }

        public double getGrossProfit(string name)
        {
            return GlobalTryCatch<double>(() =>
            {
                return Users.Find(user => user.Username == name).First().UserStat.AvgGrossProfit;
            });
        }

        public bool isUserExist(string userName)
        {
            return GlobalTryCatch<bool>(() =>
            {
                return Users.Find(user => user.Username == userName).Count() > 0;
            });
        }

        public bool checkUserDetails(string userName, string password)
        {
            return GlobalTryCatch<bool>(() =>
            {
                return Users.Find(user => user.Username == userName && user.Password == password).Count() > 0;
            });
           
        }

        public List<int> getAllAvailableReplayes()
        {
            return (from replay in Replayes.AsQueryable()
                    select replay.gameID).ToList();
        }

        public bool deleteReplay(int replayID)
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

        public bool deleteUser(string element)
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

        public void updateUserCredit(string userName , int credit)
        {
            GlobalTryCatch<object>(() =>
            {
                UserCreditInGame user = usersCredit.Find(u => u.UserName == userName).FirstOrDefault<UserCreditInGame>();
                if (user != default(UserCreditInGame))
                {
                    user.Credit += credit;
                    usersCredit.ReplaceOne(
                        u => u.UserName == userName,
                        user,
                        new UpdateOptions { IsUpsert = true });
                }
                else
                    usersCredit.InsertOne(new UserCreditInGame(userName, credit));
                return null;
            });
        }

        public void updateUsersCredit()
        {
            List<UserCreditInGame> users = usersCredit.AsQueryable().ToList();
            foreach(UserCreditInGame user in users)
            {
                UserProfile userProf = Users.Find(u => u.Username == user.UserName).First();
                userProf.Credit += user.Credit;
                Users.ReplaceOne(
                        u => u.Username == user.UserName,
                        userProf,
                        new UpdateOptions { IsUpsert = true });

                usersCredit.DeleteOne(u => u.UserName == user.UserName);
            }
        }
    }

    internal class UserCreditInGame
    {
        public ObjectId Id { get; set; }
        public string UserName { set; get; }
        public int Credit { set; get; }

        public UserCreditInGame(string name , int credit)
        {
            UserName = name;
            Credit = credit;
        }
    }

    public static class DbExtenstion
    {

        public static void AddUser(this List<UserProfile> list, UserProfile element)
        {
            DBConnection db = DBConnection.Instance;
            db.GlobalTryCatch<object>(() =>
            {
                db.Users.InsertOne(element);
                return null;
            });
        }

        public static bool ContainsKey(this List<UserProfile> list , string key)
        {
            DBConnection db = DBConnection.Instance;
            return db.GlobalTryCatch<bool>(() =>
            {
                IMongoCollection<UserProfile> coll = DBConnection.Instance.Users;
                return coll.Find(user => user.Username == key).ToList().Count >= 1;
            });

        }

        public static UserProfile GetByName(this List<UserProfile> list, string name)
        {
            DBConnection db = DBConnection.Instance;
            return db.GlobalTryCatch<UserProfile>(() =>
            {
                IMongoCollection<UserProfile> coll = DBConnection.Instance.Users;
                return coll.Find(user => user.Username == name).First();
            });

        }

        public static void Save(this League league)
        {
            DBConnection db = DBConnection.Instance;
            db.GlobalTryCatch<object>(() =>
            {
                DBConnection.Instance.Leagues.InsertOne(league);
                return null;
            });
        }
    }
}
