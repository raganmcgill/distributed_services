using models;

namespace messages.commands
{
    public interface RegisterDatabase
    {
        ConnectionDetails ConnectionDetails { get; set; }
    }
}
