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

    public class GamePlayTests
    {
        [TestCase]
        public void AllPlayersFold()
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

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "-1" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "20" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "-1" }));

            g.StartGame();

            Assert.AreEqual(1005, OPlayer.GetCredit());
        }

        [TestCase]
        public void AllPlayersCheck()
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

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5","0" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10","0" }));

            g.StartGame();
            int num_of_moves = g.GetLogger().GetMoves().Count;
            //game start - 1
            //blinds - 2
            //bets - 3
            //flop - 1
            //bets - 3
            //river - 1
            //bets - 3
            //turn - 1
            //bets - 3

            Assert.AreEqual(18, num_of_moves);
        }

        [TestCase]
        public void PlayersCloseBlindAndThenFold()
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

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5","0","-1" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0","0","-1" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10" , "100" }));

            g.StartGame();

            Assert.AreEqual(1020, NPlayer.GetCredit());
        }

        [TestCase]
        public void HeadToHeadAndFold()
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

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "-1" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "100" ,"100" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100","-1" }));

            g.StartGame();

            Assert.AreEqual(1120, OPlayer.GetCredit());
        }

        [TestCase]
        public void HeadToHeadTwiceAndFold()
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

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "100","100","0","-1" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "100", "100","200" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "-1" }));

            g.StartGame();

            Assert.AreEqual(1320, OPlayer.GetCredit());
        }


    }
}
