using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;
using ServiceLayer.Models;
using ServiceLayer.Interfaces;

namespace AT.Stubs
{
    class GameCenterStub : GCServiceInterface
    {
        
        public ClientGame createGame(GamePreferences preferecnces, string user)
        {
            return new ClientGame(new Game(preferecnces));
        }

        public List<ClientGame> getActiveGames(string criterion, object param, string user)
        {
            return null;
        }

        public List<List<Move>> getAllReplayesOfInActiveGames()
        {
            return null;
        }

        public List<ClientGame> getAllSpectatingGames()
        {
            return null;
        }

        public List<string> joinGame(int gameID, string u, int credit)
        {
            return null;
        }

        public List<string> spectateGame(int gameID, string u)
        {
            return null;
        }

        public ClientGame getGameById(int gameId)
        {
            return null;
        }

        public List<Move> getReplayByGameId(int gameId)
        {
            return null;
        }
    }
}
