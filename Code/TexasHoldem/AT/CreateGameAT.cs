using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gaming;
using ServiceLayer;
using GameSystem;
using ServiceLayer.Models;

namespace AT
{
    [TestFixture]
    class CreateGameAT
    {
        GameCenterService gc = new GameCenterService();

        [TestCase]
        public void Valid_createGame()
        {
            GamePreferences prefs = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            ClientUserProfile ohadUser = new ClientUserProfile (new UserProfile("ohad", "213"));
            gc.createGame(prefs, ohadUser);
            Assert.AreEqual(gc.getActiveGames("preferences" , prefs , ohadUser).Count(), 1);
        }

        [TestCase]
        public void Invalid_createGame_maxPlayers()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => new GamePreferences(9, 2, 5, 10, 1, 2, 3, true));
            Assert.That(ex.Message == "Maximum number of players must be at most 8");
        }

        [TestCase]
        public void Invalid_createGame_minPlayers()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => new GamePreferences(7, 0, 5, 10, 1, 2, 3, true));
            Assert.That(ex.Message == "Minimum number of players must be atleast 2");
        }

        [TestCase]
        public void Invalid_createGame_MaxminPlayers()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => new GamePreferences(0, 2, 5, 10, 1, 2, 3, true));
            Assert.That(ex.Message == "Minimum number of players must be greater then maximum players");
        }
    }
}