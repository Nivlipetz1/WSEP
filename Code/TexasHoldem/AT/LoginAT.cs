using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceLayer;
using GameSystem;

namespace AT
{
    class LoginAT
    {
        private SystemAPI us;

        [SetUp]
        public void before()
        {
            us = new SystemService();
            us.register("abc", "123");
        }

        [TestCase]
        public void Success_LoginTest()
        {
            Assert.True(us.login("abc", "123"));
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
        }
    }
}