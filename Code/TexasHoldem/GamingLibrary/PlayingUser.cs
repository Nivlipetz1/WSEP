using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;

namespace Gaming
{
    public class PlayingUser : SpectatingUser, Player
    {
        private int credit;
        private string status;
        private PlayerHand hand;

        public PlayingUser(UserProfile account, int credit, Game game) : base (account,game)
        {
            this.credit = credit;
            status = "Active";
        }

        public int GetCredit()
        {
            return credit;
        }

        public void setHand(PlayerHand hand)
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
                input = Console.ReadLine();
            while (Int32.TryParse(input, out betInput) && betInput>=minimumBet);

            //credit -=betInput; ?????
            return betInput;
        }

        public void PostMessage()
        {
            game.Message(this, Console.ReadLine());
        }
    }
}
