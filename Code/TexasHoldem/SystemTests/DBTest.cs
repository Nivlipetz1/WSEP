using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using GameSystem;
using Gaming;
using GameSystem.Data_Layer;

namespace TexasHoldemSystem.Data_Layer
{
    [TestFixture]
    public class DBtests
    {
        DBConnection db = DBConnection.Instance;
        List<int> gameReplayList;
        List<UserProfile> userList;
        List<League> leagueList;
        GameSystem.TexasHoldemSystem system = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();


        [SetUp]
        public void before()
        {
            userList = db.GetUsers();
            gameReplayList = db.getAllAvailableReplayes();
            leagueList = db.getLeagues();

        }

        [TearDown]
        public void after()
        {
            DbExtenstion.DeleteUser(db.GetUsers(), "test123");
            DbExtenstion.DeleteReplay(999);

        }


        [Test]
        public void UserListUpdated()
        {
            system.register("test123", "123");
            UserProfile test = system.getUser("test123");
            Assert.False(userList.Contains(test));
            bool flag = false;

            foreach(UserProfile p in db.GetUsers())
            {
                if (p.Username.Equals(test.Username))
                {
                    flag = true; 
                    Assert.True(true);
                }
            }

            if(!flag)
                Assert.True(false);

        }

        [Test]
        public void RegisterDuplicatedUser()
        {
            int count = 0;
            system.register("test123", "123");
            system.register("test123", "123");
            foreach (UserProfile p in db.GetUsers())
            {
                if (p.Username.Equals("test123"))
                {
                    count++;
                }
            }

            Assert.True(count == 1);

        }

        [Test]
        public void ReplayListUpdate()
        {
            Assert.False(gameReplayList.Contains(999));

            Game g = new Game(new GamePreferences());
            PlayingUser p1 = new PlayingUser("niv",100,g);
            Dictionary<string, int> playerbets = new Dictionary<string, int>();
            playerbets.Add(p1.GetUserName(), 10);
            GameLogger insert = new GameLogger(999);

            insert.AddMove(new GameStartMove(playerbets));
            Assert.True(db.getAllAvailableReplayes().Contains(999));
            insert.AddMove(new BetMove(playerbets,p1,10));
            Assert.True(db.getAllAvailableReplayes().Contains(999));

        }

        [Test]
        public void LeaguesListExists()
        {
            List<League> leagues= db.getLeagues();
            if (leagues.Count == 1)
            {
                Assert.True(leagues.First().MinimumRank==0);
            }
            else if (leagues.Count==0) //no leagues => no users registered
            {
                Assert.True(db.GetUsers().Count==0);
            }

        }






    }
}
