﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Gaming;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameSystem
{
    public class UserProfile
    {
        public ObjectId Id { get; set; }
        private string username;
        private string password;
        private Image avatar;
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
            this.Avatar = avatar;
            userStat = new Statistics();
        }

        public void setUserLeague(League league)
        {
            this.league = league;
            
        }

        public void updateStatistics(PlayingUser user)
        {
            userStat.Winnings += user.GetRoundsWon();
            userStat.Losses += user.GetRoundsLost();
            userStat.BiggestWin = (userStat.BiggestWin > user.GetMostWon()) ? userStat.BiggestWin : user.GetMostWon();
            userStat.HighestHand = (userStat.HighestHand < user.GetBestHand()) ? userStat.HighestHand : user.GetBestHand();
            userStat.BiggestWallet = (Credit > userStat.BiggestWallet) ? Credit : userStat.BiggestWallet;
        }
    }
}
