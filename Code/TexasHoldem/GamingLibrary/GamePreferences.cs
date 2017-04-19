using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class GamePreferences
    {
        private int gameID;
        private int maxPlayers;
        private int minPlayers;
        private int smallBlind;
        private int bigBlind; //== minimum bet
        private int typePolicy;
        private int buyInPolicy;
        private int chipPolicy;
        private Boolean allowSpectators;

        //typePolicy:
        const int LIMIT = 1;
        const int NO_LIMIT = 2;
        const int POT_LIMIT = 3;
        
        public GamePreferences(int maxP, int minP, int sB, int bB, int tP, int bIP, int cP, Boolean aS)
        {
            maxPlayers = maxP;
            minPlayers = minP;
            smallBlind = sB;
            bigBlind = bB;
            typePolicy = tP;
            buyInPolicy = bIP;
            chipPolicy = cP;
            allowSpectators = aS;
        }

        public GamePreferences()
        {

        }

        public Boolean AllowSpec()
        {
            return allowSpectators;
        }

        public int GetMax(){
            return maxPlayers;
        }

        public int GetbB()
        {
            return bigBlind;
        }

        public int GetsB()
        {
            return bigBlind;
        }

        public void setSmallBlind(int sb)
        {
            smallBlind = sb;
        }

        public void setBigBlind(int bb)
        {
            bigBlind = bb;
        }
    }
}
