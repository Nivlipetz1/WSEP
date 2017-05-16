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

        public ClientGame createGame(GamePreferences preferecnces, string userName)
        {
            return new ClientGame(gc.createGame(preferecnces, system.getUser(userName)));
        }

        public List<ClientGame> getActiveGames(string criterion, object param, string userName)
        {
            return gc.getActiveGames(criterion, param, system.getUser(userName)).Select(game => new ClientGame(game)).ToList();
        }

        public List<List<Move>> getAllReplayesOfInActiveGames()
        {
            return gc.getAllReplayesOfInActiveGames();
        }

        public List<ClientGame> getAllSpectatingGames()
        {
            return gc.getAllSpectatingGames().Select(game => new ClientGame(game)).ToList();
        }

        public List<string> joinGame(int gameID, string userName, int credit)
        {
            Game g = gc.getGameByID(gameID);
            if (!gc.joinGame(g, system.getUser(userName), credit))
                return null;
            return g.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(g.GetSpectators()).Select(player1 => player1.GetUserName()).ToList();
        }

        public List<string> spectateGame(int gameID, string userName)
        {
            Game g = gc.getGameByID(gameID);
            if (!gc.spectateGame(g, system.getUser(userName)))
                return null;
            return g.GetPlayers().ConvertAll(x => (SpectatingUser)x).Union(g.GetSpectators()).Select(player1 => player1.GetUserName()).ToList();
        }

        public bool unknownUserEditLeague(string userName, int minimumLeagueRank)
        {
            return gc.unknownUserEditLeague(system.getUser(userName), gc.getLeagueByID(minimumLeagueRank));
        }
    }
}
