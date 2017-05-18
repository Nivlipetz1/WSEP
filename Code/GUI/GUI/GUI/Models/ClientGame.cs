using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class ClientGame
    {
        public int id { get; set; }
        public Deck gameDeck { get; set; }
        public List<ClientUserProfile> players { get; set; }
        public List<ClientUserProfile> spectators { get; set; }
        public int[] pot { get; set; }
        public GamePreferences gamePref { get; set; }
        public IDictionary<ClientUserProfile, int> playerBets { get; set; }

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
