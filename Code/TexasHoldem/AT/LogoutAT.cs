using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceLayer;
using GameSystem;
using ServiceLayer.Models;
using ServiceLayer.Interfaces;
using AT.Stubs;

namespace AT
{
    class LogoutAT
    {
        private AuthSystemServiceInterface us;

        [SetUp]
        public void before()
        {
            if (SystemService.testable)
                us = new SystemService();
            else
                us = new SystemStub();
            us.register("abc", "123");
        }

        [TestCase]
        public void Logout_Loggedin()
        {
            Assert.True(us.login("abc", "123"));
            ClientUserProfile prof = us.getUser("abc");
            Assert.True(us.logout(prof));
            Assert.False(us.logout(prof));
        }

        [TestCase]
        public void Logout_NotLoggedin()
        {
            ClientUserProfile prof = us.getUser("abc");
            Assert.False(us.logout(prof));
        }
    }
}