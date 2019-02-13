using System;
using System.Threading.Tasks;
using messages.events;
using MassTransit;

namespace dashboard.service.consumers
{
    internal class SchemaUpdateConsumer : IConsumer<SchemaChanged>
    {
        public Task Consume(ConsumeContext<SchemaChanged> context)
        {
            var database = context.Message.Database;

            foreach (var table in database.Tables)
            {
                Console.WriteLine(table.Name);
            }

            StorageHelper.StoreDatabaseDefinition(context.Message.Database.ConnectionDetails, database.Tables, "Dashboard");

            return Task.CompletedTask;
        }
    }
}