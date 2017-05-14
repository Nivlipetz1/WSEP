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

        public bool editAvatar(byte []  avatar, ClientUserProfile u)
        {
            SystemService userService = new SystemService();
            return userService.editAvatar(avatar, u);
        }

        public bool logout(ClientUserProfile u)
        {
            SystemService userService = new SystemService();
            if (userService.logout(u))
            {
                AuthManager.Instance.Logout(u.Username, Context.ConnectionId);
                Groups.Remove(Context.ConnectionId, "loginUsers");
                return true;
            }

            return false;
        }

        public bool editPassword(string password, ClientUserProfile u)
        {
            SystemService userService = new SystemService();
            return userService.editPassword(password, u);
        }

        public bool editUserName(string userName, ClientUserProfile u)
        {
            SystemService userService = new SystemService();
            return userService.editUserName(userName, u);
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