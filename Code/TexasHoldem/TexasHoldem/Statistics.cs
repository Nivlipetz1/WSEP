using Gaming;

namespace GameSystem
{
    public class Statistics
    {
        private int winnings;
        private int losses;
        private int totalGames;
        private CardAnalyzer.HandRank highestHand;
        private int biggestWin;
        private int biggestWallet;

        private int totalGrossProfit; 
        private double avgGrossProfit; //Player gross profit win rate
        private double avgCashGain; //Player $ win rate.
        

        public Statistics()
        {
            winnings = 0;
            losses = 0;
            biggestWin = 0;
            biggestWallet = 0;
        }

        public int Winnings
        {
            get
            {
                return winnings;
            }

            set
            {
                winnings = value;
            }
        }

        public int Losses
        {
            get
            {
                return losses;
            }

            set
            {
                losses = value;
            }
        }

        public int TotalGames
        {
            get
            {
                return winnings + losses;
            }

            set
            {
                totalGames = value;
            }
        }

        public CardAnalyzer.HandRank HighestHand
        {
            get
            {
                return highestHand;
            }

            set
            {
                highestHand = value;
            }
        }

        public int BiggestWin
        {
            get
            {
                return biggestWin;
            }

            set
            {
                biggestWin = value;
            }
        }

        public int BiggestWallet
        {
            get
            {
                return biggestWallet;
            }

            set
            {
                biggestWallet = value;
            }
        }

        public int TotalGrossProfit
        {
            get { return totalGrossProfit; }
            set { totalGrossProfit = value; }
        }
        
        public double AvgGrossProfit
        {
            get { return avgGrossProfit; }
            set { avgGrossProfit = value; }
        }
        public double AvgCashGain
        {
            get { return avgCashGain; }
            set
            {
                avgCashGain = value;
            }
        }

    }
}