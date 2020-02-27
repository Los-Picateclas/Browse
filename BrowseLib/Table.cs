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

        private List<Column> columns;

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

        public void dropColumn(Column c)
        {
            columns.Remove(c);
        }
        public Column selectColumn(Column c)
        {


            return c;
        }
        public Column selectColumn(int i)
        {
            Column c = columns[i];

            return c;
        }

        public int getColumnNumber() {
            return columns.Count();
        }

        

        public void save(Table t)
        {
            int numero = 10;

            //This will create a .txt in the desktop
            string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                      t.getName()+".txt");
            try
            {
                using (StreamWriter sw = new StreamWriter(ruta))
                {
                   

                    for (int i=0; i<t.getColumnNumber(); i++) {
                        sw.WriteLine(t.selectColumn(i).name);




                    }
                }
                MessageBox.Show("Archivo creado!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

     
    }
}
