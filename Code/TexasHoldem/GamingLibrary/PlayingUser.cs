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
        private UserInput userInput;

        public PlayingUser(string name, int credit, Game game) : base (name,game)
        {
            this.credit = credit;
            status = "Active";
        }

        public void SetFakeUserInput(Queue<string> inputs)
        {
            userInput = new FakeInput(inputs);
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
            credit -= amount;
            return amount;
        }

        public int Bet(int minimumBet)
        {
            string input;
            int betInput;
            string foldInput;
            do
                foldInput = userInput.GetInput();
            while (!(foldInput.Equals("Yes") || foldInput.Equals("No")));

            if (foldInput.Equals("Yes"))
                return -1;

            do
                input = userInput.GetInput();
            while (!Int32.TryParse(input, out betInput));

            status = "Talked";

            if (credit == 0)
            {
                status = "AllIn";
            }
            else if(betInput==-1){
                status = "Fold";
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
            credit+=amount;
        }
    }

    class FakeInput : UserInput
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
    }
}
