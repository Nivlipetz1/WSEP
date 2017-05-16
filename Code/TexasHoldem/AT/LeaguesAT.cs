using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;
using Gaming;
using NUnit.Framework;
using ServiceLayer;

namespace AT
{
    [TestFixture]
    class LeaguesAT
    {
        LeagueAPI gc;
        int minRank = 10;
        GameSystem.TexasHoldemSystem us;

        [SetUp]
        public void before()
        {
            gc = GameCenter.GameCenterFactory.getInstance();
            us = TexasHoldemSystem.userSystemFactory.getInstance();
            us.register("user", "123");
            us.login("user", "123");
        }

        [TestCase]
        public void success_createLeague()
        {   
            int numberOfLeaguesBefore = gc.getLeagues().Count;
            Assert.True(gc.createNewLeague(minRank));
            Assert.AreEqual(gc.getLeagues().Count, numberOfLeaguesBefore + 1);
        }

        [TestCase]
        public void unsuccessful_createLeague()
        {
            Assert.True(gc.createNewLeague(minRank));
            int numberOfLeaguesBefore = gc.getLeagues().Count;
            Assert.False(gc.createNewLeague(minRank));
            Assert.AreEqual(gc.getLeagues().Count, numberOfLeaguesBefore);
        }

       /* [TestCase]
        public void changeRank_SameLeague()
        {
            gc.createNewLeague(minRank);
            gc.createNewLeague(minRank + 20);
            UserProfile user = us.getUser("user");
            PlayingUser pl = new PlayingUser(user.Username,0,null);
            user.Credit = minRank;
            gc.addUserToLeague(user.Username, gc.getLeagueByRank(minRank));
            user.Credit = minRank + 10;
            gc.updateLeagueToUser(pl);
            Assert.AreEqual(gc.getLeagueByRank(minRank), gc.getLeagueByUser(user));
        }

        [TestCase]
        public void changeRank_changeLeagueUP()
        {
            gc.createNewLeague(minRank);
            gc.createNewLeague(minRank + 20);
            UserProfile user = us.getUser("user");
            PlayingUser pl = new PlayingUser(user.Username, 1, null);
            user.Credit = minRank;
            gc.addUserToLeague(user, gc.getLeagueByRank(minRank));
            user.Credit = minRank + 30;
            gc.updateLeagueToUser(pl);
            Assert.AreNotEqual(gc.getLeagueByRank(minRank), gc.getLeagueByUser(user));
        }

        [TestCase]
        public void changeRank_changeLeagueDown()
        {
            gc.createNewLeague(minRank + 10);
            gc.createNewLeague(minRank);
            UserProfile user = us.getUser("user");
            user.Credit = minRank + 20;
            Assert.True(gc.addUserToLeague(user, gc.getLeagueByRank(minRank + 10)));
            PlayingUser pl = new PlayingUser(user.Username, minRank + 20, null);

            Game g = new Game(new GamePreferences());

            UserProfile Omer = new UserProfile("Omer", "456");
            gc.addUserToLeague(Omer, gc.getLeagueByRank(minRank + 10));

            us.register(Omer.Username, Omer.Password);
            PlayingUser OPlayer = new PlayingUser(Omer.Username, 1000, g);

            g.addPlayer(OPlayer);
            g.addPlayer(pl);


            OPlayer.SetFakeUserInput(new Queue<string>(new[] { "5" , "10", "5" }));
            pl.SetFakeUserInput(new Queue<string>(new[] { "0", "10" , "-1"}));

            g.StartGame();
            g.removePlayer(pl);
            user.Credit = 5; //TODO The playing user in not losing in the game.To make him lose money.
            List<Move> moves = g.GetGameReplay();
            gc.updateLeagueToUser(pl);
            League l = gc.getLeagueByUser(user);
            Assert.AreEqual(gc.getLeagueByRank(minRank), gc.getLeagueByUser(user));
        }

        [TestCase]
        public void addUserToMyLeague()
        {
            //im20 to leaguefrom10
            gc.createNewLeague(minRank + 10);
            UserProfile user = us.getUser("user");
            user.Credit = minRank + 20;
            Assert.True(gc.addUserToLeague(user, gc.getLeagueByRank(minRank+10)));
            Assert.AreEqual(gc.getLeagueByRank(minRank + 10), gc.getLeagueByUser(user));
        }

        [TestCase]
        public void addUserToWrongLeague()
        {
            gc.createNewLeague(minRank + 10);
            UserProfile user = us.getUser("user");
            user.Credit = minRank;
            Assert.False(gc.addUserToLeague(user, gc.getLeagueByRank(minRank + 10)));
        }

        [TestCase]
        public void Add_to_2_Leagues()
        {
            gc.createNewLeague(minRank + 10);
            gc.createNewLeague(minRank);
            UserProfile user = us.getUser("user");
            user.Credit = minRank + 20;
            Assert.True(gc.addUserToLeague(user, gc.getLeagueByRank(minRank + 10)));
            Assert.True(gc.addUserToLeague(user, gc.getLeagueByRank(minRank)));
            Assert.False(gc.getLeagueByRank(minRank + 10).isUser(user));
        }
        */
        [TestCase]
        public void newLeague_ToRankLeague()
        {

        }

        [TestCase]
        public void idleUser_League()
        {

        }
    }
}
