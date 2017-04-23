﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaming;
using User;

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

            nivPlayer.SetFakeUserInput(5);
            OPlayer.SetFakeUserInput(0);
            NPlayer.SetFakeUserInput(10);

            g.StartGame();

            Console.Write("");

        }
    }
}
