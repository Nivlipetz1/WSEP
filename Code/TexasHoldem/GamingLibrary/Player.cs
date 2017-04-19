using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    interface Player
    {
        int Bet(int minAmt);
        void PostMessage(string msg);
        PlayerHand GetHand();
        string GetStatus();
        int GetBlind(int blindAmt);
        int GetCredit();
    }
}
