using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using GameSystem;
using Gaming;
using GameSystem.Data_Layer;
using ServiceLayer;

namespace TexasHoldemSystem.Data_Layer
{
    [TestFixture]
    public class DBtests
    {
        DBConnection db = GameSystem.Data_Layer.DBConnection.Instance;
        List<int> gameReplayList;
        List<UserProfile> userList;
        List<League> leagueList;
        SystemService us;
        GameSystem.TexasHoldemSystem system = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();


        [SetUp]
        public void before()
        {
            userList = db.GetUsers();
            gameReplayList = db.getAllAvailableReplayes();
            leagueList = db.getLeagues();
            //us = new SystemService();

        }

        [TearDown]
        public void after()
        {
            //delete user test123
            DbExtenstion.DeleteUser(db.GetUsers(), "test123");
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
//            us.register("test123", "123");


        }

        [Test]
        public void ReplayListUpdate()
        {


        }

        [Test]
        public void LeaguesListUpdate()
        {

        }






    }
}
