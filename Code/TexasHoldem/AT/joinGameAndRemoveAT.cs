using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceLayer;
using ServiceLayer.Interfaces;
using Gaming;
using ServiceLayer.Models;
using AT.Stubs;
using GameSystem;
namespace AT
{
    [TestFixture]
    class joinGameAndRemoveAT
    {
        GCServiceInterface gc;
        GameService gameService;
        SystemService us;
        string username = "ohad", password = "213";
        ClientGame retGame;
        GamePreferences pref;
        [SetUp]
        public void before()
        {
            us = new SystemService();
            us.register(username, password);
            us.login(username, password);
            gameService = new GameService();
            if (GameCenterService.testable)
                gc = new GameCenterService();
            else
                gc = new GameCenterStub();
            pref = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);

        }
        [TearDown]
        public void after()
        {
            GameCenter.GameCenterFactory.clean();
            TexasHoldemSystem.userSystemFactory.clean();
        }
        [Test]

        public void joinGameTest()
        {
            pref = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            ClientGame game = gc.createGame(pref, username);

            List<String> users = gc.joinGame(game.getID(), username, 50);
            Assert.True(users.Contains(us.getUser(username).Username));
        }

        [Test]
        public void joinGameTwiceTest()
        {
            ClientGame game = gc.createGame(pref, username);
            List<String> users = gc.joinGame(game.getID(), username, 50);
            gc.joinGame(game.getID(), username, 60);
            Assert.True(users.Contains(us.getUser(username).Username));
        }
        [Test]
        public void joinToWrongGame()
        {
            Assert.Null(gc.joinGame(999, username, 100));
        }
        public void joinToGameAndWithTooMuchMoney()
        {
            pref = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            ClientGame game = gc.createGame(pref, username);
            Assert.Null(gc.joinGame(game.getID(), username, 999));
            game.Players.Remove(us.getUser(username));
        }

        public void jointoFullTable()
        {
            pref = new GamePreferences(3, 2, 5, 10, 1, 2, 3, true);
            ClientGame game = gc.createGame(pref, username);
            us.register("a1", "1");
            us.register("a2", "1");
            us.register("a3", "1");
            gc.joinGame(game.getID(), "a1", 100));
            gc.joinGame(game.getID(), "a2", 100));
            gc.joinGame(game.getID(), "a3", 100));
            Assert.Null(gc.joinGame(game.getID(), username, 50));
            game.Players.Remove(us.getUser(username));
        }
        [Test]
        public void removePlayerTest()
        {
            pref = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            ClientGame game = gc.createGame(pref, username);
            gc.joinGame(game.getID(), username, 3);
            gameService.removePlayer(username, game.getID());
            Assert.False(game.Players.Contains(us.getUser(username)));
        }

        [Test]
        public void removeUnExistedPlayer()
        {
            pref = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            ClientGame game = gc.createGame(pref, username);
            gc.joinGame(game.getID(), username, 100);
            Assert.Throws(typeof(InvalidOperationException), delegate
            {
                gameService.removePlayer("Moshe", game.getID());
            }
            );

        }

    }
}

