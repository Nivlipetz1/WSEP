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

        public delegate void SaveReplay(GameLogger replay);
        public static event SaveReplay saveReplayEvt;

        public delegate void PushDbDown();
        public static event PushDbDown pushDbDownEvt;

        public delegate void PushDbUp();
        public static event PushDbUp pushDbUpEvt;

        public delegate void UpdateCreditEvt(string userName , int credit);
        public static event UpdateCreditEvt updateCreditEvt;

        public delegate void pushRemovePlayer(string userName, int gameId , List<string> users);
        public static event pushRemovePlayer pushRemovePlayerEvt;

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

        public void dbDown()
        {
            var e = pushDbDownEvt;
            if (e != null)
                e();
        }

        public void dbUp()
        {
            var e = pushDbUpEvt;
            if (e != null)
                e();
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

        public void saveReplay(GameLogger replay)
        {
            var e = saveReplayEvt;
            if (e != null)
                e(replay);
        }

        public void updateCredit(string userName , int credit)
        {
            var e = updateCreditEvt;
            if (e != null)
                e(userName,credit);
        }

        public void removePlayer(string user , int gameId , List<string> usersToSend)
        {
            var e = pushRemovePlayerEvt;
            if (e != null)
                e(user, gameId , usersToSend);
        }
    }
}
