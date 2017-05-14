using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;
using Gaming;

namespace ServiceLayer.Models
{
    class ClientGame
    {
        private GameSystem.TexasHoldemSystem system = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();
        private int id;
        private Deck gameDeck;
        private List<UserProfile> players;
        private List<UserProfile> spectators;
        private int[] pot;
        private GamePreferences gamePref;
        private IDictionary<UserProfile, int> playerBets = new Dictionary<UserProfile, int>();

        public ClientGame(Game game)
        {
            id = game.getGameID();
            gamePref = game.GetGamePref();
            pot = game.getPot();
            gameDeck = game.getDeck();

            players = game.GetPlayers().Select(pl => system.getUser(pl.GetUserName())).ToList();
            spectators = game.GetSpectators().Select(pl => system.getUser(pl.GetUserName())).ToList();

            foreach (KeyValuePair<PlayingUser,int> p in game.getplayerBets())
                playerBets.Add(new KeyValuePair<UserProfile, int>(system.getUser(p.Key.GetUserName()), p.Value));
        }

        public int getID()
        {
            return id;
        }
    }
}
