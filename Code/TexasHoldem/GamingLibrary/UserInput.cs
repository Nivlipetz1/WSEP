using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    class UserInput : UserInputAPI
    {
        private string amount;
        private bool hasInputBet = false;

        public string GetInput()
        {
            while(!hasInputBet);
            hasInputBet = false;
            return amount;
        }

        public void setInput(string minimumBet)
        {
            amount = minimumBet;
            hasInputBet = true;
        }
    }
}
