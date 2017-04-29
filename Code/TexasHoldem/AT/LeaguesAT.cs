using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemSystem;
using Gaming;
using GameUtilities;
using NUnit.Framework;

namespace AT
{
    [TestFixture]
    class LeaguesAT
    {
        GameCenter gc;
        int minRank = 10;
        TexasHoldemSystem.TexasHoldemSystem us;

        [SetUp]
        public void before()
        {
            gc = new GameCenter();
            us = TexasHoldemSystem.TexasHoldemSystem.userSystemFactory.getInstance();
            us.register("user", "123");
            us.login("user", "123");
        }

        [TestCase]
        public void success_createLeague()
        {   
            int numberOfLeaguesBefore = gc.getLeagues().Count;
            Assert.True(gc.createNewLeague(minRank));
            Assert.AreEqual(gc.getLeagues().Count, numberOfLeaguesBefore + 1);
        }

        [TestCase]
        public void unsuccessful_createLeague()
        {
            Assert.True(gc.createNewLeague(minRank));
            int numberOfLeaguesBefore = gc.getLeagues().Count;
            Assert.False(gc.createNewLeague(minRank));
            Assert.AreEqual(gc.getLeagues().Count, numberOfLeaguesBefore);
        }

        [TestCase]
        public void changeRank_SameLeague()
        {
            gc.createNewLeague(minRank);
            gc.createNewLeague(minRank + 20);
            UserProfile user = us.getUser("user");
            user.Credit = minRank;
            gc.addUserToLeague(user, gc.getLeagueByRank(minRank));
            user.Credit = minRank + 10;
            gc.updateLeagueToUser(user);
            Assert.AreEqual(gc.getLeagueByRank(minRank), gc.getLeagueByUser(user));
        }

        [TestCase]
        public void changeRank_changeLeagueUP()
        {
            gc.createNewLeague(minRank);
            gc.createNewLeague(minRank + 20);
            UserProfile user = us.getUser("user");
            user.Credit = minRank;
            gc.addUserToLeague(user, gc.getLeagueByRank(minRank));
            user.Credit = minRank + 30;
            gc.updateLeagueToUser(user);
            Assert.AreNotEqual(gc.getLeagueByRank(minRank), gc.getLeagueByUser(user));
        }

        [TestCase]
        public void changeRank_changeLeagueDown()
        {
            gc.createNewLeague(minRank + 10);
            gc.createNewLeague(minRank);
            UserProfile user = us.getUser("user");
            user.Credit = minRank + 20;
            Assert.True(gc.addUserToLeague(user, gc.getLeagueByRank(minRank + 10)));
            user.Credit = minRank;
            gc.updateLeagueToUser(user);
            Assert.AreNotEqual(gc.getLeagueByRank(minRank), gc.getLeagueByUser(user));
        }

        [TestCase]
        public void addUserToMyLeague()
        {
            //im20 to leaguefrom10
            gc.createNewLeague(minRank + 10);
            UserProfile user = us.getUser("user");
            user.Credit = minRank + 20;
            Assert.True(gc.addUserToLeague(user, gc.getLeagueByRank(user.Credit)));
            Assert.AreEqual(gc.getLeagueByRank(minRank + 10), gc.getLeagueByUser(user));
        }

        [TestCase]
        public void addUserToWrongLeague()
        {
            gc.createNewLeague(minRank + 10);
            UserProfile user = us.getUser("user");
            user.Credit = minRank;
            Assert.False(gc.addUserToLeague(user, gc.getLeagueByRank(minRank + 10)));
        }

        [TestCase]
        public void Add_to_2_Leagues()
        {
            gc.createNewLeague(minRank + 10);
            gc.createNewLeague(minRank);
            UserProfile user = us.getUser("user");
            user.Credit = minRank + 20;
            Assert.True(gc.addUserToLeague(user, gc.getLeagueByRank(minRank + 10)));
            Assert.False(gc.addUserToLeague(user, gc.getLeagueByRank(minRank)));
        }

        [TestCase]
        public void newLeague_ToRankLeague()
        {

        }

        [TestCase]
        public void idleUser_League()
        {

        }
    }
}
