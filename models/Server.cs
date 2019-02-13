using System.Collections.Generic;

namespace models
{
    public class Server
    {
        public string Name { get; set; }

        public List<Database> Databases { get; set; }

        public Server()
        {
            Databases = new List<Database>();
        }
    }
}