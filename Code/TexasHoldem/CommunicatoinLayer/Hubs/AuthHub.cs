using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using GameSystem;
using ServiceLayer;
using CommunicatoinLayer.Managers;
using System.Threading.Tasks;
using System.Drawing;
using ServiceLayer.Models;

namespace CommunicatoinLayer.Hubs
{
    public class AuthHub : Hub
    {

        public async Task<bool> login(string userName, string password)
        {
            SystemService userService = new SystemService();
            if (userService.login(userName, password))
            {
                AuthManager.Instance.Login(userName, password, Context.ConnectionId);
                await Groups.Add(Context.ConnectionId, "loginUsers");
                return true;
            }

            return false;
        }

        public bool register(string userName, string password)
        {
            SystemService userService = new SystemService();
            return userService.register(userName, password);
        }

        public ClientUserProfile getClientUser(string userName)
        {
            SystemService userService = new SystemService();
            return userService.getUser(userName);
        }

        public bool editAvatar(byte []  avatar, string userName)
        {
            SystemService userService = new SystemService();
            return userService.editAvatar(avatar, userName);
        }

        public bool logout(string userName)
        {
            SystemService userService = new SystemService();
            if (userService.logout(userName))
            {
                AuthManager.Instance.Logout(userName, Context.ConnectionId);
                Groups.Remove(Context.ConnectionId, "loginUsers");
                return true;
            }

            return false;
        }

        public bool editPassword(string password, string userName)
        {
            SystemService userService = new SystemService();
            return userService.editPassword(password, userName);
        }

        public bool editUserName(string newUserName, string userName)
        {
            SystemService userService = new SystemService();
            return userService.editUserName(newUserName, userName);
        }


        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            AuthManager.Instance.Logout(AuthManager.Instance.GetNameByConnectionId(Context.ConnectionId), Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
    }
}