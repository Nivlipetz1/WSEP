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
        string username = "ohad", password = "123";
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
            Assert.AreEqual(0, game.Players.Count);

            List<String> users= gc.joinGame(game.getID(), username, 50,out retGame);
            Assert.AreEqual(1, users.Count);
            Assert.True(users.Contains(us.getUser(username).Username));
        }

        [Test]
        public void joinGameTwiceTest()
        {
            ClientGame game = gc.createGame(pref, username);
            Assert.AreEqual(0, game.Players.Count);
            List<String> users = gc.joinGame(game.getID(), username, 50, out retGame);
            gc.joinGame(game.getID(), username, 60,out retGame);
            Assert.AreEqual(1, users.Count);
            Assert.True(users.Contains(us.getUser(username).Username));
        }
        [Test]
        public void joinToWrongGame()
        {
            Assert.Null(gc.joinGame(999, username, 100,out retGame));
        }
        public void joinToGameAndWithTooMuchMoney()
        {
            pref = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            ClientGame game = gc.createGame(pref, username);
            Assert.Null(gc.joinGame(game.getID(), username, 999, out retGame));
            game.Players.Remove(us.getUser(username));
        }

        public void jointoFullTable()
        {
            pref = new GamePreferences(3, 2, 5, 10, 1, 2, 3, true);
            ClientGame game = gc.createGame(pref, username);
            us.register("a1", "1");
            us.register("a2", "1");
            us.register("a3", "1");
            Assert.NotNull(gc.joinGame(game.getID(), "a1", 100, out retGame));
            Assert.NotNull(gc.joinGame(game.getID(), "a2", 100, out retGame));
            Assert.NotNull(gc.joinGame(game.getID(), "a3", 100, out retGame));
            Assert.Null(gc.joinGame(game.getID(), username, 50, out retGame));
            game.Players.Remove(us.getUser(username));
        }
        [Test]
        public void removePlayerTest()
        {
            pref = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            ClientGame game = gc.createGame(pref, username);
            gc.joinGame(game.getID(), username, 3, out retGame);
            Assert.NotNull(gameService.removePlayer(username, game.getID()));
            Assert.False(game.Players.Contains(us.getUser(username)));
        }

        [Test]
        public void removeUnExistedPlayer()
        {
            pref = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            ClientGame game = gc.createGame(pref, username);
            gc.joinGame(game.getID(), username, 100, out retGame);
            Assert.Throws(typeof(InvalidOperationException), delegate
            {
                gameService.removePlayer("Moshe", game.getID());
            }
            );

        }

    }
}

