using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BrowseLib
{
    public class Column
    {
        public string name;
        public string type;
        public List<string> column;
        public Column(string name, string type)
        {
            this.name = name;
            this.type = type;
            column = new List<string>();
        }

        public void insert(string text)
        {

            column.Add(text);
            //MessageBox.Show("añadido");
        }

        public int getIntFromColumn(int position)
        {


            string t = column[position];


            int m = Int32.Parse(t);
            return m;


        }
        public double getDoubleFromColumn(int position)
        {


            string t = column[position];


            double m = double.Parse(t);
            return m;


        }


        public string getTextFromColumn(int position)
        {

            return column[position];


        }
        public int getColumnSize()
        {

            return column.Count;

        }


        public void updateColumn(int position, string update) {

            column[position] = update;
        
        }

        

     

    }
}


