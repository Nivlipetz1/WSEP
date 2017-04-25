using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Gaming;
using GameUtilities;

namespace TexasHoldemSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game(new GamePreferences());
            GameLogger logger = g.GetLogger();

            UserProfile Niv = new UserProfile("Niv", "123");
            UserProfile Omer = new UserProfile("Omer", "456");
            UserProfile Naor = new UserProfile("Naor", "789");

            PlayingUser nivPlayer = new PlayingUser(Niv, 1000, g);
            PlayingUser OPlayer = new PlayingUser(Omer, 1000, g);
            PlayingUser NPlayer = new PlayingUser(Naor, 1000, g);

            g.addPlayer(nivPlayer);
            g.addPlayer(OPlayer);
            g.addPlayer(NPlayer);

            nivPlayer.SetFakeUserInput(new Queue<string>(new[] { "5","0", "-1" }));
            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "0" , "0","-1" }));
            NPlayer.SetFakeUserInput(new Queue<string>(new[] { "10", "100" }));


            ThreadStart childref = new ThreadStart(g.StartGame);
            Thread childThread = new Thread(childref);
            childThread.Start();
            OPlayer.SendMessage("hello world");
            nivPlayer.SendMessage("Got That!");
            //g.StartGame();
            int num_of_moves = g.GetLogger().GetMoves().Count;
            childThread.Join();

            Console.Write("");

        }
    }
}
