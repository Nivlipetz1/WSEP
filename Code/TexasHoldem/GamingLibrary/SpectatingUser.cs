using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;

namespace Gaming
{
    public class SpectatingUser
    {

        private UserProfile account;
        protected Game game;
        private Card[] cards;
        private IDictionary<string, int> playerBets;
        private IDictionary<string, PlayerHand> playerHands;

        public SpectatingUser(UserProfile user, Game game)
        {
            account = user;
            this.game = game;
        }


        public UserProfile GetAccount()
        {
            return account;
        }

        public void PushMove(Move m)
        {
            m.update(ref playerBets,ref cards, ref playerHands);
        }

        public void DisplayCards(){
            Console.Write(cards[2].toString());
        }


    }
}
