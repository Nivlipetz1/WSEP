using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;
using NUnit.Framework;
using System.Drawing;
using ServiceLayer;
using ServiceLayer.Models;
using System.IO;

namespace AT
{
    class UpdateUserProfileAT
    {
       
        private SystemService us;
        [SetUp]
        public void before()
        {
            us = new SystemService();
            us.register("abc", "123");
        }

        [TestCase]
        public void successEditUsername()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.True(us.editUserName("aaaaa", user.Username));
        }

        [TestCase]
        public void badEditUsername_Loggedout()
        {
            ClientUserProfile user = us.getUser("abc");
            Assert.False(us.editUserName("aaaaa", user.Username));
        }

        [TestCase]
        public void badEditUsername_Loggedin()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.False(us.editUserName("", user.Username));
            Assert.False(us.editUserName("   ", user.Username));
        }

        [TestCase]
        public void successEditPassword()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            us.editPassword("124", user.Username);
            us.logout(user.Username);
            Assert.True(us.login("abc", "124"));
        }

        [TestCase]
        public void badEditPassword()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.False(us.editPassword("", user.Username));
            Assert.False(us.editPassword("    ", user.Username));
        }

        [TestCase]
        public void editAvatar()
        {
            
            Image avatar = new Bitmap("C:\\Users\\pc\\Desktop\\capture.png");
            MemoryStream ms = new MemoryStream();
            avatar.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.True(us.editAvatar(ms.ToArray(), user.Username));
            Assert.AreEqual(user.Avatar, avatar);
        }
    }
}
