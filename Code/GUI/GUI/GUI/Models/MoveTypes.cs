using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class NewCardMove : Move
    {
        public Card[] cards { set; get; }

        public NewCardMove(Card[] cards)
        {
            this.cards = cards;

        }
    }
    
    public class GameStartMove : Move
    {
        public Dictionary<string, int> playerBets { set; get; }
        public Dictionary<string, PlayerHand> playerHands { set; get; }

        public GameStartMove(Dictionary<string, int> playerBets, Dictionary<string, PlayerHand> playerHands)
        {
            this.playerBets = playerBets;
            this.playerHands = playerHands;
        }
    }

    public class BetMove : Move
    {
        public Dictionary<string, int> playerBets { set; get; }
        public string bettingPlayer { set; get; }
        public int betAmount { set; get; }

        public BetMove(Dictionary<string, int> playerBets, string bettingPlayer, int betAmount)

        {
            this.playerBets = playerBets;
            this.bettingPlayer = bettingPlayer;
            this.betAmount = betAmount;
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
        public Dictionary<string, int> playerBets { set; get; }
        public string foldingPlayer { set; get; }

        public FoldMove(Dictionary<string, int> playerBets, string foldingPlayer)
        {
            this.playerBets = playerBets;
            this.foldingPlayer = foldingPlayer;
        }

        public string GetFoldingPlayer()
        {
            return foldingPlayer;
        }

        public void SetFoldPlayer(string name) {
            foldingPlayer = name;
        }
    }


    public class EndGameMove : Move
    {
        public Dictionary<string, PlayerHand> playerHands { set; get; }
        public Dictionary<string, CardAnalyzer.HandRank> handRanks { set; get; }

        public EndGameMove(Dictionary<string, PlayerHand> playerHands, Dictionary<string, CardAnalyzer.HandRank> handRanks)
        {
            this.playerHands = playerHands;
            this.handRanks = handRanks;
        }

    }
}
