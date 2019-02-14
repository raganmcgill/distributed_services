using System;
using System.Configuration;
using helpers;
using MassTransit;
using notifications.service.consumers;

namespace notifications.service
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

            RunMassTransitReceiverWithRabbit();
        }

        private static void RunMassTransitReceiverWithRabbit()
        {
            var rabbitBusControl = Bus.Factory.CreateUsingRabbitMq(rabbit =>
            {
                var rabbitMqHost = rabbit.Host(new Uri(RabbitMqAddress), settings =>
                {
                    settings.Username(RabbitUsername);
                    settings.Password(RabbitPassword);
                });

                rabbit.ReceiveEndpoint(rabbitMqHost, "RegistrationNotifications", conf =>
                {
                    conf.Bind("Registrations");
                    conf.Consumer<DatabaseRegisteredConsumer>();
                });

                rabbit.ReceiveEndpoint(rabbitMqHost, "SchemaChangedNotifications", conf =>
                {
                    conf.Bind("SchemaChanged");
                    conf.Consumer<DatabaseUpdatedConsumer>();
                });

            });

            rabbitBusControl.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            rabbitBusControl.Stop();
        }
    }
}