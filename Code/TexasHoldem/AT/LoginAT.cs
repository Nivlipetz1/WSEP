using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceLayer;
using ServiceLayer.Interfaces;
using AT.Stubs;
using GameSystem;

namespace AT
{
    class LoginAT
    {
      //  private SystemService us;
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
        [TearDown]
        public void after()
        {
            GameCenter.GameCenterFactory.clean();
            TexasHoldemSystem.userSystemFactory.clean();
        }

        [TestCase]
        public void Success_LoginTest()
        {
            Assert.True(us.login("abc", "123"));
            us.logout("abc");
        }

        [TestCase]
        public void usernameNotExist_LoginTest()
        {
            Assert.False(us.login("aaa", "123"));
            Assert.False(us.login(" abc", "123"));
            Assert.False(us.login("abc ", "123"));
            Assert.False(us.login("            ", "123"));
        }

        [TestCase]
        public void passwordIncorrect_LoginTest()
        {
            Assert.False(us.login("abc", ""));
            Assert.False(us.login("abc", "          "));
            Assert.False(us.login("abc", "1"));
            Assert.False(us.login("abc", " 123"));
            Assert.False(us.login("abc", "123 "));
            Assert.False(us.login("abc", "1 23"));
        }

        [TestCase]
        public void AllreadyLoggedin_LoginTest()
        {
            us.login("abc", "123");
            Assert.False(us.login("abc", "123"));
            us.logout("abc");
        }
    }
}