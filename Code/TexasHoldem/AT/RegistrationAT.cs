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

namespace AT
{
    [TestFixture]

    public class RegistrationAT
    {
       
        private SystemAPI us;

        [SetUp]
        public void before()
        {
            us = new UserSystem_Service();
        }

        [TestCase]
        public void successRegister()
        {
            Assert.True(us.register("abc", "123"));
            Assert.True(us.register("bcd", "1!@#sda"));
            Assert.True(us.register("cde", "dsadasdas"));
            Assert.True(us.register("def", "AS1 22   4"));
            Assert.True(us.register("efg", "0"));
        }

        [TestCase]
        public void BadUsernameRegister()
        {
            Assert.False(us.register("1", "ab1"));
            Assert.False(us.register("        ", "ab1"));
            Assert.False(us.register("", "ab1"));
            Assert.False(us.register("$$", "ab1"));
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
            Assert.False(us.register("abc", "1"));
        }
    }
}
