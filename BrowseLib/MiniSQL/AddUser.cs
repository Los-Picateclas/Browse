using System;
namespace BrowseLib.MiniSQL
{
    public class AddUser : MiniSQLQuery
    {
        string user, password, profile;
        public AddUser(string us, string pass, string prof)
        {
            user = us;
            password = pass;
            profile = prof;
        }
        public string Execute(Database database)
        {
            return database.addUser(user, password, profile);
        }
    }
}
