using System;
using System.Collections.Generic;

namespace BrowseLib
{
    public class User
    {

        private string user, password;
        private Profile profile;





        public User(string name, string pass, Profile prof)
        {
            user = name;
            password = pass;
            profile = prof;
           
        }

        public string getName() {
            return user;

        }


    }

}
