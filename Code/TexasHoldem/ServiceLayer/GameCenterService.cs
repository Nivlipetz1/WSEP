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

        public Game createGame(GamePreferences preferecnces, UserProfile user)
        {
            return gc.createGame(preferecnces, user);
        }

        public List<Game> getActiveGames(string criterion, object param, UserProfile user)
        {
            return gc.getActiveGames(criterion, param, user);
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
