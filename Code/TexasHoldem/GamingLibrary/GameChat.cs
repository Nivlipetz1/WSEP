using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class GameChat
    {
        Game g;
        public GameChat(Game g)
        {
            this.g = g;
        }

        public List<string> SendMessage(string sender,string message)
        {
            List<string> usersToSendTo = new List<string>();
            if (isPlayer(sender))
            {
                foreach (SpectatingUser su in g.GetSpectators())
                {
                    su.PushMessage(message);
                    usersToSendTo.Add(su.GetUserName());
                }
                foreach (SpectatingUser su in g.GetPlayers())
                {
                    su.PushMessage(message);
                    usersToSendTo.Add(su.GetUserName());
                }
            }
            else
            {
                foreach (SpectatingUser su in g.GetSpectators())
                {
                    su.PushMessage(message);
                    usersToSendTo.Add(su.GetUserName());
                }
            }
            return usersToSendTo;
        }

        private bool isPlayer(string sender)
        {
            foreach(SpectatingUser user in g.GetPlayers())
            {
                if (user.GetUserName().Equals(sender))
                    return true;
            }
            return false;
        }

        public List<string> SendPMMessage(string from,string to,string message)
        {
            List<string> usersToSendTo = new List<string>();
            foreach (SpectatingUser su in g.GetSpectators())
            {
                if (su.GetUserName().Equals(to))
                {
                    //su.PushMessage(message);
                    usersToSendTo.Add(su.GetUserName());
                }
            }
            foreach (SpectatingUser su in g.GetPlayers())
            {
                if (su.GetUserName().Equals(to))
                {
                    //su.PushMessage(message);
                    usersToSendTo.Add(su.GetUserName());
                }
            }
            return usersToSendTo;
        }

    }
}
