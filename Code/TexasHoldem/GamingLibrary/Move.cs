using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;

namespace Gaming
{
    public abstract class Move
    {        
        public abstract void update(ref IDictionary<string, int> playerBets, ref Card[] cards,ref IDictionary<string, PlayerHand> playerHands);
    }
}