using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSystem;

namespace ServiceLayer.Models
{
    public class ClientUserProfile
    {
        private string username;
        private byte [] avatar;
        private int credit;
        private int leagueId;
        private Statistics userStat;

        public ClientUserProfile(UserProfile userProfile)
        {
            username = userProfile.Username;
            avatar = null;
            if(userProfile.Avatar != null)
                avatar = ImageConverter.imageToByteArray(userProfile.Avatar);
            credit = userProfile.Credit;
            leagueId = userProfile.League.MinimumRank;
            userStat = userProfile.UserStat;
        }

        public string Username
        {
            get{ return username;}
        }

        public byte [] Avatar
        {
            get {return avatar;}
        }

        public int Credit
        {
            get { return credit; }
        }

        public int League
        {
            get { return leagueId; }

        }

        public Statistics UserStat
        {
            get { return userStat; }
        }      
    }
}
