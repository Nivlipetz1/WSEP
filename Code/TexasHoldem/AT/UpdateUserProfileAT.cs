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
using ServiceLayer.Interfaces;
using AT.Stubs;
using System.IO;

namespace AT
{
    class UpdateUserProfileAT
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
        public void successEditUsername()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.True(us.editUserName("aaaaa", user.Username));
            
        }

        [TestCase]
        public void badEditUsername_Loggedout()
        {
            us.logout("abc");
            //ClientUserProfile user = us.getUser("abc");
            Assert.False(us.editUserName("aaaaa", "abc"));
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
            
            Image avatar = new Bitmap(@"C:\Users\pc\Desktop\Capture.PNG");
            byte[] avatarBytes = ServiceLayer.ImageConverter.imageToByteArray(avatar);

            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.True(us.editAvatar(avatarBytes, user.Username));
            byte[] arr = ServiceLayer.ImageConverter.imageToByteArray(TexasHoldemSystem.userSystemFactory.getInstance().getUser(user.Username).Avatar);
            Image a2 = ServiceLayer.ImageConverter.byteArrayToImage(arr);
            Assert.AreEqual(avatarBytes, arr);
        }
    }
}
