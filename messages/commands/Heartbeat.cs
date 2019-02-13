using System;

namespace messages.commands
{
    public interface Heartbeat
    {
        string Name { get; }

        string Path { get; }

        DateTime DateTime { get; }
    }
}