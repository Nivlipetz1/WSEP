using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceLayer.Interfaces;
using ServiceLayer;
using ServiceLayer.Models;
using AT.Stubs;
using Gaming;

namespace AT
{
    [TestFixture]
    class GamePlayAT
    {
        GameServiceInterface gs;
        SystemService us;
        GameCenterService gc;
        string userName, Password;
        int gameID;

        [SetUp]
        public void Before()
        {
            if (GameService.testable)
                gs = new GameService();
            else
                gs = new GameServiceStub();
            us = new SystemService();
            gc = new GameCenterService();

            userName = "Ohad";
            Password = "1234";
            us.register(userName, Password);
            us.login(userName, Password);
            GamePreferences pref = new GamePreferences(8, 2, 5, 10, 1, 2, 3, true);
            gameID = gc.createGame(pref, userName).getID();
        }
    }
}
