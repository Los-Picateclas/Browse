﻿using System;
namespace BrowseLib.MiniSQL
{
    public class Revoke : MiniSQLQuery
    {
        string privilege, table, profile;
        public Revoke(string pri, string tab, string pr)
        {
            privilege = pri;
            table = tab;
            profile = pr;
        }

        public string Execute(Database database)
        {
            return database.revoke(privilege, table, profile);
        }
    }
}
