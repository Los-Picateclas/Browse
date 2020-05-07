using System;
using System.Collections.Generic;

namespace BrowseLib
{
    public class Profile
    {
        public List<Privileges> privileges;
        public Profile()
        {
            privileges = new List<Privileges>();
        }
        public void addPrivilege(Privileges p)
        {
            privileges.Add(p);

        }
        public void removePrivilege(Privileges p)
        {
            privileges.Remove(p);

        }


    }

}
