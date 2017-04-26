using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming
{
    public class IllegalBetException : Exception
    {
        public IllegalBetException(string message)
        : base(message)
        {
        }
    }
}
