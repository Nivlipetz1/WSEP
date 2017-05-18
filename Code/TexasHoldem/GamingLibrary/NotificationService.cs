using Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class NotificationService
    {
        private static Lazy<NotificationService> LazyInstance = new Lazy<NotificationService>(() => new NotificationService(), true);

        public delegate void NotifyUser(string userName, string message);
        public static event NotifyUser notifyUserEvt;

        public delegate void NotifyAllUsers(string message);
        public static event NotifyAllUsers notifyAllUsesrEvt;

        public delegate void PushMove(List<string> userNames, Move move, int gameId);
        public static event PushMove pushMoveEvt;

        public delegate void PushWinners(List<string> userNames, List<string> winners, int gameId);
        public static event PushWinners pushWinnersEvt;

        public delegate void YourTurn(string userName , int minimumBet , int gameId);
        public static event YourTurn pushYourTurnEvt;

        public delegate void SetHand(string userName, PlayerHand hand , int gameId);
        public static event SetHand setHandEvt;

        private NotificationService()
        {

        }
        public static NotificationService Instance
        {
            get { return LazyInstance.Value; }
        }

        public void notifyAllUsers(string message)
        {
            var e = notifyAllUsesrEvt;
            if (e != null)
                e(message);
        }

        public void notifyUser(string userName , string message)
        {
            var e = notifyUserEvt;
            if (e != null)
                e(userName , message);
        }

        public void pushMove(List<string> userNames , Move move , int gameId)
        {
            var e = pushMoveEvt;
            if (e != null)
                e(userNames, move , gameId);
        }

        public void pushWinners(List<string> userNames, List<string> winners, int gameId)
        {
            var e = pushWinnersEvt;
            if (e != null)
                e(userNames, winners , gameId);
        }

        public void pushYourTurn(string userName , int minimumBet , int gameId)
        {
            var e = pushYourTurnEvt;
            if (e != null)
                e(userName,minimumBet,gameId);
        }

        public void setHand(string userName, PlayerHand playerHand , int gameId)
        {
            var e = setHandEvt;
            if (e != null)
                e(userName, playerHand, gameId);
        }
    }
}
