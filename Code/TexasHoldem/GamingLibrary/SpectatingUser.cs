using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameUtilities;

namespace Gaming
{
    public class SpectatingUser
    {

        private UserProfile account;
        protected Game game;
        private Card[] cards;
        private IDictionary<string, int> playerBets;
        private IDictionary<string, PlayerHand> playerHands;
        private List<string> messages;

        public SpectatingUser(UserProfile user, Game game)
        {
            account = user;
            this.game = game;
            messages = new List<string>();
        }


        public UserProfile GetAccount()
        {
            return account;
        }

        public void PushMove(Move m)
        {
            m.update(ref playerBets,ref cards, ref playerHands);
        }

        public void PushMessage(string m)
        {
            messages.Add(m);
        }

        public void SendMessage(string m)
        {
            game.GetChat().SendMessage(m);
        }

        public List<string> GetMessages()
        {
            return messages;
        }
    }
}
