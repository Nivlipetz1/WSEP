using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class GamePreferences
    {
        private int maxPlayers;
        private int minPlayers;
        private int smallBlind;
        private int bigBlind; //== minimum bet
        private int typePolicy;
        private int buyInPolicy;
        private int chipPolicy;
        private Boolean allowSpectators;
        private string status;

        //typePolicy:
        const int LIMIT = 1;
        const int NO_LIMIT = 2;
        const int POT_LIMIT = 3;
        
        public GamePreferences(int maxP, int minP, int sB, int bB, int tP, int bIP, int cP, Boolean aS)
        {
            if (maxP > 8)
                throw new InvalidOperationException("Maximum number of players must be at most 8");
            if (minP < 2)
                throw new InvalidOperationException("Minimum number of players must be atleast 2");
            if (minP> maxP)
                throw new InvalidOperationException("Maximum number of players must be greater then minimum players");

            maxPlayers = maxP;
            minPlayers = minP;
            smallBlind = sB;
            bigBlind = bB;
            typePolicy = tP;
            buyInPolicy = bIP;
            chipPolicy = cP;
            allowSpectators = aS;
            status = "init";
        }

        //testing
        public GamePreferences()
        {
            maxPlayers = 6;
            minPlayers = 2;
            smallBlind = 5;
            bigBlind = 10;
            allowSpectators = true;
            buyInPolicy = 50;
            status = "init";
        }

        public string GetStatus()
        {
            return status;
        }

        public void SetStatus(string status)
        {
            this.status = status;
        }

        public Boolean AllowSpec()
        {
            return allowSpectators;
        }

        public int GetMaxPlayers(){
            return maxPlayers;
        }
        public int GetMinPlayers()
        {
            return minPlayers;
        }

        public int GetbB()
        {
            return bigBlind;
        }

        public int GetsB()
        {
            return smallBlind;
        }

        public int GetTypePolicy()
        {
            return typePolicy;
        }

        public int GetChipPolicy()
        {
            return chipPolicy;
        }

        public int GetBuyInPolicy()
        {
            return buyInPolicy;
        }

        public void setSmallBlind(int sb)
        {
            smallBlind = sb;
        }

        public void setBigBlind(int bb)
        {
            bigBlind = bb;
        }

        public void SetAllowSpec(bool aS)
        {
            allowSpectators = aS;
        }
    }
}
