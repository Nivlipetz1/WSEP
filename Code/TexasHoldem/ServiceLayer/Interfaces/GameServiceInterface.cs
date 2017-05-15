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
         List<string> bet(ClientUserProfile user, int gameID, string minimumBet);    
           
         List<string> removePlayer(ClientUserProfile user, int gameID);

         List<string> removeSpectator(ClientUserProfile user, int gameID);

         List<string> postMessage(ClientUserProfile user, string message, int gameID);

         List<string> postWhisperMessage(ClientUserProfile from, ClientUserProfile to, string message, int gameID);
    }
}
