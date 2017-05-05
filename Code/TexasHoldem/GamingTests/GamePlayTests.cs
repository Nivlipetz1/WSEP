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

            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "Fold" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "20" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "Fold" }));

            g.StartGame();

            Assert.AreEqual(1015, OPlayer.GetCredit());
        }

        [TestCase]
        public void AllPlayersCheck()
        {
            Game g = new Game(new GamePreferences());
            GameLogger logger = g.GetLogger();

            UserProfile Niv = new UserProfile("Niv", "123");
            UserProfile Omer = new UserProfile("Omer", "456");
            UserProfile Naor = new UserProfile("Naor", "789");

            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "0" }));

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

            Assert.AreEqual(19, num_of_moves);
        }

        [TestCase]
        public void PlayersCloseBlindAndThenFold()
        {
            Game g = new Game(new GamePreferences());
            GameLogger logger = g.GetLogger();

            UserProfile Niv = new UserProfile("Niv", "123");
            UserProfile Omer = new UserProfile("Omer", "456");
            UserProfile Naor = new UserProfile("Naor", "789");

            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "Fold" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "Fold" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100" }));

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

            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "Fold" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "100", "100" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "Fold" }));

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

            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "100", "100", "0", "Fold" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "100", "100", "200" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "Fold" }));

            g.StartGame();

            Assert.AreEqual(1320, OPlayer.GetCredit());
        }

        public void HeadToHead3Players()
        {
            Game g = new Game(new GamePreferences());
            GameLogger logger = g.GetLogger();

            UserProfile Niv = new UserProfile("Niv", "123");
            UserProfile Omer = new UserProfile("Omer", "456");
            UserProfile Naor = new UserProfile("Naor", "789");

            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "50", "50", "100", "0" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "50", "50", "100", "0" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "0", "100", "0" }));

            g.StartGame();
            BetMove lastMove = (BetMove)logger.GetMoves().ElementAt(logger.GetMoves().Count - 1);
            IDictionary<string, int> playerBets = lastMove.GetPlayerBets();
            int potSize = 0;
            foreach (string s in playerBets.Keys)
            {
                potSize += playerBets[s];
            }

            Assert.AreEqual(1000 + 420, OPlayer.GetCredit());
        }

        [TestCase]
        public void BadBetAndThenFixes()
        {
            Game g = new Game(new GamePreferences());
            GameLogger logger = g.GetLogger();

            UserProfile Niv = new UserProfile("Niv", "123");
            UserProfile Omer = new UserProfile("Omer", "456");
            UserProfile Naor = new UserProfile("Naor", "789");

            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "Fold" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "100", "100", "0", "50", "Fold" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));

            g.StartGame();

            Assert.AreEqual(790, OPlayer.GetCredit());
        }

        [TestCase]
        public void CheckGameReplay_20Moves()
        {
            Game g = new Game(new GamePreferences());
            GameLogger logger = g.GetLogger();

            UserProfile Niv = new UserProfile("Niv", "123");
            UserProfile Omer = new UserProfile("Omer", "456");
            UserProfile Naor = new UserProfile("Naor", "789");

            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "Fold" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "100", "100", "0", "50", "Fold" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));

            g.StartGame();

            Assert.AreEqual(logger.GetMoves().Count, 20);
        }

        [TestCase]
        public void CheckGameReplay_29Moves()
        {
            Game g = new Game(new GamePreferences());
            GameLogger logger = g.GetLogger();

            UserProfile Niv = new UserProfile("Niv", "123");
            UserProfile Omer = new UserProfile("Omer", "456");
            UserProfile Naor = new UserProfile("Naor", "789");
            UserProfile Koren = new UserProfile("Koren", "9");
            UserProfile Ohad = new UserProfile("Ohad", "8");



            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);
            PlayingUser KPlayer = new PlayingUser(Koren.Username, 1000, g);
            PlayingUser OhPlayer = new PlayingUser(Ohad.Username, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);
            g.addPlayer(KPlayer);
            g.addPlayer(OhPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "100" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "100", "100", "0", "50", "Fold" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            KPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            OhPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));

            g.StartGame();

            Assert.AreEqual(29, logger.GetMoves().Count);
        }


        [TestCase]
        public void MultipleRoundsIn1Game()
        {
            Game g = new Game(new GamePreferences());
            GameLogger logger = g.GetLogger();

            UserProfile Niv = new UserProfile("Niv", "123");
            UserProfile Omer = new UserProfile("Omer", "456");
            UserProfile Naor = new UserProfile("Naor", "789");
            UserProfile Koren = new UserProfile("Koren", "9");
            UserProfile Ohad = new UserProfile("Ohad", "8");

            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor.Username, 1000, g);
            PlayingUser KPlayer = new PlayingUser(Koren.Username, 1000, g);
            PlayingUser OhPlayer = new PlayingUser(Ohad.Username, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);
            g.addPlayer(KPlayer);
            g.addPlayer(OhPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "100" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "100", "100", "0", "50", "Fold" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            KPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            OhPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            g.StartGame();
            Assert.AreEqual(logger.GetMoves().Count, 29);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "100" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "100", "100", "0", "50", "Fold" }));
            KPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            OhPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            g.StartGame();
            Assert.AreEqual( (29 * 2), logger.GetMoves().Count);

        }

    }
}