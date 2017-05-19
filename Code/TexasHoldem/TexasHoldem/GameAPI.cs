using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;

namespace GameSystem
{
    public interface GameAPI
    {
        List<string> bet(UserProfile player, int gameID, string minimumBet);
        List<string> removePlayer(UserProfile player, int gameID);
        List<string> removeSpectator(UserProfile spec, int gameID);
        List<string> postMessage(UserProfile spec, string message, int gameID);
        List<string> postWhisperMessage(UserProfile from, UserProfile to, string message, int gameID);
    }
}
