using System.Collections.Generic;

namespace models
{
    public class Table
    {
        public string Schema { get; set; }

        public string Name { get; set; }

        public List<Column> Columns { get; set; }

        public Table()
        {
            Columns = new List<Column>();
        }
    }
}