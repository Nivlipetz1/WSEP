using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;
using GameSystem;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class GameCenterService : GameCenterInterface
    {
        private GameCenterInterface gc = GameCenter.GameCenterFactory.getInstance();

        public ClientGame createGame(GamePreferences preferecnces, UserProfile user)
        {
            return new ClientGame(gc.createGame(preferecnces, user));
        }

        public List<ClientGame> getActiveGames(string criterion, object param, UserProfile user)
        {
            return gc.getActiveGames(criterion, param, user).Select(game => new ClientGame(game)).ToList();
        }

        public List<List<Move>> getAllReplayesOfInActiveGames()
        {
            return gc.getAllReplayesOfInActiveGames();
        }

        public List<ClientGame> getAllSpectatingGames()
        {
            return gc.getAllSpectatingGames().Select(game => new ClientGame(game)).ToList();
        }

        public List<string> joinGame(int gameID, UserProfile u, int credit)
        {
            Game g = gc.getGameByID(gameID);
            gc.joinGame(g, u, credit);
            return g.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(g.GetSpectators()).Select(player1 => player1.GetUserName()).ToList();
        }

        public List<string> spectateGame(int gameID, UserProfile u)
        {
            Game g = gc.getGameByID(gameID);
            gc.spectateGame(g, u);
            return g.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(g.GetSpectators()).Select(player1 => player1.GetUserName()).ToList();
        }
    }
}
