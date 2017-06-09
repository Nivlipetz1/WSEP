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
        private bool hasAnswer = false;
        private bool betPlaced = false;
        private bool betAccepted = false;

        public string GetInput()
        {
            while(!betPlaced);
            betPlaced = false;
            return amount;
        }

        public void setAccepted(bool accepted)
        {
            betAccepted = accepted;
            hasAnswer = true;
        }

        public bool setInput(string minimumBet)
        {
            amount = minimumBet;
            hasAnswer = false;
            betPlaced = true;
            while (!hasAnswer) ;
            return betAccepted;
        }
    }
}
