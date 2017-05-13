using Gaming;

namespace GameSystem
{
    public class Statistics
    {
        private int winnings;
        private int losses;
        private CardAnalyzer.HandRank highestHand;
        private int biggestWin;
        private int biggestWallet;

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

    }
}