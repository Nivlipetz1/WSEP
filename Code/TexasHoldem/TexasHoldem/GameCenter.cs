using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;
using Gaming;

namespace TexasHoldemSystem
{
    class GameCenter
    {
        List<Game> games = new List<Game>();


        public bool createGame(GamePreferences preferecnces, user creatingUser, int buyIn)
        {
            Game game = new Game(preferecnces, creatingUser, buyIn);
            games.Add(game);
            return true;
        }

        public List<Game> getAllSpectatingGames(user u)
        {
            return games;
        }

        public List<Game> getAllActiveGamesByPlayerName(String playerName)
        {
            List<Game> activeGames = new List<Game>();
            foreach (Game game in games)
            {
                List<user> players = game.getPlayers();
                if (players.Where(u => ((user)u).name == playerName).ToList().Count > 0)
                    activeGames.Add(game);
            }

            return activeGames;
        }

        public List<Game> getAllActiveGamesByPotSize(int potSize)
        {
            return games.Where(game => ((Game)game).potSize == potSize).ToList();
        }

        public List<Game> getAllActiveGamesByGamePreference(GamePreferences preferences)
        {
            return games.Where(game => ((Game)game).preferecnces.Equals(preferences)).ToList();
        }

        public void joinGame(Game game, user u)
        {
            game.addPlayer(u);
        }

        public void spectateGame(Game game, user u)
        {
            game.addSpectator(u);
        }
    }
}
