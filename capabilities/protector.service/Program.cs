using System;
using System.Configuration;
using helpers;
using MassTransit;
using protector.service.consumers;

namespace protector.service
{
    class Program
    {
        private static readonly string RabbitMqAddress = ConfigurationManager.AppSettings["RabbitHost"];
        private static readonly string RabbitUsername = ConfigurationManager.AppSettings["RabbitUserName"];
        private static readonly string RabbitPassword = ConfigurationManager.AppSettings["RabbitPassword"];

        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 20);
            ConsoleAppHelper.PrintHeader("Header.txt");

            var rabbitBusControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri(RabbitMqAddress), h =>
                {
                    h.Username(RabbitUsername);
                    h.Password(RabbitPassword);
                });

                //                sbc.ReceiveEndpoint(host, "ProtectorQueue", ep =>
                //                {
                //                    ep.Bind("DatabaseRegistered");
                //                    ep.Consumer(() => new DatabaseRegisteredConsumer());
                //                });
                //
                //                sbc.ReceiveEndpoint(host, "ProtectorQueue", ep =>
                //                {
                //                    ep.Bind("DatabaseRegistered");
                //                    ep.Consumer(() => new DatabaseRegisteredConsumer());
                //                });

                sbc.ReceiveEndpoint(host, "SchemaChanged", ep =>
                {
                    ep.Consumer(() => new SchemaChangedConsumer() );
                });

            });
            rabbitBusControl.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            rabbitBusControl.Stop();
        }
    }
}
