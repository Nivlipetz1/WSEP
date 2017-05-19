using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class Statistics
    {
        public int winnings { set; get; }
        public int losses { set; get; }
        public CardAnalyzer.HandRank highestHand { set; get; }
        public int biggestWin { set; get; }
        public int biggestWallet { set; get; }
    }
}

