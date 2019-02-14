using helpers;
using System;
using MassTransit;
using script_formatting.service.consumers;
using System.Configuration;

namespace script_formatting.service
{
    class Program
    {
        private static readonly string RabbitMqAddress = ConfigurationManager.AppSettings["RabbitHost"];
        private static readonly string RabbitUsername = ConfigurationManager.AppSettings["RabbitUserName"];
        private static readonly string RabbitPassword = ConfigurationManager.AppSettings["RabbitPassword"];

        static void Main(string[] args)
        {

            ConsoleAppHelper.PrintHeader("Header.txt");

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri(RabbitMqAddress), h =>
                {
                    h.Username(RabbitUsername);
                    h.Password(RabbitPassword);
                });

                sbc.ReceiveEndpoint(host, "FormatScript", ep =>
                {
                    //ep.Bind(exchangeName);
                    ep.Consumer(() => new FormatScriptConsumer());
                });

            });

            bus.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
    }
}