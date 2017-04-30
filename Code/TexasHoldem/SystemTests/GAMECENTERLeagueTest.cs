using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GameSystem;
using System;
using Gaming;

namespace SystemTests
{
    [TestFixture]
    public class GameCenterLeagueTest
    {
        [TestCase]
        public void CreateNewLeagueTest()
        {
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
            Dictionary<int,League> le = gc.getLeagues();
            Assert.True(gc.createNewLeague(10));
            Assert.True(le.Keys.Contains(10));
        }
        [TestCase]
        public void CreateNewLeagueWithSameRankTest()
        {
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
            Dictionary<int,League> le = gc.getLeagues();
            Assert.True(gc.createNewLeague(10));
            Assert.False(gc.createNewLeague(10));
            Assert.True(le.Count==2);
        }
        [TestCase]
        public void addUserToAppropriateLeague()
        {
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
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
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
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
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
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
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
            Assert.True(gc.createNewLeague(10));
            League l = gc.getLeagueByRank(10);
            UserProfile u = new UserProfile("ohad", "dali");
            u.Credit = 40;
            Assert.False(gc.removeUserFromLeague(u, l));
        }
        [TestCase]
        public void changeLeagueMinimumRankTest()
        {
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
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
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
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
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
            Assert.True(gc.createNewLeague(10));
            Assert.True(gc.createNewLeague(15));
            League l = gc.getLeagueByRank(10);
            Assert.False(gc.changeLeagueMinimumRank(l, 15));
        }
        [TestCase]
        public void updateLeaguesTest()
        {
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
            gc.createNewLeague(50);
            gc.createNewLeague(30);
            UserProfile u = new UserProfile("Ohad", "Dali");
            List<UserProfile> users = new List<UserProfile>();
            users.Add(u);
            gc.setUsers(users);
            PlayingUser up = new PlayingUser(u.Username, 0, null);
            u.Credit = 60;
            gc.addUserToLeague(u, gc.getLeagueByRank(50));
            u.Credit = 45;
            gc.updateLeagueToUser(up);
            League l = gc.getLeagueByUser(u);
            Assert.AreEqual(gc.getLeagueByRank(30), gc.getLeagueByUser(u));
            Assert.False(gc.getLeagueByRank(50).isUser(u));
        }
        [TestCase]
        public void updateLeaguesWithoutChangeTest()
        {
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
            gc.createNewLeague(50);
            gc.createNewLeague(30);
            UserProfile u = new UserProfile("Ohad", "Dali");
            List<UserProfile> users = new List<UserProfile>();
            users.Add(u);
            gc.setUsers(users);
            PlayingUser up = new PlayingUser(u.Username, 5, null);
            u.Credit = 60;
            gc.addUserToLeague(u, gc.getLeagueByRank(50));
            u.Credit = 50;
            gc.updateLeagueToUser(up);
            Assert.AreEqual(gc.getLeagueByRank(50), gc.getLeagueByUser(u));
            Assert.True(gc.getLeagueByRank(50).isUser(u));
        }
        [TestCase]
        public void setUsersTest()
        {
            GameCenter gc = GameCenter.GameCenterFactory.getInstance();
            Dictionary<String, UserProfile> user = new Dictionary<string, UserProfile>();
            gc.setUsers(user.Values);
            UserProfile u = new UserProfile("aaa", "bbb");
            Assert.AreEqual(null, gc.getHighestRankUser());
            u.Credit = 40;
            user.Add("aaa", u);
            gc.updateState();
            Assert.AreEqual(u, gc.getHighestRankUser());
            
        }
    }
}
