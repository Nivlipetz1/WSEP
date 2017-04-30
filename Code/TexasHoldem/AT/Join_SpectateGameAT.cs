using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;
using Gaming;
using NUnit.Framework;
using GameUtilities;
using ServiceLayer;

namespace AT
{
    [TestFixture]
    class Join_SpectateGameAT
    {
        GameCenterService gc;
        GameSystem.TexasHoldemSystem us;
        UserProfile userProf;
        UserProfile userProf2;
        UserProfile userProf3;
        GamePreferences prefs;

        [SetUp]
        public void before()
        {
            gc = new GameCenterService();
            prefs = new GamePreferences(8, 2, 5, 10, 1, 20, 3, true);
            gc.createGame(prefs);
            prefs = new GamePreferences(8, 2, 5, 10, 1, 20, 3, false);
            gc.createGame(prefs);
            us = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();
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
            Game g = gc.getAllActiveGamesByGamePreference(prefs)[0];
            int NumberOfPlayersBefore = g.GetNumberOfPlayers();
            Assert.True(gc.joinGame(g, userProf, 30));
            Assert.AreEqual(g.GetNumberOfPlayers(), NumberOfPlayersBefore + 1);
        }

        [TestCase]
        public void InValid_joinGame_Buyin()
        {
            userProf.Credit = 50;
            Game g = gc.getAllActiveGamesByGamePreference(prefs)[0];
            int NumberOfPlayersBefore = g.GetNumberOfPlayers();
            Assert.False(gc.joinGame(g, userProf, 10));
            Assert.AreEqual(g.GetNumberOfPlayers(), NumberOfPlayersBefore);
        }

        [TestCase]
        public void InValid_joinGame_Credit()
        {
            userProf.Credit = 50;
            Game g = gc.getAllActiveGamesByGamePreference(prefs)[0];
            int NumberOfPlayersBefore = g.GetNumberOfPlayers();
            Assert.False(gc.joinGame(g, userProf, 60));
            Assert.AreEqual(g.GetNumberOfPlayers(), NumberOfPlayersBefore);
        }

        [TestCase]
        public void InValid_joinGame_MaxPlayers()
        {
            userProf.Credit = 50;
            userProf2.Credit = 50;
            userProf3.Credit = 50;

            GamePreferences prefs1 = new GamePreferences(2, 2, 5, 12, 1, 20, 3, false);
            gc.createGame(prefs1);
            Game g = gc.getAllActiveGamesByGamePreference(prefs1)[0];
            Assert.True(gc.joinGame(g, userProf, 30));
            Assert.True(gc.joinGame(g, userProf2, 30));
            Assert.False(gc.joinGame(g, userProf3, 30));

            Assert.AreEqual(g.GetNumberOfPlayers(), 2);
        }

        [TestCase]
        public void valid_spectateGame()
        {
            Game g = gc.getAllSpectatingGames()[0];
            int NumberOfPlayersBefore = g.GetSpectators().Count;
            Assert.True(gc.spectateGame(g, userProf));
            Assert.AreEqual(g.GetSpectators().Count, NumberOfPlayersBefore + 1);
        }

        [TestCase]
        public void valid_spectateGamesList()
        {
            List<Game> games = gc.getAllSpectatingGames();
            foreach (Game g in games)
                Assert.True(g.GetGamePref().AllowSpec());
        }

        [TestCase]
        public void valid_gameListByName()
        {
            userProf.Credit = 50;
            Game g = gc.getAllActiveGamesByGamePreference(prefs)[0];
            gc.joinGame(g, userProf, 30);

            List<Game> games = gc.getAllActiveGamesByPlayerName("abc");
            Assert.AreEqual(1,games.Count);
            Assert.True(games[0].GetUserProfiles().Contains(userProf));
        }
    }
}
