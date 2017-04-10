using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;

namespace TexasHoldem
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game("niv");
            Console.Write(g.name);
            Console.Read();
        }
    }
}
