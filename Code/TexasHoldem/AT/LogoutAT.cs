using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GameUtilities;

namespace AT
{
    class LogoutAT
    {
        private TexasHoldemSystem.TexasHoldemSystem us;

        [SetUp]
        public void before()
        {
            us = TexasHoldemSystem.TexasHoldemSystem.userSystemFactory.getInstance();
            us.register("abc", "123");
        }

        [TestCase]
        public void Logout_Loggedin()
        {
            Assert.True(us.login("abc", "123"));
            UserProfile prof = us.getUser("abc");
            Assert.True(us.logout(prof));
            Assert.False(us.logout(prof));
        }

        [TestCase]
        public void Logout_NotLoggedin()
        {
            UserProfile prof = us.getUser("abc");
            Assert.False(us.logout(prof));
        }
    }
}