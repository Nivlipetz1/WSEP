using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NUnit.Framework;
using GameSystem;

namespace SystemTests
{
    [TestFixture]

    public class testUserSystem
    {
        GameSystem.TexasHoldemSystem us;
        [SetUp]
        public void before()
        {
            us = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();
        }

        [TearDown]
        public void after()
        {
            us.clearUsers();
        }
        [TestCase]
        public void registerTest()
        {
            Assert.True(us.register("abc", "123"));
        }
        [TestCase]
        public void registerWithUsedNameTest()
        {
            Assert.True(us.register("abc", "123"));
            Assert.False(us.register("abc", "321"));
        }

        [TestCase]
        public void SuccesfullLoginTest()
        {
            us.register("abc", "123");
            Assert.True(us.login("abc", "123"));
        }
        [TestCase]
        public void failureLoginTest()
        {
            us.register("abc", "123");
            us.register("aaa", "123");
            us.login("aaa", "123");
            Assert.False(us.login("aaa", "123"));
        }
        [TestCase]
        public void editUsernameAndPasswordTest()
        {
            us.register("abc", "123");
            us.login("abc", "123");
            UserProfile user = us.getUser("abc","123");

            Assert.True(us.editPassword("123456",user));
            Assert.True(us.editUserName("abcdef",user));     
            Assert.AreEqual(user.Password, "123456");
            Assert.AreEqual(user.Username,"abcdef");
        }

        [TestCase]
        public void editProfileToLoggedOutUser()
        {
            us.register("abc", "123");
            UserProfile user = us.getUser("abc", "123");
            Assert.False(us.editUserName("aaaaa",user));
        }
    }
}
