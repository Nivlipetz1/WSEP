using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceLayer;
using Services;
using Gaming;
using GameUtilities;

namespace AT
{
    [TestFixture]
    class IntegrationTest
    {
        SystemAPI usys = null;
        GameCenterInterface gc = null;
        LeagueAPI lc = null;
        [SetUp]
        public void before()
        {
            usys = new UserSystem_Service();
            gc = new GameCenterService();
            lc = new LeagueService();
        }
        [TestCase]
        public void Integration()
        {
            Assert.True(usys.register("user1", "12345"));
            UserProfile user1 = usys.getUser("user1");
            Assert.True(usys.register("user2", "12345"));
            UserProfile user2 = usys.getUser("user2");
            Assert.True(usys.login("user1", "12345"));
            Assert.False(usys.login("user2", "12346"));
            Assert.True(usys.login("user2", "12345"));
            GamePreferences pref = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            Game game1=gc.createGame(pref);
            Assert.AreEqual(game1, gc.getAllActiveGamesByGamePreference(pref)[0]);
            Assert.True(gc.joinGame(game1, user1,100));
            Assert.False(gc.joinGame(game1, user2, 200));
            Assert.True(gc.joinGame(game1, user2, 150));
            Assert.AreEqual(50, user1.Credit);
            Assert.AreEqual(0, user1.Credit);
        }
    }
}
