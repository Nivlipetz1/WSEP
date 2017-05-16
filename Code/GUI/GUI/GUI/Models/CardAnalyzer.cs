using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class CardAnalyzer
    {
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
    }
}
