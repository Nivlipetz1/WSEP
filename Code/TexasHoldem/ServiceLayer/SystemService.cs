using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameSystem;
using System.Text.RegularExpressions;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class SystemService
    {
        private SystemAPI system;
        public SystemService()
        {
            system = GameSystem.TexasHoldemSystem.userSystemFactory.getInstance();
        }

        public bool editAvatar(byte[] avatar, ClientUserProfile u)
        {
            return system.editAvatar(ImageConverter.byteArrayToImage(avatar), system.getUser(u.Username));
        }

        public bool editPassword(string password, ClientUserProfile u)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            return system.editPassword(password, system.getUser(u.Username));
        }

        public bool editUserName(string userName, ClientUserProfile u)
        {
            if(string.IsNullOrWhiteSpace(userName))
                return false;
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (!r.IsMatch(userName))
            {
                return false;
            }
            r = new Regex("^[0-9]*$");
            if (r.IsMatch(""+userName[0]))
                return false;
            return system.editUserName(userName, system.getUser(u.Username));
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

        public bool logout(ClientUserProfile u)
        {
            return system.logout(system.getUser(u.Username));
        }

        public bool register(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;
            Regex r = new Regex("^[a-zA-Z0-9]*$");
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
    }
}
