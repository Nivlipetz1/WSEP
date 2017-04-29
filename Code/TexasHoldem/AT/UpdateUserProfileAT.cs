using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;
using NUnit.Framework;
using GameUtilities;
using System.Drawing;
using ServiceLayer;


namespace AT
{
    class UpdateUserProfileAT
    {
       
        private UserSystem_Service us;
        [SetUp]
        public void before()
        {
            us = new UserSystem_Service();
            us.register("abc", "123");
        }

        [TestCase]
        public void successEditUsername()
        {
            us.login("abc", "123");
            UserProfile user = us.getUser("abc", "123");
            Assert.True(us.editUserName("aaaaa", user));
        }

        [TestCase]
        public void badEditUsername_Loggedout()
        {
            UserProfile user = us.getUser("abc", "123");
            Assert.False(us.editUserName("aaaaa", user));
        }

        [TestCase]
        public void badEditUsername_Loggedin()
        {
            us.login("abc", "123"); 
            UserProfile user = us.getUser("abc", "123");
            Assert.False(us.editUserName("", user));
            Assert.False(us.editUserName("   ", user));
        }

        [TestCase]
        public void successEditPassword()
        {
            us.login("abc", "123");
            UserProfile user = us.getUser("abc", "123");
            us.editPassword("124", user);
            us.logout(user);
            Assert.True(us.login("abc", "124"));
        }

        [TestCase]
        public void badEditPassword()
        {
            us.login("abc", "123");
            UserProfile user = us.getUser("abc", "123");
            Assert.False(us.editPassword("", user));
            Assert.False(us.editPassword("    ", user));
        }

        [TestCase]
        public void editAvatar()
        {
            
            Image avatar = new Bitmap("C:\\Users\\pc\\Desktop\\capture.png");
            us.login("abc", "123");
            UserProfile user = us.getUser("abc", "123");
            Assert.True(us.editAvatar(avatar, user));
            Assert.AreEqual(user.Avatar, avatar);
        }
    }
}
