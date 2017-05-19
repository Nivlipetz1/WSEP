using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommunicatoinLayer.Hubs;
using Gaming;

namespace CommunicatoinLayer.Managers
{
    public class AuthManager
    {
        private static Lazy<AuthManager> LazyInstance = new Lazy<AuthManager>(() => new AuthManager(GlobalHost.ConnectionManager), true);
        private readonly ConcurrentDictionary<string, string> _usersByName;
        private readonly ConcurrentDictionary<string, string> _usersByConnectionId;

        private AuthManager(IConnectionManager connectionManager)
        {
            _usersByName = new ConcurrentDictionary<string, string>();
            _usersByConnectionId = new ConcurrentDictionary<string, string>();

            Clients = connectionManager.GetHubContext<AuthHub>().Clients;

            NotificationService.notifyAllUsesrEvt += notifyAllUsers;
            NotificationService.notifyUserEvt += notifyUser;
        }

        private IHubConnectionContext<dynamic> Clients { set; get; }

        public void Login(string name, string password, string connectionId)
        {
            // login 

            //on login success
            if (_usersByName.ContainsKey(name))
                Logout(name, GetConnectionIdByName(name));

            _usersByName[name] = connectionId;
            _usersByConnectionId[connectionId] = name;
        }

        private void notifyUser(string userName, string message)
        {
            if(containsUserName(userName))
                Clients.Client(GetConnectionIdByName(userName)).notify(message);
        }

        private void notifyAllUsers(string message)
        {
            Clients.All.notify(message);
        }

        public void Logout(string name, string connectionId)
        {
            // login 

            //on login success
            string val1, val2;
            _usersByName.TryRemove(name, out val1);
            _usersByConnectionId.TryRemove(connectionId, out val2);
        }


        public string GetConnectionIdByName(string name)
        {
            return _usersByName[name];
        }

        public string GetNameByConnectionId(string id)
        {
            return _usersByConnectionId[id];
        }

        public bool containsConnection(string id)
        {
            return _usersByConnectionId.ContainsKey(id);
        }

        public bool containsUserName(string userName)
        {
            return _usersByName.ContainsKey(userName);
        }

        public static AuthManager Instance
        {
            get { return LazyInstance.Value; }
        }

    }
}