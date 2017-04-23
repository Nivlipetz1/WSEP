using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using User;

namespace TexasHoldemSystem
{
    [TestFixture]

    public class testUserSystem
    {
        [TestCase]
        public void registerTest()
        {
            TexasHoldemSystem us = TexasHoldemSystem.userSystemFactory.getInstance();
            Assert.True(us.register("abc", "123"));
        }
        [TestCase]
        public void registerWithUsedNameTest()
        {
            TexasHoldemSystem us = TexasHoldemSystem.userSystemFactory.getInstance();
            Assert.True(us.register("abc", "123"));
            Assert.False(us.register("abc", "321"));
        }

        [TestCase]
        public void SuccesfullLoginTest()
        {
            TexasHoldemSystem us = TexasHoldemSystem.userSystemFactory.getInstance();
            us.register("abc", "123");
            Assert.True(us.login("abc", "123"));
        }
        [TestCase]
        public void failureLoginTest()
        {
            TexasHoldemSystem us = TexasHoldemSystem.userSystemFactory.getInstance();
            us.register("abc", "123");
            us.register("aaa", "123");
            us.login("aaa", "123");
            Assert.False(us.login("aaa", "123"));
        }
        [TestCase]
        public void editUsernameAndPasswordTest()
        {
            
            TexasHoldemSystem us = TexasHoldemSystem.userSystemFactory.getInstance();
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
            TexasHoldemSystem us = TexasHoldemSystem.userSystemFactory.getInstance();
            us.register("abc", "123");
            UserProfile user = us.getUser("abc", "123");
            Assert.False(us.editUserName("aaaaa",user));
        }
        [TestCase]
        public void editAvatarTest()
        {
            Image avatar = new Bitmap("C:/Users/pc/Desktop/avatar.png");
            TexasHoldemSystem us = TexasHoldemSystem.userSystemFactory.getInstance();
            us.register("abc", "123");
            us.login("abc", "123");
            UserProfile user = us.getUser("abc", "123");
            Assert.True(us.editAvatar(avatar, user));
            Assert.AreEqual(user.Avatar, avatar);
        }

    }
}
