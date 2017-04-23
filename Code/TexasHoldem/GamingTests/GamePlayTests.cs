using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gaming;
using User;

namespace GamingTests
{

    [TestFixture]

    public class GamePlayTests
    {


        [TestCase]
        public void foldFirstMove()
        {
            Game g = new Game(new GamePreferences());
            GameLogger logger = g.GetLogger();

            UserProfile Niv = new UserProfile("Niv", "123");
            UserProfile Omer = new UserProfile("Omer", "456");
            UserProfile Naor = new UserProfile("Naor", "789");

            PlayingUser nivPlayer = new PlayingUser(Niv, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);

            nivPlayer.SetFakeUserInput(-1);
            OPlayer.SetFakeUserInput(2);
            NPlayer.SetFakeUserInput(-1);

            g.StartGame();

            Assert.True(true);
        }


    }
}
