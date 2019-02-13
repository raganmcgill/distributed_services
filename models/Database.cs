using System.Collections.Generic;

namespace models
{
    public class Database
    {
        public ConnectionDetails ConnectionDetails { get; set; }

        public string Name { get; set; }

        public List<Table> Tables { get; set; }

        public Database()
        {
            Tables = new List<Table>();
        }

    }
}
