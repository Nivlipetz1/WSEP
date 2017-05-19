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
        public bool bet(string user, int gameID, string minimumBet)
        {
            return true;
        }

        public List<string> postMessage(string user, string message, int gameID)
        {
            return null;
        }

        public List<string> postWhisperMessage(string from, string to, string message, int gameID)
        {
            return null;
        }

        public List<string> removePlayer(string user, int gameID)
        {
            return null;
        }

        public List<string> removeSpectator(string user, int gameID)
        {
            return null;
        }
    }
}
