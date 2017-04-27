using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GameUtilities;

namespace TexasHoldemSystem
{
    [TestFixture]
    public class GAMECENTERLeagueTest
    {
        [TestCase]
        public void CreateNewLeagueTest()
        {
            GameCenter gc = new GameCenter();
            Assert.True(gc.createNewLeague(10));
            Assert.True(gc.leagues[0].MinimumRank == 10);
        }
        [TestCase]
        public void CreateNewLeagueWithSameRankTest()
        {
            GameCenter gc = new GameCenter();
            Assert.True(gc.createNewLeague(10));
            Assert.False(gc.createNewLeague(10));
            Assert.True(gc.leagues.Count==1);
        }
        [TestCase]
        public void addUserToAppropriateLeague()
        {
            GameCenter gc = new GameCenter();
            Assert.True(gc.createNewLeague(10));
            League l = gc.getLeagueByRank(10);
            UserProfile u = new UserProfile("ohad", "dali");
            u.Credit = 40;
            Assert.True(gc.addUserToLeague(u,l));
            Assert.True(l.isUser(u));
        }
        [TestCase]
        public void addUserToNotAppropriateLeague()
        {
            GameCenter gc = new GameCenter();
            Assert.True(gc.createNewLeague(10));
            League l = gc.getLeagueByRank(10);
            UserProfile u = new UserProfile("ohad", "dali");
            u.Credit = 5;
            Assert.False(gc.addUserToLeague(u, l));
            Assert.False(l.isUser(u));
        }
        [TestCase]
        public void removeExistedUser()
        {
            GameCenter gc = new GameCenter();
            Assert.True(gc.createNewLeague(10));
            League l = gc.getLeagueByRank(10);
            UserProfile u = new UserProfile("ohad", "dali");
            u.Credit = 40;
            Assert.True(gc.addUserToLeague(u, l));
            Assert.True(l.isUser(u));
            Assert.True(gc.removeUserFromLeague(u, l));
            Assert.False(l.isUser(u));
        }
        [TestCase]
        public void removeUnExistedUser()
        {
            GameCenter gc = new GameCenter();
            Assert.True(gc.createNewLeague(10));
            League l = gc.getLeagueByRank(10);
            UserProfile u = new UserProfile("ohad", "dali");
            u.Credit = 40;
            Assert.False(gc.removeUserFromLeague(u, l));
        }
        [TestCase]
        public void changeLeagueMinimumRankTest()
        {
            GameCenter gc = new GameCenter();
            Assert.True(gc.createNewLeague(10));
            League l = gc.getLeagueByRank(10);
            UserProfile u = new UserProfile("ohad", "dali");
            u.Credit = 40;
            Assert.True(gc.addUserToLeague(u,l));
            Assert.True(gc.changeLeagueMinimumRank(l,50));
            Assert.False(l.isUser(u));
               
        }

        [TestCase]
        public void changeLeagueMinimumRank2Test()
        {
            GameCenter gc = new GameCenter();
            Assert.True(gc.createNewLeague(10));
            League l = gc.getLeagueByRank(10);
            UserProfile u = new UserProfile("ohad", "dali");
            u.Credit = 40;
            Assert.True(gc.addUserToLeague(u, l));
            Assert.True(gc.changeLeagueMinimumRank(l, 15));
            Assert.True(l.isUser(u));

        }

        [TestCase]
        public void changeLeagueMinimumRankToExistedLeagueTest()
        {
            GameCenter gc = new GameCenter();
            Assert.True(gc.createNewLeague(10));
            Assert.True(gc.createNewLeague(15));
            League l = gc.getLeagueByRank(10);
            Assert.False(gc.changeLeagueMinimumRank(l, 15));

        }
    }
}
