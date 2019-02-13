using models;

namespace messages
{
    public interface DatabaseRegistered
    {
        Database Schema { get; }

        ConnectionDetails Database { get; }
    }
}
