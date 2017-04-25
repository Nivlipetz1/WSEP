using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;
using GameUtilities;

namespace Services
{
    public abstract class GameAPI
    {
        public abstract void sendMessage();
        public abstract void receiveMessage();
    }
}
