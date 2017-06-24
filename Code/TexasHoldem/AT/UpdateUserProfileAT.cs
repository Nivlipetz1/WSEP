﻿using System;
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
        [TearDown]
        public void after()
        {
            GameCenter.GameCenterFactory.clean();
            TexasHoldemSystem.userSystemFactory.clean();
        }

        [TestCase]
        public void successEditUsername()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.True(us.editUserName("aaaaa", user.Username));
            us.editUserName("abc", "aaaaa");

        }

        [TestCase]
        public void badEditUsername_Loggedout()
        {
            us.logout("abc");
            //ClientUserProfile user = us.getUser("abc");
            Assert.False(us.editUserName("aaaaa", "abc"));
        }

        [TestCase]
        public void badEditUsername_EmptyUserName_Loggedin()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.False(us.editUserName("", user.Username));
            us.logout("abc");
        }

        [TestCase]
        public void badEditUsername_OnlySpacesUsername_Loggedin()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.False(us.editUserName("   ", user.Username));
            us.logout("abc");
        }

        [TestCase]
        public void successEditPassword()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            us.editPassword("124", user.Username);
            us.logout(user.Username);
            Assert.True(us.login("abc", "124"));
            us.editPassword("123", user.Username);
        }

        [TestCase]
        public void badEditPassword_emptyPassword()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.False(us.editPassword("", user.Username));
            us.logout("abc");
        }
        [TestCase]
        public void badEditPassword_onlySpacesPassword()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            Assert.False(us.editPassword("    ", user.Username));
            us.logout("abc");
        }
        [TestCase]
        public void editAvatar()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            try
            {
                Image avatar = new Bitmap(Environment.CurrentDirectory+ "\\AT\\avatar.jpg");
                us.editAvatar(ServiceLayer.ImageConverter.imageToByteArray(avatar), "abc");
                Assert.AreEqual(ServiceLayer.ImageConverter.imageToByteArray(avatar), user.Avatar);
            }
            catch(Exception e)
            {
                Exception s = e;
                Assert.True(false);
            }
        }

        [TestCase]
        public void badEditAvatar_PictureTooBig()
        {
            us.login("abc", "123");
            ClientUserProfile user = us.getUser("abc");
            try
            {
                Image avatar = new Bitmap(Environment.CurrentDirectory + "\\AT\\avatarBIG.jpg");
                us.editAvatar(ServiceLayer.ImageConverter.imageToByteArray(avatar), "abc");
                Assert.AreNotEqual(ServiceLayer.ImageConverter.imageToByteArray(avatar), user.Avatar);
            }
            catch (Exception e)
            {
                Exception s = e;
                Assert.True(false);
            }
        }
    }
}
