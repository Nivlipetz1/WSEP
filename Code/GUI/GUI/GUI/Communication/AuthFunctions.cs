using GUI.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Communication
{
    public class AuthFunctions
    {
        private static Lazy<AuthFunctions> LazyInstance = new Lazy<AuthFunctions>(() => new AuthFunctions(), true);
        private IHubProxy authHubProxy;

        private AuthFunctions()
        {

        }

        public static AuthFunctions Instance
        {
            get { return LazyInstance.Value; }
        }

        public IHubProxy AuthHubProxy
        {
            get {return authHubProxy;}

            set {authHubProxy = value;}
        }

        public void initOnFunctions()
        {
            authHubProxy.On<string>("notify" , (message) =>
            {
            });
        }

        public async Task<bool> register(string userName, string password)
        {
            return await authHubProxy.Invoke<bool>("register", userName, password);
        }

        public async Task<bool> login(string userName, string password)
        {
            return await authHubProxy.Invoke<bool>("login", userName, password);
        }

        public async Task<ClientUserProfile> getClientUser()
        {
            return await authHubProxy.Invoke<ClientUserProfile>("getClientUser");
        }

        public async Task<bool> logout()
        {
            return await authHubProxy.Invoke<bool>("logout");
        }

        public async Task<bool> editAvatar(byte [] avatar)
        {
            return await authHubProxy.Invoke<bool>("editAvatar", avatar);
        }

        public async Task<bool> editPassword(string password)
        {
            return await authHubProxy.Invoke<bool>("editPassword", password);
        }

        public async Task<bool> editUserName(string newUserName)
        {
            return await authHubProxy.Invoke<bool>("editUserName", newUserName);
        }



    }
}
