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

        public void PrivateMessage(string message,string sender,SpectatingUser reciever)
        {
            reciever.PushPrivateMessage(message, sender);
        }

        public void SendMessage(string message,SpectatingUser sender)
        {
            if (sender is PlayingUser)
            {
                foreach (SpectatingUser su in g.GetSpectators())
                {
                    su.PushMessage(message);
                }
                foreach (SpectatingUser su in g.GetPlayers())
                {
                    su.PushMessage(message);
                }
            }
            else
            {
                foreach (SpectatingUser su in g.GetSpectators())
                {
                    su.PushMessage(message);
                }
            }
        }

    }
}
