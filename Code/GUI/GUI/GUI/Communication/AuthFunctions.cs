﻿using GUI.Models;
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

        public bool register(string userName, string password)
        {
            Task<bool> res = authHubProxy.Invoke<bool>("register", userName, password);
            res.Wait();
            return res.Result;
        }

        public bool login(string userName, string password)
        {
            Task<bool> res = authHubProxy.Invoke<bool>("login", userName, password);
            res.Wait();
            return res.Result;
        }

        public ClientUserProfile getClientUser(string userName)
        {
            Task<ClientUserProfile> res = authHubProxy.Invoke<ClientUserProfile>("getClientUser", userName);
            res.Wait();
            return res.Result;
        }

        public bool logout(string user)
        {
            Task<bool> res = authHubProxy.Invoke<bool>("logout" , user);
            res.Wait();
            return res.Result;
        }

        public bool editAvatar(byte [] avatar , string user)
        {
            Task<bool> res = authHubProxy.Invoke<bool>("editAvatar", avatar, user);
            res.Wait();
            return res.Result;
        }

        public bool editPassword(string password , string user)
        {
            Task<bool> res = authHubProxy.Invoke<bool>("editPassword", password , user);
            res.Wait();
            return res.Result;
        }

        public bool editUserName(string newUserName, string user)
        {
            Task<bool> res = authHubProxy.Invoke<bool>("editUserName", newUserName, user);
            res.Wait();
            return res.Result;
        }



    }
}
