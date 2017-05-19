using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    interface ClientToServerFunctions
    {
        void Register(string username, string password);
        void EditProfile(string username, string password);
        void ConnectToServer();
        bool PostChatMessage(string message, int gameID);
        void CreateGame(Models.GamePreferences pref);
        void RefreshProfile();
        void Login(string username, string password);
        bool SendPMMessage(string to, string message, int gameID);
        void NotifyTurn(int minimumBet, int gameID);
        void JoinGame(int gameID, int credit);
        void PushHand(Models.PlayerHand hand, int gameID);
        void Notify(string message);
        void PushMoveToGame(Models.Move move, int gameID);
        void QuitGame(int gameID);
        void Bet(int gameID, int amount, int minimumBet, Game gameWindow);
        void Fold(int gameID, Game gameWindow);


    }
}
