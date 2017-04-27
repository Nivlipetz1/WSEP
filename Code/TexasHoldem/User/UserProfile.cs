using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameUtilities
{
    public class UserProfile
    {
        private string username;
        private string password;
        private Image avatar;
        private int credit;
        private List<Notification> notifications = new List<Notification>();

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

        public int Credit
        {
            get { return credit; }
            set { credit = value; }
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

        public void addNotify(String message)
        {
            Notification notification = new Notification(message);
            notifications.Add(notification);
        }
    }
}
