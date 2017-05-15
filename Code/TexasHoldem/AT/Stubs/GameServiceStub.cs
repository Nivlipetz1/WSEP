using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace AT.Stubs
{
    class GameServiceStub : GameServiceInterface
    {
        public List<string> bet(ClientUserProfile user, int gameID, string minimumBet)
        {
            throw new NotImplementedException();
        }

        public List<string> postMessage(ClientUserProfile user, string message, int gameID)
        {
            throw new NotImplementedException();
        }

        public List<string> postWhisperMessage(ClientUserProfile from, ClientUserProfile to, string message, int gameID)
        {
            throw new NotImplementedException();
        }

        public List<string> removePlayer(ClientUserProfile user, int gameID)
        {
            throw new NotImplementedException();
        }

        public List<string> removeSpectator(ClientUserProfile user, int gameID)
        {
            throw new NotImplementedException();
        }
    }
}
