using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gaming;
using GameSystem;

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
        public void AddPlayerToGameWaitingList()
        {
            Game g = new Game(new GamePreferences());
            UserProfile Niv = new UserProfile("Niv", "123");
            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);

            g.GetGamePref().SetStatus("Active");
            Assert.IsEmpty(g.GetPlayers());
            g.addPlayer(nivPlayer);
            Assert.False(g.GetPlayers().Contains(nivPlayer));
            Assert.True(g.GetWaitingList().Contains(nivPlayer));
        }
        
        [TestCase]
        public void AddPlayerToGame()
        {
            Game g = new Game(new GamePreferences());
            UserProfile Niv = new UserProfile("Niv", "123");
            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);

            Assert.IsEmpty(g.GetPlayers());
            g.addPlayer(nivPlayer);
            Assert.Contains(nivPlayer, g.GetPlayers());
        }

        [TestCase]
        public void AddSpectatorToGame()
        {
            Game g = new Game(new GamePreferences());
            UserProfile Niv = new UserProfile("Niv", "123");
            SpectatingUser nivPlayer = new SpectatingUser(Niv.Username, g);

            Assert.IsEmpty(g.GetSpectators());
            g.addSpectator(nivPlayer);
            Assert.Contains(nivPlayer, g.GetSpectators());
        }

        [TestCase]
        public void ChatTest()
        {
            Game g = new Game(new GamePreferences());
            UserProfile Niv = new UserProfile("Niv", "123");
            SpectatingUser nivPlayer = new SpectatingUser(Niv.Username, g);
            UserProfile Omer = new UserProfile("Omer", "123");
            SpectatingUser omerPlayer = new SpectatingUser(Omer.Username, g);
            g.addSpectator(nivPlayer);
            g.addSpectator(omerPlayer);
            nivPlayer.SendMessage("hello world!");


            Assert.AreEqual("hello world!", omerPlayer.GetMessages().First());
        }

        [TestCase]
        public void AddSpectatorsToGameThatDoesntAllowSpecators()
        {
            Assert.Throws(typeof(InvalidOperationException), delegate()
            {
                Game g = new Game(new GamePreferences(8, 2, 5, 10, 1, 2, 3, false));
                UserProfile Niv = new UserProfile("Niv", "123");
                SpectatingUser N = new SpectatingUser(Niv.Username, g);
                Assert.False(g.GetGamePref().AllowSpec());
                g.addSpectator(N);
            });
        }

        [TestCase]
        public void RemovePlayerFromGame()
        {
            Game g = new Game(new GamePreferences());
            UserProfile Niv = new UserProfile("Niv", "123");
            PlayingUser nivPlayer = new PlayingUser(Niv.Username, 1000, g);

            g.addPlayer(nivPlayer);
            Assert.IsNotEmpty(g.GetPlayers());
            g.removePlayer(nivPlayer);
            Assert.IsEmpty(g.GetPlayers());
        }

        [TestCase]
        public void RemoveSpectatorFromGame()
        {
            Game g = new Game(new GamePreferences());
            UserProfile Niv = new UserProfile("Niv", "123");
            SpectatingUser nivPlayer = new SpectatingUser(Niv.Username, g);

            g.addSpectator(nivPlayer);
            Assert.IsNotEmpty(g.GetSpectators());
            g.removeSpectator(nivPlayer);
            Assert.IsEmpty(g.GetSpectators());
        }


        [TestCase]
        public void IncreaseRoundsWon()
        {
            Game g = new Game(new GamePreferences());

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

            Assert.AreEqual(1, OPlayer.GetRoundsWon());
        }

        [TestCase]
        public void IncreaseRoundsLost()
        {
            Game g = new Game(new GamePreferences());

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

            Assert.AreEqual(1, nivPlayer.GetRoundsLost());
        }

        [TestCase]
        public void IncreaseBiggestPotWon()
        {
            Game g = new Game(new GamePreferences());

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

            Assert.AreEqual(45, OPlayer.GetMostWon());
        }

        [TestCase]
        public void UpdateBestHand()
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

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0", "100" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "0", "0", "100", "100", "0", "50", "Fold" }));
            KPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            OhPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "100", "100" }));
            g.StartGame();

            foreach(PlayingUser player in g.GetPlayers()){
                if (player.GetCredit() > 1000)
                {
                    Assert.AreNotEqual(CardAnalyzer.HandRank.HighCard, player.GetBestHand());
                }
            }
        }
    }
}
