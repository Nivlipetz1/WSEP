using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient
{
    public class Pair<Username,Value>
    {
        Username username;
        Value value;

        public Pair(Username username, Value v)
        {
            this.username = username;
            this.value = v;
        }

        public Value getValue()
        {
            return this.value;
        }

        public Username getUsername()
        {
            return this.username;
        }
    }
}