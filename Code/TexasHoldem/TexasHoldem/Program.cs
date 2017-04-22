using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;

namespace TexasHoldemSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Game g = new Game("niv");
            Console.Write(g.name);*/
            PlayingUser omer = new PlayingUser(null, 50, null);
            omer.PushMove(new NewCardMove(new Card[] { null, null, new Card(4, Card.Suit.CLUB), null }));
            omer.DisplayCards();
            Console.Read();
        }
    }
}
