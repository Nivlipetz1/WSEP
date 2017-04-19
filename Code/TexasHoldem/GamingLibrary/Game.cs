using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;

namespace Gaming
{
    public class Game
    {
        public string name;
        public int potSize;
        public GamePreferences preferecnces;
        private int buyIn;
        private user creatingUser;

        public Game(string name)
        {
            this.name = name;
        }

        public Game(GamePreferences preferecnces, user creatingUser, int buyIn)
        {
            this.preferecnces = preferecnces;
            this.creatingUser = creatingUser;
            this.buyIn = buyIn;
        }

        public List<user> getPlayers()
        {
            throw new NotImplementedException();
        }

        public void addPlayer(user u)
        {
            throw new NotImplementedException();
        }

        public void addSpectator(user u)
        {
            throw new NotImplementedException();
        }
    }
}
