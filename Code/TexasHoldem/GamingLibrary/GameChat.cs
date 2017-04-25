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

        public void SendMessage(string message)
        {
            foreach(SpectatingUser su in g.GetSpectators())
            {
                su.PushMessage(message);
            }
            foreach (SpectatingUser su in g.GetPlayers())
            {
                su.PushMessage(message);
            }
        }

    }
}
