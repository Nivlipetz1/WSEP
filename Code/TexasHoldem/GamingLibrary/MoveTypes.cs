using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class NewCardMove : Move
    {
        private Card[] cards;
        public NewCardMove(Card[] cards)
        {
            this.cards = cards;
            base.type = "NewCardMove";
        }

        public Card[] Cards
        {
            get
            {
                return cards;
            }
            set
            {
                cards = value;
            }
        }

        public override void update(ref IDictionary<string, int> playerBets, ref Card[] cards, ref IDictionary<string, PlayerHand> playerHands)
        {
            cards = this.cards;
        }
    }
    public class GameStartMove : Move
    {
        private IDictionary<string, int> playerBets;
        
        public GameStartMove(IDictionary<string, int> playerBets)
        {
            this.playerBets = playerBets;
            base.type = "GameStartMove";
        }

        public override void update(ref IDictionary<string, int> playerBets, ref Card[] cards, ref IDictionary<string, PlayerHand> playerHands)
        {
            playerBets = this.playerBets;
        }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public IDictionary<string, int> PlayerBets
        {
            get
            {
                return playerBets;
            }
            set
            {
                playerBets = value;
            }
        }
    }

    public class BetMove : Move
    {
        private IDictionary<string, int> playerBets;
        private string bettingPlayer;
        private int betAmount;

        public BetMove(IDictionary<string, int> playerBets, PlayingUser better, int amt)
        {
            this.playerBets = playerBets;
            bettingPlayer = better.GetUserName();
            //bettingPlayer.SetStatus("Talked");
            betAmount = amt;
            base.type = "BetMove";
        }

        public override void update(ref IDictionary<string, int> playerBets, ref Card[] cards, ref IDictionary<string, PlayerHand> playerHands)
        {
            playerBets = this.playerBets;
        }

        public IDictionary<string, int> GetPlayerBets()
        {
            return playerBets;
        }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public IDictionary<string, int> PlayerBets
        {
            get
            {
                return playerBets;
            }

            set
            {
                playerBets = value;
            }
        }

        public string BettingPlayer
        {
            get
            {
                return bettingPlayer;
            }
            set
            {
                bettingPlayer = value;
            }
        }

        public int BetAmount
        {
            get
            {
                return betAmount;
            }
            set
            {
                betAmount = value;
            }
        }
    }

    public class FoldMove : Move
    {
        private IDictionary<string, int> playerBets;
        private string foldingPlayer;

        public FoldMove(IDictionary<string, int> playerBets, PlayingUser folder)
        {
            this.playerBets = playerBets;
            foldingPlayer = folder.GetUserName();
            //foldingPlayer.SetStatus("Fold");
            base.type = "FoldMove";
        }

        public override void update(ref IDictionary<string, int> playerBets, ref Card[] cards, ref IDictionary<string, PlayerHand> playerHands)
        {
            playerBets = this.playerBets;
        }

        public IDictionary<string, int> GetPlayerBets()
        {
            return playerBets;
        }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public IDictionary<string, int> PlayerBets
        {
            get
            {
                return playerBets;
            }
            set
            {
                playerBets = value;
            }
        }

        public string FoldingPlayer
        {
            get
            {
                return foldingPlayer;
            }
            set
            {
                foldingPlayer = value;
            }
        }
    }


    public class EndGameMove : Move
    {
        private IDictionary<string, PlayerHand> playerHands;
        private IDictionary<string, CardAnalyzer.HandRank> handRanks;
        private CardAnalyzer ca = new CardAnalyzer();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public IDictionary<string, PlayerHand> PlayerHands
        {
            get
            {
                return playerHands;
            }
            set
            {
                playerHands = value;
            }
        }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public IDictionary<string, CardAnalyzer.HandRank> HandRanks
        {
            get
            {
                return handRanks;
            }
            set
            {
                handRanks = value;
            }
        }

        public EndGameMove(IDictionary<string, PlayerHand> playerHands)
        {
            this.playerHands = playerHands;
            handRanks = new Dictionary<string, CardAnalyzer.HandRank>();
            base.type = "EndGameMove";
        }

        public override void update(ref IDictionary<string, int> playerBets, ref Card[] cards, ref IDictionary<string, PlayerHand> playerHands)
        {
            playerHands = this.playerHands;
            if (cards != null) //this happens when all fold before flop
            {
                ca.setCardArray(cards);
                if (HandRanks.Count == 0)
                {
                    foreach (string player in playerHands.Keys)
                    {
                        ca.setHand(playerHands[player]);
                        HandRanks.Add(player, ca.analyze());
                    }
                }
            }
        }
    }

}
