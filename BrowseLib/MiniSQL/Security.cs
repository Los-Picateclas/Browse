using System;
using System.Collections.Generic;

namespace BrowseLib.MiniSQL
{
    public class Security
    {

        public List<User> users;
        public List<Profile> profiles;

        public Security()
        {
            users = new List<User>();
            profiles = new List<Profile>();

        }
    }
}
