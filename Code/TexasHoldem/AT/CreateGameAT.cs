using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GameUtilities;
using Gaming;
using TexasHoldemSystem;

namespace AT
{
    [TestFixture]
    class CreateGameAT
    {
        TexasHoldemSystem.GameCenter gc = new GameCenter();

        [TestCase]
        public void Valid_createGame()
        {
            GamePreferences prefs = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            gc.createGame(prefs);
            Assert.AreEqual(gc.getAllActiveGamesByGamePreference(prefs).Count(), 1);
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