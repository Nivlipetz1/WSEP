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
    public class MoveTests
    {
        [TestCase]
        public void ValidBetMove()
        {
            Game g = new Game(new GamePreferences());
            GameLogger logger = g.GetLogger();

            UserProfile Niv = new UserProfile("Niv", "123");
            UserProfile Omer = new UserProfile("Omer", "456");

            PlayingUser nivPlayer = new PlayingUser(Niv, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5", "0"}));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0" }));

            g.StartGame();

            IDictionary<string, int> playerBets = new Dictionary<string,int>();
            playerBets.Add(nivPlayer.GetAccount().Username, 5);
            playerBets.Add(OPlayer.GetAccount().Username, 10);
            BetMove bm = new BetMove(playerBets);
            BetMove compareToBetMove = ((BetMove)logger.GetMoves()[2]); //third move in game -> bigblind (first = start game, second = small blind, third=bigblind)


            foreach(string s in bm.GetPlayerBets().Keys){
                int testBet = bm.GetPlayerBets()[s];
                int gameBet = compareToBetMove.GetPlayerBets()[s];
                Assert.AreEqual(testBet, gameBet);
            }
        
        }

        [TestCase]
        public void ValidNewCardMove()
        {

        }

        [TestCase]
        public void ValidNewGameMove()
        {


        }

        [TestCase]
        public void ValidEndGameMoves()
        {

        } 
    }
}
