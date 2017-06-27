using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class ClientGame
    {
        public int id { set; get; }
        public Deck gameDeck { set; get; }
        public List<ClientUserProfile> players { set; get; }
        public List<ClientUserProfile> spectators { set; get; }
        public List<ClientUserProfile> waitingList { set; get; }
        public List<ClientUserProfile> waitingListSpec { set; get; }
        public int[] pot { set; get; }
        public GamePreferences gamePref { set; get; }
        public Dictionary<string, int> playerBets { set; get; }
        public Dictionary<string, string> messageList { set; get; }


        public void RemovePlayer(string username)
        {
            foreach(ClientUserProfile prof in players)
            {
                if (prof.username.Equals(username))
                {
                    players.Remove(prof);
                    messageList.Remove(username);
                    return;
                }
            }
        }

        public void AddPlayerToWaitingList(ClientUserProfile profile)
        {
            waitingList.Add(profile);
            messageList.Add(profile.username, "");
        }

        public void AddSpecToWaitingList(ClientUserProfile profile)
        {
            waitingListSpec.Add(profile);
            messageList.Add(profile.username, "");
        }

        public void AddMessage(string sender, string message)
        {
            messageList[sender] += sender + ": " + message + "\n";
        }

        public void AddMyMessage(string sender, string message)
        {
            messageList[sender] += "me" + ": " + message + "\n";
        }

        public string GetMessages(string user)
        {
            return messageList[user];
        }

        public void InitMessageList(string username)
        {
            messageList = new Dictionary<string, string>();
            foreach(ClientUserProfile prof in players)
            {
                messageList.Add(prof.username, "");
            }
            foreach(ClientUserProfile prof in spectators)
            {
                messageList.Add(prof.username, "");
            }

            messageList.Remove(username);
        }

        public void UpdatePlayerListFromWaitingList()
        {
            foreach (Models.ClientUserProfile prof in waitingList)
            {
                players.Add(prof);
            }

            waitingList.Clear();
            foreach (Models.ClientUserProfile prof in waitingListSpec)
            {
                spectators.Add(prof);
            }

            waitingListSpec.Clear();
        }

        public byte[] GetAvatar(string username)
        {
            byte[] array = null;
            foreach(ClientUserProfile prof in players)
            {
                if (prof.username.Equals(username))
                {
                    array = prof.avatar;
                }
            }

            return array;
        }
    }
}
