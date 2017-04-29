using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GameUtilities;
using Gaming;
using GameSystem;
using Services;

namespace SystemTests
{
    public class GameCenterTest
    {
        private GameCenter gameCenter;
        private TexasHoldemSystem us = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();
        [SetUp]
        public void setUp()
        {
            gameCenter =  new GameCenter();
        }

        [TestCase]
        public void createGameTest()
        {
            GamePreferences preferecnces = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            Assert.True((gameCenter.createGame(preferecnces) != null));
        }

        [TestCase]
        public void getAllSpectatingGamesTest()
        {
            GamePreferences preferecncesAllowSpec = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            GamePreferences preferecncesNotAllowSpec = new GamePreferences(8, 2, 10, 20, 1, 20, 6, false);
            Game gameAllowSpec = gameCenter.createGame(preferecncesAllowSpec);
            Game gameNotAllowSpec = gameCenter.createGame(preferecncesNotAllowSpec);
            bool correctOutput = gameCenter.getAllSpectatingGames().Contains(gameAllowSpec) && !gameCenter.getAllSpectatingGames().Contains(gameNotAllowSpec);
            Assert.True(correctOutput);
        }

        [TestCase]
        public void getAllActiveGamesByPlayerNameTest()
        {
            GamePreferences preferecnces1 = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            GamePreferences preferecnces2 = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            Game game1 = gameCenter.createGame(preferecnces1);
            Game game2 = gameCenter.createGame(preferecnces2);
            UserProfile ohadUser = new UserProfile("ohad", "213");
            UserProfile naorUser = new UserProfile("naor", "465");
            ohadUser.Credit = 500;
            naorUser.Credit = 500;
            gameCenter.joinGame(game1, ohadUser , 200);
            gameCenter.joinGame(game2, naorUser, 200);
            List<Game> games = gameCenter.getAllActiveGamesByPlayerName("naor");
            Assert.True(games.Count == 1 && games.ElementAt(0) == game2);
        }

        public void getAllActiveGamesByGamePreferenceTest()
        {
            GamePreferences preferecnces1 = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            GamePreferences preferecnces2 = new GamePreferences(8, 2, 10, 20, 1, 20, 6, true);
            GamePreferences preferecnces3 = new GamePreferences(8, 2, 10, 20, -1, -1, -1, false);
            Game game1 = gameCenter.createGame(preferecnces1);
            Game game2 = gameCenter.createGame(preferecnces2);
            List<Game> games = gameCenter.getAllActiveGamesByGamePreference(preferecnces3);
            Assert.True(games.Count == 2 && games.Contains(game1) && games.Contains(game2));
        }

        [TestCase]
        public void joinGameTest()
        {
            GamePreferences preferecnces = new GamePreferences(2, 2, 10, 20, 1, 20, 6, true);
            Game game = gameCenter.createGame(preferecnces);
            UserProfile ohadUser = new UserProfile("ohad", "213");
            ohadUser.Credit = 10;
            Assert.False(gameCenter.joinGame(game, ohadUser, 200));
            ohadUser.Credit = 500;
            Assert.False(gameCenter.joinGame(game, ohadUser, 5));

            Assert.True(gameCenter.joinGame(game, ohadUser, 200));

            UserProfile naorUser = new UserProfile("naor", "213");
            naorUser.Credit = 500;
            Assert.True(gameCenter.joinGame(game, naorUser, 200));

            UserProfile korenUser = new UserProfile("koren", "213");
            korenUser.Credit = 500;
            Assert.False(gameCenter.joinGame(game, korenUser, 200));
        }

        [TestCase]
        public void spectateGameTest()
        {
            GamePreferences preferecnces1 = new GamePreferences(2, 2, 10, 20, 1, 20, 6, true);
            Game game1 = gameCenter.createGame(preferecnces1);
            GamePreferences preferecnces2 = new GamePreferences(2, 2, 10, 20, 1, 20, 6, false);
            Game game2 = gameCenter.createGame(preferecnces2);
            UserProfile ohadUser = new UserProfile("ohad", "213");

            Assert.True(gameCenter.spectateGame(game1, ohadUser));
            Assert.False(gameCenter.spectateGame(game2, ohadUser));

        }


    }
}
