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
    public class GameCenterService
    {
        private GameCenterInterface gc = GameCenter.GameCenterFactory.getInstance();
        private TexasHoldemSystem system = TexasHoldemSystem.userSystemFactory.getInstance();

        public ClientGame createGame(GamePreferences preferecnces, ClientUserProfile user)
        {
            return new ClientGame(gc.createGame(preferecnces, system.getUser(user.Username)));
        }

        public List<ClientGame> getActiveGames(string criterion, object param, ClientUserProfile user)
        {
            return gc.getActiveGames(criterion, param, system.getUser(user.Username)).Select(game => new ClientGame(game)).ToList();
        }

        public List<List<Move>> getAllReplayesOfInActiveGames()
        {
            return gc.getAllReplayesOfInActiveGames();
        }

        public List<ClientGame> getAllSpectatingGames()
        {
            return gc.getAllSpectatingGames().Select(game => new ClientGame(game)).ToList();
        }

        public List<string> joinGame(int gameID, ClientUserProfile user, int credit)
        {
            Game g = gc.getGameByID(gameID);
            if (!gc.joinGame(g, system.getUser(user.Username), credit))
                return null;
            return g.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(g.GetSpectators()).Select(player1 => player1.GetUserName()).ToList();
        }

        public List<string> spectateGame(int gameID, ClientUserProfile user)
        {
            Game g = gc.getGameByID(gameID);
            if (!gc.spectateGame(g, system.getUser(user.Username)))
                return null;
            return g.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(g.GetSpectators()).Select(player1 => player1.GetUserName()).ToList();
        }
    }
}
