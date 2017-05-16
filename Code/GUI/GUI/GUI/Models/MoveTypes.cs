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
    }
    public class GameStartMove : Move
    {
        IDictionary<string, int> playerBets;
    }

    public class BetMove : Move
    {
        IDictionary<string, int> playerBets { get; }
        string bettingPlayer { get; }
        int betAmount { get;}
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
        IDictionary<string, CardAnalyzer.HandRank> handRanks;
    }
}
