using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class GamePreferences
    {
        public int maxPlayers { set; get; }
        public int minPlayers { set; get; }
        public int smallBlind { set; get; }
        public int bigBlind { set; get; } //== minimum bet
        public int typePolicy { set; get; }
        public int buyInPolicy { set; get; }
        public int chipPolicy { set; get; }
        public Boolean allowSpectators { set; get; }
        public string status { set; get; }

        //typePolicy:
        const int LIMIT = 1;
        const int NO_LIMIT = 2;
        const int POT_LIMIT = 3;

        public GamePreferences()
        {
            maxPlayers = 6;
            minPlayers = 2;
            smallBlind = 5;
            bigBlind = 10;
            allowSpectators = true;
            buyInPolicy = 50;
            status = "Init";
        }

        public GamePreferences(int maxP,int minP,int sB,int bB,int tP,int bIP,int cP,bool aS)
        {
            maxPlayers = maxP;
            minPlayers = minP;
            smallBlind = sB;
            bigBlind = bB;
            allowSpectators = aS;
            buyInPolicy = buyInPolicy;
            status = "Init";
            chipPolicy = cP;
            typePolicy = tP;
        }



    }
}
