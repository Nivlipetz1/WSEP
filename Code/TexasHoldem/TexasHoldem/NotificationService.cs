using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem
{
    public class NotificationService
    {
        private static Lazy<NotificationService> LazyInstance = new Lazy<NotificationService>(() => new NotificationService(), true);
        public delegate void NotifyUser(string userName, string message);
        public static event NotifyUser notifyUserEvt;

        public delegate void NotifyAllUsers(string message);
        public static event NotifyAllUsers notifyAllUsesrEvt;

        private NotificationService()
        {

        }
        public static NotificationService Instance
        {
            get { return LazyInstance.Value; }
        }

        public void notifyAllUser(string message)
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
    }
}
