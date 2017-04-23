using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gaming;
using User;

namespace Gaming
{

    /**
     * 
     * Class for testing players and spectators
     * 
     * */

    [TestFixture]

    class GameParticipantsTests
    {


        [TestCase]
        public void addPlayer()
        {
            Game g = new Game(new GamePreferences());
            UserProfile Niv = new UserProfile("Niv", "123");
            PlayingUser nivPlayer = new PlayingUser(Niv, 1000, g);

            Assert.IsEmpty(g.GetPlayers());
            g.addPlayer(nivPlayer);
            Assert.Contains(nivPlayer, g.GetPlayers());
        }

        

    }
}
