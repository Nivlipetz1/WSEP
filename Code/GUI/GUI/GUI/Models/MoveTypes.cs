using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class NewCardMove : Move
    {
        Card[] cards;
        public Card[] Cards
        {
            get { return Cards; }
        }
    }
    public class GameStartMove : Move
    {
        private IDictionary<string, int> playerBets;

        public IDictionary<string, int> GetPlayerBets()
        {
            return playerBets;
        }
    }

    public class BetMove : Move
    {
        IDictionary<string, int> playerBets;
        string bettingPlayer;
        int betAmount;

        public IDictionary<string, int> GetPlayerBets()
        {
            return playerBets;
        }

        public string GetBettingPlayer()
        {
            return bettingPlayer;
        }

        public int GetAmount()
        {
            return betAmount;
        }

        public void setPlayer(string name)
        {
            bettingPlayer = name;
        }

        public void setAmt(int amt)
        {
            betAmount = amt;
        }
    }

    public class FoldMove : Move
    {
        IDictionary<string, int> playerBets;
        string foldingPlayer;
        public IDictionary<string, int> GetPlayerBets()
        {
            return playerBets;
        }
    }


    public class EndGameMove : Move
    {
        IDictionary<string, PlayerHand> playerHands;
        public IDictionary<string, PlayerHand> GetPlayerHands()
        {
            return playerHands;
        }
        IDictionary<string, CardAnalyzer.HandRank> handRanks;
    }
}
