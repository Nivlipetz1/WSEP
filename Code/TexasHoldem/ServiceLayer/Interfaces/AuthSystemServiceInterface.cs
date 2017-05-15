using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    interface AuthSystemServiceInterface
    {
         bool editAvatar(byte[] avatar, ClientUserProfile u);

         bool editPassword(string password, ClientUserProfile u);
         
         bool editUserName(string userName, ClientUserProfile u);

         ClientUserProfile getUser(string username);

         bool isConnected(string username);

         bool login(string userName, string password);

         bool logout(ClientUserProfile u);

        bool register(string userName, string password);
        
    }
}
