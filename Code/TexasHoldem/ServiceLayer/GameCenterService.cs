using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;
using GameSystem;

namespace ServiceLayer
{
    public class GameCenterService : GameCenterInterface
    {
        private GameCenterInterface gc = GameCenter.GameCenterFactory.getInstance();

        public Game createGame(GamePreferences preferecnces)
        {
            return gc.createGame(preferecnces);
        }

        public List<Game> getAllActiveGamesByGamePreference(GamePreferences preferences)
        {
            return gc.getAllActiveGamesByGamePreference(preferences);
        }

        public List<Game> getAllActiveGamesByPlayerName(string playerName)
        {
            return gc.getAllActiveGamesByPlayerName(playerName);
        }

        public List<Game> getAllActiveGamesByPotSize(int potSize)
        {
            return gc.getAllActiveGamesByPotSize(potSize);
        }

        public List<List<Move>> getAllReplayesOfInActiveGames()
        {
            return gc.getAllReplayesOfInActiveGames();
        }

        public List<Game> getAllSpectatingGames()
        {
            return gc.getAllSpectatingGames();
        }

        public bool joinGame(Game game, UserProfile u, int credit)
        {
            return gc.joinGame(game, u, credit);
        }

        public bool spectateGame(Game game, UserProfile u)
        {
            return gc.spectateGame(game, u);
        }
    }
}
