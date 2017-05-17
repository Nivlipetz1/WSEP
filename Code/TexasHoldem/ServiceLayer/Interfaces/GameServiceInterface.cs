using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
      public interface GameServiceInterface
    {
         bool bet(string user, int gameID, string minimumBet);    
           
         List<string> removePlayer(string user, int gameID);

         List<string> removeSpectator(string user, int gameID);

         List<string> postMessage(string user, string message, int gameID);

         List<string> postWhisperMessage(string from, string to, string message, int gameID);
    }
}
