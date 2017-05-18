using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
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
            if (minP > maxP)
                throw new InvalidOperationException("Minimum number of players must be greater then maximum players");

            maxPlayers = maxP;
            minPlayers = minP;
            smallBlind = sB;
            bigBlind = bB;
            typePolicy = tP;
            buyInPolicy = bIP;
            chipPolicy = cP;
            allowSpectators = aS;
            status = "active";
        }

        public int GameID
        {
            get
            {
                return gameID;
            }

            set
            {
                gameID = value;
            }
        }

        public int MaxPlayers
        {
            get
            {
                return maxPlayers;
            }

            set
            {
                maxPlayers = value;
            }
        }

        public int MinPlayers
        {
            get
            {
                return minPlayers;
            }

            set
            {
                minPlayers = value;
            }
        }

        public int SmallBlind
        {
            get
            {
                return smallBlind;
            }

            set
            {
                smallBlind = value;
            }
        }

        public int BigBlind
        {
            get
            {
                return bigBlind;
            }

            set
            {
                bigBlind = value;
            }
        }

        public int TypePolicy
        {
            get
            {
                return typePolicy;
            }

            set
            {
                typePolicy = value;
            }
        }

        public int BuyInPolicy
        {
            get
            {
                return buyInPolicy;
            }

            set
            {
                buyInPolicy = value;
            }
        }

        public int ChipPolicy
        {
            get
            {
                return chipPolicy;
            }

            set
            {
                chipPolicy = value;
            }
        }

        public bool AllowSpectators
        {
            get
            {
                return allowSpectators;
            }

            set
            {
                allowSpectators = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}
