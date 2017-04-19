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

        public SpectatingUser(UserProfile user, Game game)
        {
            account = user;
            this.game = game;
        }


        public UserProfile GetAccount()
        {
            return account;
        }

        public void PushMove(Move move)
        {
            //Got the last move;
        }
    }
}
