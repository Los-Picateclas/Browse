using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BrowseLib
{
    public class Table
    {
        private string name;
        public List<Column> columns;

        public Table(string n)
        {
            name = n;
            columns = new List<Column>();
        }
        //Add a column to the table
        public void addColumn(Column c)
        {
            columns.Add(c);
        }
        public string getName()
        {
            return name;
        }
        public List<string> getColumnNames() {
            List<string> aux = null;
            foreach (Column c in  columns) {
                string n = c.name;
                aux.Add(n);


            }
            return aux;
        }
        public List<Column> getColumns() {
            return columns;
        }

        public void dropColumn(Column c)
        {
            columns.Remove(c);
        }
        public Column selectColumn(Column c)
        {
            return c;
        }
        public int columnSize()
        {
            return columns.Count();

        }
        public Column selectColumn(int i)
        {
            Column c = columns[i];
            return c;
        }

        public int getColumnNumber()
        {
            return columns.Count();
        }

        public void save(Table t)
        {
            //This will create a .txt in the desktop
            //string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),t.getName()+".txt");
            //This one actually works but it is an absolute route 
            //string ruta = "C:\\Users\\docencia\\Documents\\Browse\\" + t.getName() + ".txt";
            string ruta = "..\\data\\Browse\\" + t.getName() + ".txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(ruta))
                {
                    for (int i = 0; i < t.getColumnNumber(); i++)
                    {
                        sw.Write(t.selectColumn(i).type);
                        if (i + 1 != t.getColumnNumber()) { sw.Write(", "); }
                    }
                    sw.WriteLine("");
                    for (int i = 0; i < t.getColumnNumber(); i++)
                    {
                        sw.Write(t.selectColumn(i).name);
                        if (i + 1 != t.getColumnNumber()) { sw.Write(", "); }
                    }
                    sw.WriteLine("");
                    for (int i = 0; i < t.selectColumn(0).getColumnSize(); i++)
                    {
                        sw.WriteLine("");
                        for (int y = 0; y < t.columnSize(); y++)
                        {
                            sw.Write(t.selectColumn(y).getTextFromColumn(i) + ", ");
                        }
                    }
                }
                //MessageBox.Show("Archivo creado!!");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        public void save(Table t, Database db)
        {

            //This will create a .txt in the desktop
            //string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),t.getName()+".txt");
            string ruta = "C:\\Users\\docencia\\Documents\\Browse\\" + db.databaseName + "\\" + t.getName() + ".txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(ruta))
                {
                    for (int i = 0; i < t.getColumnNumber(); i++)
                    {
                        sw.Write(t.selectColumn(i).type);
                        if (i + 1 != t.getColumnNumber()) { sw.Write(", "); }
                    }
                    sw.WriteLine("");
                    for (int i = 0; i < t.getColumnNumber(); i++)
                    {
                        sw.Write(t.selectColumn(i).name);
                        if (i + 1 != t.getColumnNumber()) { sw.Write(", "); }
                    }
                    sw.WriteLine("");
                    for (int i = 0; i < t.selectColumn(0).getColumnSize(); i++)
                    {
                        sw.WriteLine("");
                        for (int y = 0; y < t.columnSize(); y++)
                        {
                            sw.Write(t.selectColumn(y).getTextFromColumn(i) + ", ");
                        }
                    }
                }
                //MessageBox.Show("Archivo creado!!");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        /**  public void loadDatabases(string path)
          {
              DirectoryInfo di = new DirectoryInfo(path);
              DirectoryInfo[] directories = di.GetDirectories();
              foreach (DirectoryInfo fi in directories)
              {
                  databases.Add(new Database(fi.Name));
              }
          }**/


    }
}

