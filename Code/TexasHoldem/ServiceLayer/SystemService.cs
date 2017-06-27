using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameSystem;
using System.Text.RegularExpressions;
using ServiceLayer.Models;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class SystemService : AuthSystemServiceInterface
    {
        public static bool testable=true;
        private SystemAPI system;
        public SystemService()
        {
            system = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();
        }

        public bool editAvatar(byte[] avatar, string userName)
        {
            return system.editAvatar(ImageConverter.byteArrayToImage(avatar), system.getUser(userName));
        }

        public bool editPassword(string password, string userName)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            return system.editPassword(password, system.getUser(userName));
        }

        public bool editUserName(string newUserName, string userName)
        {
            if(string.IsNullOrWhiteSpace(newUserName))
                return false;
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (!r.IsMatch(newUserName))
            {
                return false;
            }
            r = new Regex("^[0-9]*$");
            if (r.IsMatch(""+ newUserName[0]))
                return false;
            return system.editUserName(newUserName, system.getUser(userName));
        }

        public bool checkUserDetails(string userName, string password)
        {
            return system.checkUserDetails(userName, password);
        }

        public List<Tuple<string,int>> getTop20(string criteria)
        {
            return system.getTop20(criteria);
        }

        public ClientUserProfile getUser(string username)
        {
            if(system.isConnected(username))
                return new ClientUserProfile(system.getUser(username));

            return null;
        }

        public bool isConnected(string username)
        {
            return system.isConnected(username);
        }

        public bool login(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return false;
            return system.login(userName, password);
        }

        public bool logout(string userName)
        {
            return system.logout(system.getUser(userName));
        }

        public bool register(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;
            Regex r = new Regex("^[a-zA-Z0-9@]*$.");
            if (!r.IsMatch(userName))
            {
                return false;
            }
            r = new Regex("^[0-9]*$");
            if (r.IsMatch("" + userName[0]))
                return false;
            if (string.IsNullOrWhiteSpace(password))
                return false;
            return system.register(userName, password);
        }

        public double getCashGain(string name)
        {
            return system.getCashGain(name);
        }

        public double getGrossProfit(string name)
        {
            return system.getGrossProfit(name);
        }

        public bool isUserExist(string name)
        {
            return system.isUserExist(name);
        }
    }
}
