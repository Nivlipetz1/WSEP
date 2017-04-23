using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;

namespace Gaming
{
    public class PlayingUser : SpectatingUser
    {
        private int credit;
        private string status;
        private PlayerHand hand;
        private UserInput userInput;

        public PlayingUser(UserProfile account, int credit, Game game) : base (account,game)
        {
            this.credit = credit;
            status = "Active";
        }

        public void SetFakeUserInput(int amount)
        {
            userInput = new FakeInput(amount);
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

        internal void ReceiveWinnings(int amount)
        {
            credit+=amount;
        }
    }

    class FakeInput : UserInput
    {
        int amount;
        bool flag = true;
        public FakeInput(int amount)
        {
            this.amount = amount;
        }
        public string GetInput()
        {
            if (flag)
            {
                flag = false;
                return amount.ToString();

            }
            return "0";

        }
    }
}
