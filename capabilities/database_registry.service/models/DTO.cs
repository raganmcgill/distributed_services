using messages.commands;
using models;

namespace database_registry.service.models
{
    public class DTO : RegisterDatabase
    {
        public ConnectionDetails ConnectionDetails { get; set; }
    }
}