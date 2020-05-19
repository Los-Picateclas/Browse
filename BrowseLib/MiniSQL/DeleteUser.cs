using System;
namespace BrowseLib.MiniSQL
{
    public class DeleteUser : MiniSQLQuery
    {
        string user;
        public DeleteUser(string us)
        {
            user = us;
        }

        public string Execute(Database database)
        {
            return database.deleteUser(user, database);
        }
    }
}
