using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    interface UserInputAPI
    {
        string GetInput();
        bool setInput(string minimumBet);
        void setAccepted(bool accepted);
    }
}
