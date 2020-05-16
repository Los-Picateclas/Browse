using System;
namespace BrowseLib.MiniSQL
{
    public class DropProfile : MiniSQLQuery
    {
        private string name;

        public DropProfile(string profile)
        {
            profile = name;
        }

        public string Execute(Database database)
        {
            
            return database.dropProfile(name, database);
            
        }
    }
}
