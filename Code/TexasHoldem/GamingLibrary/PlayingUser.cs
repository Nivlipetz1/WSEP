using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class PlayingUser : SpectatingUser
    {
        private int credit;
        private string status;
        private PlayerHand hand;
        private UserInputAPI userInput;
        private CardAnalyzer.HandRank bestHand;
        private int roundsWon;
        private int roundsLost;
        private int biggestPotWon;

        public PlayingUser(string name, int credit, Game game) : base (name,game)
        {
            this.credit = credit;
            status = "Active";
            roundsWon = 0;
            roundsLost = 0;
            biggestPotWon = 0;
            bestHand = CardAnalyzer.HandRank.HighCard;
        }

        public void SetFakeUserInput(Queue<string> inputs)
        {
            userInput = new FakeInput(inputs);
        }

        public void setInput(string input)
        {
            userInput.setInput(input);
        }

        public int GetRoundsWon()
        {
            return roundsWon;
        }

        public int GetRoundsLost()
        {
            return roundsLost;
        }

        public void SetRoundsLost(int timesLost)
        {
            roundsLost = timesLost;
        }

        public void SetBestHand(CardAnalyzer.HandRank hr)
        {
            bestHand = hr;
        }

        public CardAnalyzer.HandRank GetBestHand()
        {
            return bestHand;
        }

        public int GetMostWon()
        {
            return biggestPotWon;
        }


        public int GetCredit()
        {
            return credit;
        }

        public void SetHand(PlayerHand hand)
        {
            this.hand = hand;
        }

        public PlayerHand GetHand()
        {
            return hand;
        }

        public String GetStatus()
        {
            return status;
        }

        public void SetStatus(string st)
        {
            status = st;
        }

        public int GetBlind(int amount)
        {
            status = "Active";
            credit -= amount;
            return amount;
        }

        public int Bet(int minimumBet)
        {
            string input;
            int betInput;

            do
            {
                input = userInput.GetInput();

                if (input.Equals("Fold"))
                {
                    status = "Fold";
                    return -1;
                }
            }
            while (!Int32.TryParse(input, out betInput) && !(input.Equals("Fold")));

            status = "Talked";

            if (credit == 0)
            {
                status = "AllIn";
            }

            if(betInput>0)
               credit -= betInput;
            return betInput;
        }

        public void PostMessage()
        {
            game.Message(this, userInput.GetInput());
        }

        public int BadBet(int returnAmount ,int minimumBet)
        {
            credit += returnAmount;
            return Bet(minimumBet);
        }

        internal void ReceiveWinnings(int amount)
        {
            if (amount > biggestPotWon)
            {
                biggestPotWon = amount;
            }

            roundsWon++;
            credit+=amount;
        }
   }

    class FakeInput : UserInputAPI
    {
        Queue<string> inputs;
        string input = "0";
        public FakeInput(Queue<string> inputs)
        {
            this.inputs = inputs;
        }
        public string GetInput()
        {
            if (inputs.Count > 0)
            {
                input = inputs.Dequeue();
            }
            return input;
        }

        public void setInput(string minimumBet)
        {
            throw new NotImplementedException();
        }
    }

}
