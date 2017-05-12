using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommunicatoinLayer.Hubs;

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
        }

        private IHubConnectionContext<dynamic> Clients { set; get; }

        public void Login(string name, string password, string connectionId)
        {
            // login 

            //on login success
            _usersByName[name] = connectionId;
            _usersByConnectionId[connectionId] = name;
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

        public static AuthManager Instance
        {
            get { return LazyInstance.Value; }
        }

    }
}