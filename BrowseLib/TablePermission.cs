using System;
using System.Collections.Generic;

namespace BrowseLib
{
    public class TablePermission
    {

        private string table;
        private List<Privileges> privileges;



        public TablePermission(string tbname)
        {
            table = tbname;
            privileges = new List<Privileges>();
        }
        public string getTableName() {
            return table;
        }

        public void addPrivilege(Privileges p) {
            privileges.Add(p);
        }
        public void removePrivilege(Privileges p)
        {

            privileges.Remove(p);
        }
        public List<Privileges> getPrivileges() { return privileges; }

        public Boolean samePrivileges(TablePermission tp) {
            Boolean samePrivileges = true;
            foreach (Privileges p in privileges) {
                if (!tp.getPrivileges().Contains(p)) {

                    samePrivileges = false;

                }
            }

            return samePrivileges;
        }

    }


}
