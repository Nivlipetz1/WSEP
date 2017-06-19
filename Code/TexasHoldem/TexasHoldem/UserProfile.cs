using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Gaming;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.IO;
using GameSystem.Data_Layer;

namespace GameSystem
{
    public class UserProfile
    {
        public ObjectId Id { get; set; }
        private string username;
        private string password;
        private Image avatar;
        private byte[] avatarContent;
        private int credit;
        private League league;
        private int leagueId;
        private Statistics userStat;

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

        [BsonIgnore]
        public Image Avatar
        {
            get {return avatar;}
            set
            {
                avatar = value;
                if (avatar == null)
                    return;
                MemoryStream ms = new MemoryStream();
                avatar.Save(ms, avatar.RawFormat);
                avatarContent = ms.ToArray();
            }
        }

        public byte[] AvatarContent
        {
            get { return avatarContent; }
            set
            {
                avatarContent = value;
                if (avatarContent == null)
                    return;
                MemoryStream ms = new MemoryStream(avatarContent);
                Image returnImage = Image.FromStream(ms);
                avatar = returnImage;
            }
        }

        public int Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        public void UpdateCredit(int val)
        {
            Credit += val;
            DBConnection.Instance.updateUserProfile(this);
        }

        [BsonIgnore]
        public League League
        {
            get
            {
                return league;
            }

            set
            {
                league = value;
                LeagueId = league.minimumRank;
                DBConnection.Instance.updateUserProfile(this);
            }
        }

        public int LeagueId
        {
            get
            {
                return leagueId;
            }

            set
            {
                leagueId = value;
            }
        }

        public Statistics UserStat
        {
            get
            {
                return userStat;
            }

            set
            {
                userStat = value;
            }
        }

        public UserProfile(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            userStat = new Statistics();
        }
        public UserProfile(string username,string password,Image avatar)
        {
            this.Username = username;
            this.Password = password;
            Avatar = avatar;
            userStat = new Statistics();
        }

        public void setUserLeague(League league)
        {
            this.league = league;
            
        }

        public void updateStatistics(PlayingUser user)
        {

            int Grosssum = (from g in user.GainPerRound where g >= 0 select g).Sum();
            int roundsPlayed = userStat.Winnings + userStat.Losses;

            //Leaderboards
            userStat.Winnings += user.GetRoundsWon();
            userStat.Losses += user.GetRoundsLost(); //number of games played
            
            userStat.BiggestWin = Math.Max(userStat.BiggestWin, user.GetMostWon()); //highest cash gain
            userStat.TotalGrossProfit += Grosssum;
            userStat.HighestHand = (userStat.HighestHand < user.GetBestHand()) ? userStat.HighestHand : user.GetBestHand();
            userStat.BiggestWallet = Math.Max(Credit, userStat.BiggestWallet);


            //Userstat            
            UserStat.AvgCashGain = (userStat.AvgCashGain * roundsPlayed + user.GainPerRound.Sum()) / (userStat.Winnings + userStat.Losses);
            userStat.AvgGrossProfit = (userStat.Winnings + userStat.Losses == 0) ? 0 : userStat.TotalGrossProfit / (userStat.Winnings + userStat.Losses);

            DBConnection.Instance.updateUserProfile(this);
            
        }
    }
}
