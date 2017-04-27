using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUtilities
{
    public class League
    {
         int minimumRank;
        HashSet<UserProfile> users;
        public League(int minimumRank)
        {
            this.minimumRank = minimumRank;
            users = new HashSet<UserProfile>();
        }
        public int MinimumRank
        {
            set { minimumRank = value; }
            get { return minimumRank; }
        }

        public bool addUser(UserProfile user)
        {
            if (users.Contains(user))
                return false;
            users.Add(user);
            return true;
        }

        public bool removeUser(UserProfile user)
        {
            if (!users.Contains(user))
                return false;
            users.Remove(user);
            return true;
        }
        public void update(int newRank)
        {
            List<UserProfile> UserToRemove = new List<UserProfile>();
            foreach(UserProfile user in users)
            {
                if (user.Credit < newRank)
                    UserToRemove.Add(user);
            }
            foreach(UserProfile user in UserToRemove)
            {
                users.Remove(user);
            }
            minimumRank = newRank;
        }
        public bool isUser(UserProfile user)
        {
            return users.Contains(user);
        }

    }
}
