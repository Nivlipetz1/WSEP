using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
/// <summary>
/// Created by omerh on 4/8/2017.
/// </summary>


namespace Gaming
{

    public class CardAnalyzer
    {
        public const int ACE = 13;
        public enum HandRank
        {
            RoyalFlush,
            StraightFlush,
            FourOfAKind,
            FullHouse,
            Flush,
            Straight,
            ThreeOfAKind,
            TwoPair,
            OnePair,
            HighCard
        }



        private Card[] cardArray;
        private PlayerHand hand;
        private IDictionary<HandRank, AnalyzeOp> map = new Dictionary<HandRank, AnalyzeOp>();
        delegate int AnalyzeOp(CardAnalyzer ca);

        public CardAnalyzer()
        {
            map.Add(HandRank.RoyalFlush, RoyalFlush);
            map.Add(HandRank.StraightFlush, StraightFlush);
            map.Add(HandRank.FourOfAKind, FourOfAKind);
            map.Add(HandRank.FullHouse, FullHouse);
            map.Add(HandRank.Flush, Flush);
            map.Add(HandRank.Straight, Straight);
            map.Add(HandRank.ThreeOfAKind, ThreeOfAKind);
            map.Add(HandRank.TwoPair, TwoPair);
            map.Add(HandRank.OnePair, OnePair);
            map.Add(HandRank.HighCard, HighCard);
        }

        public void setCardArray(Card[] cardArray)
        {
            this.cardArray = cardArray;
        }

        public void setHand(PlayerHand hand)
        {
            this.hand = hand;
        }

        public HandRank analyze()
        {
            foreach (HandRank rank in Enum.GetValues(typeof(HandRank)))
            {
                if (map[rank](this) != 0)
                {
                    return rank;
                }
            }
            return 0; //Should Never Get Here!!
        }


        AnalyzeOp RoyalFlush = (CardAnalyzer ca) =>
        {
            bool breakLoop = false;
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                for (int value = 10; value <= ACE; value++)
                {
                    if (!ca.CheckIfCardExists(new Card(value, suit)))
                    {
                        breakLoop = true;
                        break;
                    }
                }
                if (!ca.CheckIfCardExists(new Card(1, suit)))
                {
                    breakLoop = true;
                }
                if (!breakLoop)
                {
                    return 10;
                }
            }
            return 0;
        };

