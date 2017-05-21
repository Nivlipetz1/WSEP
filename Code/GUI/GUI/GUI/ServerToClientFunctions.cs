using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public interface ServerToClientFunctions
    {
       void PushHand(Models.PlayerHand hand, int gameID);
       void Notify(string message);
       void PushMoveToGame(Models.Move move, int gameID);
       void NotifyTurn(int minimumBet, int gameID);
       void PushPMMessage(int gameId, string sender, string message);
       void PushChatMessage(int gameId, string sender, string message);
        void PlayerJoinedGame(int gameID, Models.ClientUserProfile prof);
        void PushWinners(List<string> winners, int gameID);
        //Missing Functions:
        //a player has joined the game
        //a playe has quit the game
        //a spectator has joined the game
        // a spectator has left the game
        //show the winnerss of a round

    }
}
