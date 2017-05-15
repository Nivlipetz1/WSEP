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
        public bool editAvatar(byte[] avatar, ClientUserProfile u)
        {
            throw new NotImplementedException();
        }

        public bool editPassword(string password, ClientUserProfile u)
        {
            throw new NotImplementedException();
        }

        public bool editUserName(string userName, ClientUserProfile u)
        {
            throw new NotImplementedException();
        }

        public ClientUserProfile getUser(string username)
        {
            throw new NotImplementedException();
        }

        public bool isConnected(string username)
        {
            throw new NotImplementedException();
        }

        public bool login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool logout(ClientUserProfile u)
        {
            throw new NotImplementedException();
        }

        public bool register(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
