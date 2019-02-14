using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using helpers;
using MassTransit;
using messages.commands;
using messages.events;
using script_formatting.service.messages;

namespace script_formatting.service.consumers
{
    internal class FormatScriptConsumer : IConsumer<FormatScript>
    {
        public Task Consume(ConsumeContext<FormatScript> context)
        {
            ConsoleAppHelper.PrintHeader("Header.txt");

            var script = context.Message.Script;

            Console.WriteLine(script);

            Console.WriteLine("");
            Console.WriteLine("--FORMATTING--");
            Console.WriteLine("");

            var formattedSql = NSQLFormatter.Formatter.Format(script);

            Console.WriteLine(formattedSql);

            var message = new ScriptFormattedMessage
            {
                FormattedScript = formattedSql
            };

            context.Publish<ScriptFormatted>(message);

            Console.WriteLine($"Formatted script complete");

            return Task.CompletedTask;
        }
    }
}
