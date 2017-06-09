using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface AuthSystemServiceInterface
    {
         bool editAvatar(byte[] avatar, String u);

         bool editPassword(string password, String u);
         
         bool editUserName(string userName, String u);

         ClientUserProfile getUser(string username);

         bool isConnected(string username);

         bool login(string userName, string password);

         bool logout(string u);

         bool register(string userName, string password);
        List<ClientUserProfile> getTop20(string criteria);
        
    }
}
