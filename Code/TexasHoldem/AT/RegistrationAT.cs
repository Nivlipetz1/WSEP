using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NUnit.Framework;
using GameSystem;
using ServiceLayer;
using Gaming;
using ServiceLayer.Interfaces;
using AT.Stubs;

namespace AT
{
    [TestFixture]

    public class RegistrationAT
    {

        private AuthSystemServiceInterface us;

        [SetUp]
        public void before()
        {
            if (SystemService.testable)
                us = new SystemService();
            else
                us = new SystemStub();
        }
        [TearDown]
        public void after()
        {
            GameCenter.GameCenterFactory.clean();
            TexasHoldemSystem.userSystemFactory.clean();
        }

        [TestCase]
        public void successRegister()
        {
            Assert.True(us.register("myTest"+new Random().Next(100), "123"));
        }

        [TestCase]
        public void BadUsername_Only_Number_Register()
        {
            Assert.False(us.register("123", "ab1"));
        }
        [TestCase]
        public void BadUsername_Only_signes_Register()
        {
            Assert.False(us.register("$$", "ab1"));
        }
        [TestCase]
        public void BadUsername_Empty_username_Register()
        {
            Assert.False(us.register("", "ab1"));
        }
        [TestCase]
        public void BadUsername_Only_spaces_Register()
        {
            Assert.False(us.register("        ", "ab1"));
        }



        [TestCase]
        public void emptyPasswordRegister()
        {
            Assert.False(us.register("abc", ""));
        }

        [TestCase]
        public void spacesPasswordRegister()
        {
            Assert.False(us.register("Chicken", "         "));
        }

        [TestCase]
        public void usernameExistRegister()
        {
            us.register("abc", "123");
            Assert.False(us.register("abc", "123"));
        }
    }
}
