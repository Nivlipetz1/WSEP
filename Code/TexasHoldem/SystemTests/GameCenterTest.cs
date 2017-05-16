using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gaming;
using GameSystem;

namespace SystemTests
{
    public class GameCenterTest
    {
        private GameCenter gameCenter = GameCenter.GameCenterFactory.getInstance();
        private TexasHoldemSystem us = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();
        private UserProfile user1, user2;

        [SetUp]
        public void setUp()
        {
            us.register("user1", "123");
            us.login("user1", "123");
            user1 = us.getUser("user1");

            us.register("user2", "123");
            us.login("user2", "123");
            user2 = us.getUser("user2"); 
        }

        [TestCase]
        public void createGameTest()
        {
            GamePreferences preferecnces = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            Assert.AreNotEqual(gameCenter.createGame(preferecnces, user1), null);
        }

        [TestCase]
        public void getAllSpectatingGamesTest()
        {
            GamePreferences preferecncesAllowSpec = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            GamePreferences preferecncesNotAllowSpec = new GamePreferences(8, 2, 10, 20, 1, 20, 6, false);
            Game gameAllowSpec = gameCenter.createGame(preferecncesAllowSpec, user1);
            Game gameNotAllowSpec = gameCenter.createGame(preferecncesNotAllowSpec, user2);

            Assert.True(gameCenter.getAllSpectatingGames().Contains(gameAllowSpec));
            Assert.False(gameCenter.getAllSpectatingGames().Contains(gameNotAllowSpec));
        }

        [TestCase]
        public void getAllActiveGamesByPlayerNameTest()
        {
            GamePreferences preferecnces1 = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            GamePreferences preferecnces2 = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);

            Game game1 = gameCenter.createGame(preferecnces1, user1);
            Game game2 = gameCenter.createGame(preferecnces2, user2);
            
            user1.Credit = 500;
            user2.Credit = 500;
            
            gameCenter.joinGame(game1, user1, 200);
            Assert.AreEqual(true, gameCenter.joinGame(game2, user2, 200));

            List<Game> games = gameCenter.getActiveGames("playerName", "user2", user1);
            Assert.AreEqual(games.Count, 1);
            Assert.AreEqual(games.ElementAt(0), game2);
        }

        public void getAllActiveGamesByGamePreferenceTest()
        {
            GamePreferences preferecnces1 = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            GamePreferences preferecnces2 = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            GamePreferences preferecnces3 = new GamePreferences(8, 2, 10, 20, -1, -1, -1, false);

            Game game1 = gameCenter.createGame(preferecnces1, user1);
            Game game2 = gameCenter.createGame(preferecnces2, user1);
            List<Game> games = gameCenter.getActiveGames("gamepreference", preferecnces3, user1);
            Assert.True(games.Count == 2 && games.Contains(game1) && games.Contains(game2));
        }

        [TestCase]
        public void joinGameTest()
        {
            GamePreferences preferecnces = new GamePreferences(2, 2, 10, 20, 1, 20, 6, true);

            Game game = gameCenter.createGame(preferecnces, user1);
            user1.Credit = 10;
            Assert.False(gameCenter.joinGame(game, user1, 200));
            user1.Credit = 500;
            Assert.False(gameCenter.joinGame(game, user1, 5));

            Assert.True(gameCenter.joinGame(game, user1, 200));

            user2.Credit = 500;
            Assert.True(gameCenter.joinGame(game, user2, 200));

            UserProfile user3 = new UserProfile("user3", "213");
            user3.Credit = 500;
            Assert.False(gameCenter.joinGame(game, user3, 200));
        }

        [TestCase]
        public void spectateGameTest()
        {

            GamePreferences preferecnces1 = new GamePreferences(2, 2, 10, 20, 1, 20, 6, true);
            Game game1 = gameCenter.createGame(preferecnces1, user2);
            GamePreferences preferecnces2 = new GamePreferences(2, 2, 10, 20, 1, 20, 6, false);
            Game game2 = gameCenter.createGame(preferecnces2, user2);


            Assert.True(gameCenter.spectateGame(game1, user1));
            Assert.False(gameCenter.spectateGame(game2, user1));

        }


    }
}
