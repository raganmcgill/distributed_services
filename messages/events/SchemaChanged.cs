using models;

namespace messages.events
{
    public interface SchemaChanged
    {
        Database Database { get; }
    }
}
