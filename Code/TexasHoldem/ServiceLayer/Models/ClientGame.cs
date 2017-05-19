using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;
using Gaming;

namespace ServiceLayer.Models
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

        public ClientGame(Game game)
        {
            GameSystem.TexasHoldemSystem system = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();
            id = game.getGameID();
            gamePref = game.GetGamePref();
            pot = game.getPot();
            gameDeck = game.getDeck();

            players = game.GetPlayers().Select(pl => new ClientUserProfile(system.getUser(pl.GetUserName()))).ToList();
            spectators = game.GetSpectators().Select(pl => new ClientUserProfile(system.getUser(pl.GetUserName()))).ToList();

            foreach (KeyValuePair<PlayingUser, int> p in game.getplayerBets())
                playerBets.Add(new KeyValuePair<ClientUserProfile, int>(new ClientUserProfile(system.getUser(p.Key.GetUserName())), p.Value));
        }

        public int getID()
        {
            return id;
        }

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
