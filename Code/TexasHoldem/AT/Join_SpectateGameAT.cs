using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;
using Gaming;
using NUnit.Framework;
using ServiceLayer;
using ServiceLayer.Models;
using ServiceLayer.Interfaces;
using AT.Stubs;

namespace AT
{
 
    [TestFixture]
    class Join_SpectateGameAT
    {
        UserProfile userProf;
        UserProfile userProf2;
        UserProfile userProf3;
        GamePreferences prefs;
        SystemService us;
        GCServiceInterface gc;
        [SetUp]
        public void before()
        {
            //  gc = GameCenter.GameCenterFactory.getInstance();
            if (GameCenterService.testable)
                gc = new GameCenterService();
            else
                gc = new GameCenterStub();
             us = new SystemService();
            prefs = new GamePreferences(8, 2, 5, 10, 1, 20, 3, true);
            UserProfile ohadUser = new UserProfile("ohad", "213");
            us.register(ohadUser.Username, "213");
            gc.createGame(prefs, ohadUser.Username);
            prefs = new GamePreferences(8, 2, 5, 10, 1, 20, 3, false);
            gc.createGame(prefs, ohadUser.Username);
         
            //us = new SystemService();
            us.register("abc", "123");
            us.login("abc", "123");
 
            us.register("abc2", "123");
            us.login("abc2", "123");
       
            us.register("abc3", "123");
            us.login("abc3", "123");
  

        }
         [TearDown]
        public void after()
        {
            GameCenter.GameCenterFactory.clean();
            TexasHoldemSystem.userSystemFactory.clean();
        }
        
        
        [TestCase]
        public void valid_spectateGame()
        {
            ClientGame g = gc.getAllSpectatingGames()[0];
            int NumberOfPlayersBefore = g.Spectators.Count;
            List<string> spec = null;
            spec = gc.spectateGame(g.getID(), "abc");
            Assert.AreEqual(NumberOfPlayersBefore + 1, spec.Count);
        }

        [TestCase]
        public void valid_spectateGamesList()
        {
            List<ClientGame> games = gc.getAllSpectatingGames();
            foreach (ClientGame g in games)
                Assert.True(g.GamePref.AllowSpec());
        }

        [TestCase]
        public void valid_gameListByName()
        {
          
            ClientGame g = gc.getActiveGames("preferences", prefs, "abc")[0]; ;
            gc.joinGame(g.getID(), "ohad", 30);
            
            List<ClientGame> games = gc.getActiveGames("playerName", "ohad","abc");
            Assert.AreEqual(1,games.Count);
            //Assert.True(games[0].GetUserProfiles().Contains(userProf));s
        }
    }
}
