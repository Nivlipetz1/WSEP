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
        public IDictionary<string, int> playerBets { set; get; }

        public GameStartMove(IDictionary<string, int> playerBets)
        {
            this.playerBets = playerBets;
        }
    }

    public class BetMove : Move
    {
        public IDictionary<string, int> playerBets { set; get; }
        public string bettingPlayer { set; get; }
        public int betAmount { set; get; }

        public BetMove(IDictionary<string, int> playerBets, string bettingPlayer, int betAmount)
        {
            this.playerBets = playerBets;
            this.bettingPlayer = bettingPlayer;
            this.betAmount = betAmount;
        }
    }

    public class FoldMove : Move
    {
        public IDictionary<string, int> playerBets { set; get; }
        public string foldingPlayer { set; get; }

        public FoldMove(IDictionary<string, int> playerBets, string foldingPlayer)
        {
            this.playerBets = playerBets;
            this.foldingPlayer = foldingPlayer;
        }
    }


    public class EndGameMove : Move
    {
        public IDictionary<string, PlayerHand> playerHands { set; get; }
        public IDictionary<string, CardAnalyzer.HandRank> handRanks { set; get; }

        public EndGameMove(IDictionary<string, PlayerHand> playerHands, IDictionary<string, CardAnalyzer.HandRank> handRanks)
        {
            this.playerHands = playerHands;
            this.handRanks = handRanks;
        }
    }
}
