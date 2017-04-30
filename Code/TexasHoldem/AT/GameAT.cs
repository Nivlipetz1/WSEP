using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;
using GameSystem;
using NUnit.Framework;

namespace AT
{
    [TestFixture]
    class GameAT
    {
        Game game1 = null;
        
        [SetUp]
        public void before()
        {
            game1 = new Game(new GamePreferences(8, 2, 5, 10, 1, 2, 3, true));
        }

        [TestCase]
        public void remove_unknown_player()
        {
            UserProfile prof1 = new UserProfile("abc", "123");
            UserProfile prof2 = new UserProfile("cbc", "123");
            prof1.Credit = 500;
            prof2.Credit = 500;
            PlayingUser player1 = new PlayingUser(prof1.Username, prof1.Credit, game1);
            PlayingUser player2 = new PlayingUser(prof2.Username, prof2.Credit, game1);
            game1.addPlayer(player2);
            var ex = Assert.Throws<InvalidOperationException>(() => game1.removePlayer(player1));
            Assert.That(ex.Message.Equals("Player not in game"));
        }

        [TestCase]
        public void start_game_with_less_than_minPlayers()
        {
            UserProfile prof1 = new UserProfile("abc", "123");
            prof1.Credit = 500;
            PlayingUser player1 = new PlayingUser(prof1.Username, prof1.Credit, game1);
            game1.addPlayer(player1);
            var ex = Assert.Throws<InvalidOperationException>(() => game1.StartGame());
            Assert.That(ex.Message.Equals("Can't start game with less than the minimum number of players"));
        }

        [TestCase]
        public void RemovePlayerFromGame()
        {
            UserProfile prof = new UserProfile("abc", "123");
            prof.Credit = 500;
            PlayingUser player = new PlayingUser(prof.Username, prof.Credit, game1);

            game1.addPlayer(player);
            Assert.IsNotEmpty(game1.GetPlayers());
            game1.removePlayer(player);
            Assert.IsEmpty(game1.GetPlayers());
        }

        [TestCase]
        public void RemoveSpectatorFromGame()
        {
            UserProfile prof = new UserProfile("abc", "123");
            prof.Credit = 500;
            SpectatingUser player = new SpectatingUser(prof.Username, game1);

            game1.addSpectator(player);
            Assert.IsNotEmpty(game1.GetSpectators());
            game1.removeSpectator(player);
            Assert.IsEmpty(game1.GetSpectators());
        }

        [TestCase]
        public void Allin_Fold()
        {
            UserProfile prof1 = new UserProfile("abc", "123");
            UserProfile prof2 = new UserProfile("cbc", "123");
            UserProfile prof3 = new UserProfile("def", "123");
            prof1.Credit = 500;
            prof2.Credit = 500;
            prof3.Credit = 500;
            PlayingUser player1 = new PlayingUser(prof1.Username, prof1.Credit, game1);
            PlayingUser player2 = new PlayingUser(prof2.Username, prof2.Credit, game1);
            PlayingUser player3 = new PlayingUser(prof3.Username, prof3.Credit, game1);
            game1.addPlayer(player1); 
            game1.addPlayer(player2);
            game1.addPlayer(player3);
            

            player1.SetFakeUserInput(new Queue<string>(new[] { "5", "-1", "0" }));
            player2.SetFakeUserInput(new Queue<string>(new[] { "0", "-1", "0", "0" }));
            player3.SetFakeUserInput(new Queue<string>(new[] { "10", "100", "0" }));
            game1.StartGame();

            Assert.AreEqual(520, player3.GetCredit());
        }

        [TestCase]
        public void Allin_Fold1()
        {
            UserProfile prof1 = new UserProfile("abc", "123");
            UserProfile prof2 = new UserProfile("cbc", "123");
            UserProfile prof3 = new UserProfile("def", "123");
            prof1.Credit = 500;
            prof2.Credit = 500;
            prof3.Credit = 500;
            PlayingUser player1 = new PlayingUser(prof1.Username, prof1.Credit, game1);
            PlayingUser player2 = new PlayingUser(prof2.Username, prof2.Credit, game1);
            PlayingUser player3 = new PlayingUser(prof3.Username, prof3.Credit, game1);
            game1.addPlayer(player1);
            game1.addPlayer(player2);
            game1.addPlayer(player3);

            player1.SetFakeUserInput(new Queue<string>(new[] { "5", "200", "100" }));
            player2.SetFakeUserInput(new Queue<string>(new[] { "0", "200", "-1", "-1" }));
            player3.SetFakeUserInput(new Queue<string>(new[] { "10", "200", "-1","-1" }));
            game1.StartGame();

            Assert.AreEqual(920, player1.GetCredit());
        }

        [TestCase]
        public void MovesNumber()
        {
            UserProfile prof1 = new UserProfile("abc", "123");
            UserProfile prof2 = new UserProfile("cbc", "123");
            UserProfile prof3 = new UserProfile("def", "123");
            prof1.Credit = 500;
            prof2.Credit = 500;
            prof3.Credit = 500;
            PlayingUser player1 = new PlayingUser(prof1.Username, prof1.Credit, game1);
            PlayingUser player2 = new PlayingUser(prof2.Username, prof2.Credit, game1);
            PlayingUser player3 = new PlayingUser(prof3.Username, prof3.Credit, game1);
            game1.addPlayer(player1);
            game1.addPlayer(player2);
            game1.addPlayer(player3);

            player1.SetFakeUserInput(new Queue<string>(new[] { "5", "200", "100" }));
            player2.SetFakeUserInput(new Queue<string>(new[] { "0", "200", "-1", "-1" }));
            player3.SetFakeUserInput(new Queue<string>(new[] { "10", "200", "-1", "-1" }));
            game1.StartGame();

            int num_of_moves = game1.GetLogger().GetMoves().Count;
            Assert.AreEqual(15,num_of_moves);
        }

        [TestCase]
        public void resetPotSize()
        {
            UserProfile prof1 = new UserProfile("abc", "123");
            UserProfile prof2 = new UserProfile("cbc", "123");
            UserProfile prof3 = new UserProfile("def", "123");
            prof1.Credit = 500;
            prof2.Credit = 500;
            prof3.Credit = 500;
            PlayingUser player1 = new PlayingUser(prof1.Username, prof1.Credit, game1);
            PlayingUser player2 = new PlayingUser(prof2.Username, prof2.Credit, game1);
            PlayingUser player3 = new PlayingUser(prof3.Username, prof3.Credit, game1);
            game1.addPlayer(player1);
            game1.addPlayer(player2);
            game1.addPlayer(player3);

            player1.SetFakeUserInput(new Queue<string>(new[] { "5", "200", "100" }));
            player2.SetFakeUserInput(new Queue<string>(new[] { "0", "200", "-1", "-1" }));
            player3.SetFakeUserInput(new Queue<string>(new[] { "10", "200", "-1", "-1" }));
            game1.StartGame();

            int pot = game1.GetPotSize();
            Assert.AreEqual(0, pot);
        }
    }
}