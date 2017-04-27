using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gaming;
using GameUtilities;

namespace GamingTests
{
    [TestFixture]
    public class GamePropertiesTests
    {

        [TestCase]
        public void InvalidGameCreation()
        {
            //create gamepreferences with maxPlayers >8 and min players <2

        }

        [TestCase]
        public void ValidGameCreation()
        {
            Game g = new Game(new GamePreferences(8, 2, 5, 10, 1, 2, 3, true));
            Assert.True(g.GetGamePref().GetMaxPlayers() == 8);
            Assert.True(g.GetGamePref().GetMinPlayers() == 2);
            Assert.True(g.GetGamePref().GetsB() == 5);
            Assert.True(g.GetGamePref().GetbB() == 10);
            Assert.True(g.GetGamePref().GetTypePolicy() == 1);
            Assert.True(g.GetGamePref().GetBuyInPolicy() == 2);
            Assert.True(g.GetGamePref().GetChipPolicy() == 3);
            Assert.True(g.GetGamePref().AllowSpec());
        }

        [TestCase]
        public void GameStartWithLessThanMinimumPlayers()
        {
            Assert.Throws(typeof(InvalidOperationException), delegate()
            {
                Game g = new Game(new GamePreferences(8, 2, 5, 10, 1, 2, 3, true));
                UserProfile Niv = new UserProfile("Niv", "123");
                PlayingUser nivPlayer = new PlayingUser(Niv, 1000, g);
                g.addPlayer(nivPlayer);
                g.StartGame();
            });
        }

        [TestCase]
        public void MoreThanMaxPlayersJoinGame()
        {
            Game g = new Game(new GamePreferences());

        }

        [TestCase]
        public void ActiveGame()
        {
            Game g = new Game(new GamePreferences());
            Assert.True(g.GetGamePref().GetStatus().Equals("active"));
            g.InactivateGame();
            Assert.True(g.GetGamePref().GetStatus().Equals("inactive"));
        }
    }
}
