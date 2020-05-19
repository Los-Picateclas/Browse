using System;
namespace BrowseLib.MiniSQL
{
    public class Grant : MiniSQLQuery
    {
        string privilege, table, profile;
        public Grant(string pri, string tab, string pr)
        {
            privilege = pri;
            table = tab;
            profile = pr;
        }

        public string Execute(Database database)
        {
            return database.grant(privilege, table, profile);
        }
    }
}