        AnalyzeOp StraightFlush = (CardAnalyzer ca) =>
        {
            int return_value = 0;
            bool hasStraight = false;
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                int straightInARow = 0;
                for (int value = 1; value <= ACE; value++)
                {
                    if (!ca.CheckIfCardExists(new Card(value, suit)))
                    {
                        straightInARow = 0;
                    }
                    else
                    {
                        if (straightInARow < 5)
                        {
                            straightInARow++;
                        }
                        if (straightInARow >= 5)
                        {
                            hasStraight = true;
                            return_value = value;
                        }
                    }
                }
                if (ca.CheckIfCardExists(new Card(1, suit)) && straightInARow == 4)
                {
                    return 1;
                }

            }
            //System.out.println(return_value);
            if (hasStraight)
            {
                return return_value;
            }
            else
            {
                return 0;
            }
        };

        AnalyzeOp FourOfAKind = (CardAnalyzer ca) =>
        {
            int[] instanceCounter = ca.countInstancesByValue();
            int i = 0;
            foreach (int instances in instanceCounter)
            {
                i++;
                if (instances == 4)
                {
                    return i;
                }
            }
            return 0;
        };

        AnalyzeOp FullHouse = (CardAnalyzer ca) =>
        {
            int[] instanceCounter = ca.countInstancesByValue();
            for (int i = 0; i < instanceCounter.Length; i++)
            {
                if (instanceCounter[i] >= 3)
                {
                    for (int j = 0; j < instanceCounter.Length; j++)
                    {
                        if (instanceCounter[j] >= 2 && i != j)
                        {
                            return i;
                        }
                    }
                }
            }
            return 0;
        };

        AnalyzeOp Flush = (CardAnalyzer ca) =>
        {
            int[] instanceCounter = ca.countInstancesBySuit();
            foreach (int instances in instanceCounter)
            {
                if (instances == 5)
                {
                    return 10;
                }
            }
            return 0;
        };

        AnalyzeOp Straight = (CardAnalyzer ca) =>
        {
            int straightInARow = 0;
            int return_value = 0;
            bool hasStraight = false;
            for (int value = 1; value <= ACE; value++)
            {
                bool valueAppeared = false;
                foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
                {
                    if (ca.CheckIfCardExists(new Card(value, suit)))
                    {
                        valueAppeared = true;
                    }
                }
                if (valueAppeared)
                {
                    if (straightInARow < 5)
                    {
                        straightInARow++;
                    }
                    if (straightInARow == 5)
                    {
                        return_value = value;
                    }
                    //System.out.println("increased int to "+straightInARow);
                    if (straightInARow >= 5)
                    {
                        hasStraight = true;
                    }
                }
                else
                {
                    straightInARow = 0;
                    //System.out.println("reset int");
                }
            }
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                if (ca.CheckIfCardExists(new Card(1, suit)) && straightInARow >= 4)
                {
                    return 1;
                }
            }
            if (hasStraight)
            {
                //System.out.println("im returning "+return_value);
                return return_value;
            }
            else
            {
                return 0;
            }
        };

        AnalyzeOp ThreeOfAKind = (CardAnalyzer ca) =>
        {
            int[] instanceCounter = ca.countInstancesByValue();
            int i = 0;
            foreach (int instances in instanceCounter)
            {
                i++;
                if (instances >= 3)
                {
                    return i;
                }
            }
            return 0;
        };

        AnalyzeOp TwoPair = (CardAnalyzer ca) =>
        {
            int[] instanceCounter = ca.countInstancesByValue();
            for (int i = 0; i < instanceCounter.Length; i++)
            {
                if (instanceCounter[i] >= 2)
                {
                    for (int j = 0; j < instanceCounter.Length; j++)
                    {
                        if (instanceCounter[j] >= 2 && i != j)
                        {

                            return i > j ? i : j;
                        }
                    }
                }
            }
            return 0;
        };

        AnalyzeOp OnePair = (CardAnalyzer ca) =>
        {
            int[] instanceCounter = ca.countInstancesByValue();
            int i = 0;
            foreach (int instances in instanceCounter)
            {
                i++;
                if (instances >= 2)
                {
                    return i;
                }

            }
            return 0;
        };

        AnalyzeOp HighCard = (CardAnalyzer ca) =>
        {
            int[] instances = ca.countInstancesByValue();
            for (int i = instances.Length - 1; i >= 0; i--)
            {
                if (instances[i] > 0)
                {
                    return i;
                }
            }
            return 0; //Should Never Get Here!!
        };

        public PlayerHand tieBreaker(HandRank rank, PlayerHand hand1, PlayerHand hand2)
        {
            this.hand = hand1;
            int hand1rank = map[rank](this);
            this.hand = hand2;
            int hand2rank = map[rank](this);
            if (hand1rank > hand2rank)
            {
                return hand1;
            }
            else if (hand2rank > hand1rank)
            {
                return hand2;
            }
            else
            {
                return null;
            }
        }

        private int[] countInstancesByValue()
        {
            int[] instanceCounter = new int[15];
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                for (int value = 1; value <= ACE; value++)
                {
                    if (CheckIfCardExists(new Card(value, suit)))
                    {
                        instanceCounter[value]++;
                    }
                }
            }
            return instanceCounter;
        }

        private int[] countInstancesBySuit()
        {
            int[] instanceCounter = new int[4];
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                for (int value = 1; value <= ACE; value++)
                {
                    if (CheckIfCardExists(new Card(value, suit)))
                    {
                        instanceCounter[(int)suit]++;
                    }
                }
            }
            return instanceCounter;
        }

        private bool CheckIfCardExists(Card card)
        {
            foreach (Card cardFromTable in cardArray)
            {
                if (card.equals(cardFromTable))
                {
                    return true;
                }
            }
            if (card.equals(hand.getFirst()))
            {
                return true;
            }
            if (card.equals(hand.getSecond()))
            {
                return true;
            }

            //System.out.println("Card "+card.getValue()+" of Suite "+card.getSuit()+ " doesnt exist");
            return false;
        }

        public PlayerHand GetHand()
        {
            return hand;
        }

    }



}