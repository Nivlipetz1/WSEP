using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gaming;
using GameSystem;

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
                PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
                g.addPlayer(nivPlayer);
                g.StartGame();
            });
        }

        [TestCase]
        public void MoreThanMaxPlayersJoinGame()
        {

            Assert.Throws(typeof(InvalidOperationException), delegate()
            {
                Game g = new Game(new GamePreferences(2, 2, 1, 2, 1, 1, 1, true));
                UserProfile Niv = new UserProfile("Niv", "123");
                UserProfile Omer = new UserProfile("Omer", "456");
                UserProfile Naor = new UserProfile("Naor", "789");
                UserProfile Ohad = new UserProfile("Ohad", "8");

                PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
                PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
                PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);

                g.addPlayer(nivPlayer);
                g.addPlayer(OPlayer);
                g.addPlayer(NPlayer);

            });

        }

        [TestCase]
        public void InitGame()
        {
            Game g = new Game(new GamePreferences());
            Assert.True(g.GetGamePref().GetStatus().Equals("Init"));
            g.InactivateGame();
            Assert.True(g.GetGamePref().GetStatus().Equals("Inactive"));
        }
    }
}
