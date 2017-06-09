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
        private List<int> gainPerRound;

        public PlayingUser(string name, int credit, Game game) : base (name,game)
        {
            this.credit = credit;
            status = "Active";
            roundsWon = 0;
            roundsLost = 0;
            biggestPotWon = 0;
            bestHand = CardAnalyzer.HandRank.HighCard;
            userInput = new UserInput();
            gainPerRound = new List<int>();
        }

        public void SetFakeUserInput(Queue<string> inputs)
        {
            userInput = new FakeInput(inputs);
        }

        public bool setInput(string input)
        {
            return userInput.setInput(input);
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
        public List<int> GainPerRound
        {
            get { return gainPerRound; }
            set { gainPerRound = value; }
        }

        public int GetBlind(int amount)
        {
            status = "Active";
            credit -= amount;
            return amount;
        }

        public int Bet(int minimumBet)
        {
            NotificationService.Instance.pushYourTurn(GetUserName(), minimumBet, game.getGameID());
            string input;
            int betInput;

            while(true)
            {
                input = userInput.GetInput();

                if (input.Equals("Fold"))
                {
                    status = "Fold";
                    return -1;
                }
                else{
                    if (Int32.TryParse(input, out betInput))
                    {
                        if (betInput > credit)
                        {
                            userInput.setAccepted(false);
                            continue;
                        }
                    }
                    else
                    {
                        userInput.setAccepted(false);
                        continue;
                    }

                    break;
                }
            }
            userInput.setAccepted(true);
            status = "Talked";

            if (credit == 0)
            {
                status = "AllIn";
            }

            if (betInput > 0)
            {
                credit -= betInput;
                gainPerRound[gainPerRound.Count-1] -= betInput;
            }
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
            gainPerRound[gainPerRound.Count - 1] += amount;
            
        }

        internal void SetFoldUserInput()
        {
            userInput = new FakeInput("Fold");
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
        public FakeInput(string input)
        {
            this.inputs = new Queue<string>();
            inputs.Enqueue(input);
        }
        public string GetInput()
        {
            if (inputs.Count > 0)
            {
                input = inputs.Dequeue();
            }
            return input;
        }

        public void setAccepted(bool accepted)
        {
            //throw new NotImplementedException();
        }


        public bool setInput(string minimumBet)
        {
            //throw new NotImplementedException();
            return true;
        }
    }

}
