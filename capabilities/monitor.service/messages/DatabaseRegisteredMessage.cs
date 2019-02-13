using messages;
using models;

namespace monitor.service.messages
{
    public class DatabaseRegisteredMessage : DatabaseRegistered
    {
        public Database Schema { get; set; }

        public ConnectionDetails Database { get; set; }
    }
}
