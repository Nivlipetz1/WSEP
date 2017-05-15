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
        public ClientGame createGame(GamePreferences preferecnces, ClientUserProfile user)
        {
            throw new NotImplementedException();
        }

        public List<ClientGame> getActiveGames(string criterion, object param, ClientUserProfile user)
        {
            throw new NotImplementedException();
        }

        public List<List<Move>> getAllReplayesOfInActiveGames()
        {
            throw new NotImplementedException();
        }

        public List<ClientGame> getAllSpectatingGames()
        {
            throw new NotImplementedException();
        }

        public List<string> joinGame(int gameID, ClientUserProfile u, int credit)
        {
            throw new NotImplementedException();
        }

        public List<string> spectateGame(int gameID, ClientUserProfile u)
        {
            throw new NotImplementedException();
        }
    }
}
