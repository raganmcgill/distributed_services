using helpers;
using System;
using System.Threading.Tasks;
using messages;
using MassTransit;
using messages.commands;
using monitor.service.messages;
using monitor.service.models;

namespace monitor.service.consumers
{
    internal class RegisterDatabaseConsumer : IConsumer<RegisterDatabase>
    {
        public Task Consume(ConsumeContext<RegisterDatabase> context)
        {
            ConsoleAppHelper.PrintHeader("Header.txt");

            var connectionDetails = context.Message.ConnectionDetails;

            var schema = Helper.GetSchemaInfo(connectionDetails);

            StorageHelper.StoreDatabaseDefinition(connectionDetails, schema.Tables, "monitor");

            var message = new DatabaseRegisteredMessage
            {
                Database = connectionDetails,
                Schema = schema
            };

            context.Publish<DatabaseRegistered>(message);

            Console.WriteLine($"Registered database with {schema.Tables.Count} tables at {DateTime.Now}");

            return Task.CompletedTask;
        }
    }
}
