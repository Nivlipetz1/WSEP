using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;
using Gaming;
using NUnit.Framework;
using ServiceLayer;
using ServiceLayer.Models;
using ServiceLayer.Interfaces;
using AT.Stubs;

namespace AT
{
    [TestFixture]
    class Join_SpectateGameAT
    {
        UserProfile userProf;
        UserProfile userProf2;
        UserProfile userProf3;
        GamePreferences prefs;
        TexasHoldemSystem us;
        GCServiceInterface gc;

        [SetUp]
        public void before()
        {
            //  gc = GameCenter.GameCenterFactory.getInstance();
            if (GameCenterService.testable)
                gc = new GameCenterService();
            else
                gc = new GameCenterStub();
            //systemService = new SystemService();
            prefs = new GamePreferences(8, 2, 5, 10, 1, 20, 3, true);
            UserProfile ohadUser = new UserProfile("ohad", "213");
            gc.createGame(prefs, ohadUser.Username);
            prefs = new GamePreferences(8, 2, 5, 10, 1, 20, 3, false);
            gc.createGame(prefs, ohadUser.Username);
              us = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();
            //us = new SystemService();
            us.register("abc", "123");
            us.login("abc", "123");
            userProf = us.getUser("abc");
            us.register("abc2", "123");
            us.login("abc2", "123");
            userProf2 = us.getUser("abc2");
            us.register("abc3", "123");
            us.login("abc3", "123");
            userProf3 = us.getUser("abc3");

        }

        [TestCase]
        public void Valid_joinGame()
        {
            userProf.Credit = 50;
            ClientGame g = gc.getActiveGames("preferences",prefs, userProf3.Username)[0];
            
            int NumberOfPlayersBefore = g.Players.Count;
            Assert.NotNull(gc.joinGame(g.getID(), userProf.Username, 30));
            Assert.AreEqual(g.Players.Count, NumberOfPlayersBefore + 1);
        }

        [TestCase]
        public void InValid_joinGame_Buyin()
        {
            userProf.Credit = 50;
            ClientGame g = gc.getActiveGames("preferences", prefs, userProf3.Username)[0];
            int NumberOfPlayersBefore = g.Players.Count;
            Assert.Null(gc.joinGame(g.getID(), userProf.Username, 10));
            Assert.AreEqual(g.Players.Count, NumberOfPlayersBefore);
        }

        [TestCase]
        public void InValid_joinGame_Credit()
        {
            userProf.Credit = 50;
            ClientGame g = gc.getActiveGames("preferences", prefs, userProf3.Username)[0];
            int NumberOfPlayersBefore = g.Players.Count;
            Assert.Null(gc.joinGame(g.getID(), userProf.Username, 60));
            Assert.AreEqual(g.Players.Count, NumberOfPlayersBefore);
        }

        [TestCase]
        public void InValid_joinGame_MaxPlayers()
        {
            userProf.Credit = 50;
            userProf2.Credit = 50;
            userProf3.Credit = 50;
            GamePreferences prefs1; 
            Assert.Fail("Maximum number of players must be greater then minimum players",
               prefs1 = new GamePreferences(2, 2, 5, 12, 1, 20, 3, false));
            
            gc.createGame(prefs1, userProf3.Username);
            ClientGame g = gc.getActiveGames("preferences", prefs1, userProf3.Username)[0];
            Assert.NotNull(gc.joinGame(g.getID(), userProf.Username, 30));
            Assert.NotNull(gc.joinGame(g.getID(), userProf2.Username, 30));
            Assert.Null(gc.joinGame(g.getID(), userProf3.Username, 30));

            Assert.AreEqual(g.Players.Count, 2);
        }

        [TestCase]
        public void valid_spectateGame()
        {
            ClientGame g = gc.getAllSpectatingGames()[0];
            int NumberOfPlayersBefore = g.Spectators.Count;
            Assert.NotNull(gc.spectateGame(g.getID(), userProf.Username));
            Assert.AreEqual(g.Spectators.Count, NumberOfPlayersBefore + 1);
        }

        [TestCase]
        public void valid_spectateGamesList()
        {
            List<ClientGame> games = gc.getAllSpectatingGames();
            foreach (ClientGame g in games)
                Assert.True(g.GamePref.AllowSpec());
        }

        [TestCase]
        public void valid_gameListByName()
        {
            userProf.Credit = 50;
            ClientGame g = gc.getActiveGames("preferences", prefs, userProf3.Username)[0]; ;
            gc.joinGame(g.getID(), userProf.Username, 30);

            List<ClientGame> games = gc.getActiveGames("playername","abc", userProf3.Username);
            Assert.AreEqual(1,games.Count);
            //Assert.True(games[0].GetUserProfiles().Contains(userProf));s
        }
    }
}
