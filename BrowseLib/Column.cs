using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib
{
    public class Column
    {
        public string name;
        public string type;
        public List<Object> columns;
        public Column(string name, string type)
        {
            this.name = name;
            this.type = type;
            if (type == "TEXT")
            {
                List<string> columns = new List<string>();
            }
            if (type == "INT")
            {
                List<int> columns = new List<int>();
            }
            if (type == "DOUBLE")
            {
                List<double> columns = new List<double>();
            }
        }

    }
}


