using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace AT.Stubs
{
    class SystemStub : AuthSystemServiceInterface
    {
        public bool editAvatar(byte[] avatar, string u)
        {
            return true;
        }

        public bool editPassword(string password, string u)
        {
            return true;
        }

        public bool editUserName(string userName, string u)
        {
            return true;
        }

        public ClientUserProfile getUser(string username)
        {
            return new ClientUserProfile(new GameSystem.UserProfile(username, "password"));
        }

        public bool isConnected(string username)
        {
            return true;
        }

        public bool login(string userName, string password)
        {
            return true;
        }

        public bool logout(string u)
        {
            return true;
        }

        public bool register(string userName, string password)
        {
            return true;
        }
    }
}
