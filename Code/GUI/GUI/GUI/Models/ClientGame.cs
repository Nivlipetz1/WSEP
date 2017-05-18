using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class ClientGame
    {
        private int id;
        private Deck gameDeck;
        private List<ClientUserProfile> players;
        private List<ClientUserProfile> spectators;
        private int[] pot;
        private GamePreferences gamePref;
        private IDictionary<ClientUserProfile, int> playerBets = new Dictionary<ClientUserProfile, int>();

        public Deck GameDeck
        {
            get
            {
                return gameDeck;
            }

            set
            {
                gameDeck = value;
            }
        }

        public List<ClientUserProfile> Players
        {
            get
            {
                return players;
            }

            set
            {
                players = value;
            }
        }

        public int[] Pot
        {
            get
            {
                return pot;
            }

            set
            {
                pot = value;
            }
        }

        public GamePreferences GamePref
        {
            get
            {
                return gamePref;
            }

            set
            {
                gamePref = value;
            }
        }

        public IDictionary<ClientUserProfile, int> PlayerBets
        {
            get
            {
                return playerBets;
            }

            set
            {
                playerBets = value;
            }
        }

        public List<ClientUserProfile> Spectators
        {
            get
            {
                return spectators;
            }

            set
            {
                spectators = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }
}
