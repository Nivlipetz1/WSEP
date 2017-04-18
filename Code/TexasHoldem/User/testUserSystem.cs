using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    [TestFixture]

    class testUserSystem
    {
        [TestCase]
        public void registerTest()
        {
            UserSystem us = UserSystem.userSystemFactory.getInstance();
            Assert.True(us.register("abc", "123"));
        }
        [TestCase]
        public void registerWithUsedNameTest()
        {
            UserSystem us = UserSystem.userSystemFactory.getInstance();
            Assert.True(us.register("abc", "123"));
            Assert.False(us.register("abc", "321"));
        }

        [TestCase]
        public void SuccesfullLoginTest()
        {
            UserSystem us = UserSystem.userSystemFactory.getInstance();
            us.register("abc", "123");
            Assert.True(us.login("abc", "123"));
        }
        [TestCase]
        public void failureLoginTest()
        {
            UserSystem us = UserSystem.userSystemFactory.getInstance();
            us.register("abc", "123");
            us.register("aaa", "123");
            us.login("aaa", "123");
            Assert.False(us.login("aaa", "123"));
        }
        [TestCase]
        public void editUsernameAndPasswordTest()
        {
            
            UserSystem us = UserSystem.userSystemFactory.getInstance();
            us.register("abc", "123");
            us.login("abc", "123");
            user user = us.getUser("abc","123");
            Assert.True(us.editPassword("123456",user));
            Assert.True(us.editUserName("abcdef",user));     
            Assert.AreEqual(user.Password, "123456");
            Assert.AreEqual(user.Username,"abcdef");
        }

        [TestCase]
        public void editProfileToLoggedOutUser()
        {
            UserSystem us = UserSystem.userSystemFactory.getInstance();
            us.register("abc", "123");
            user user = us.getUser("abc", "123");
            Assert.False(us.editUserName("aaaaa",user));  
        }

    }
}
