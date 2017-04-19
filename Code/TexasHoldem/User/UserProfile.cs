using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace User
{
    public class UserProfile
    {
        private string username;
        private string password;
        private Image avatar;

        public string Username
        {
            get{ return username;}
            set{username = value;}
        }

        public string Password
        {
            get{return password;}
            set {password = value; }
        }

        public Image Avatar
        {
            get {return avatar;}
            set {avatar = value;}
        }

        public UserProfile(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public UserProfile(string username,string password,Image avatar)
        {
            this.Username = username;
            this.Password = password;
            this.Avatar = avatar;
        }
    }
}
