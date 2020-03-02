using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BrowseLib
{
    public class Database
    {
        public List<Table> tables;
        public Database db;
        public string databaseName;
        public string username;
        public string password;

        public Database(string dN, string uN, string pW)
        {
            tables = new List<Table>();
            this.databaseName = dN;
            this.username = uN;
            this.password = pW;
        }


        public void addTable(Table table)
        {
            tables.Add(table);
        }

        public void dropTable(Table table)
        {
            tables.Remove(table);
        }

        public void updateName(string databaseName)
        {
            this.databaseName = databaseName;
        }

        public Table getTable(int position)
        {
            return tables.ElementAt(position);
        }

        public void loadTables (string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                DirectoryInfo[] directories = di.GetDirectories();
                foreach (DirectoryInfo fi in directories)
                {
                    tables.Add(new Table(fi.Name));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void saveTables(string path)
        {
            try
            {
                foreach (Table tb in tables)
                {
                    Directory.CreateDirectory(Path.Combine(path, tb.getName()));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
