using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    interface Users
    {
        bool register(string userName, string password);
        bool login(string userName, string password);
        bool logout();
        bool editUserName(string userName);
        bool editPassword(string password);
    }
}
