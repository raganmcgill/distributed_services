using System;
using System.Configuration;
using System.Net.Http;
using helpers;
using Microsoft.Owin.Hosting;

namespace database_registry.service
{
    class Program
    {
        private static readonly string RabbitMqAddress = ConfigurationManager.AppSettings["RabbitHost"];
        private static readonly string RabbitUsername = ConfigurationManager.AppSettings["RabbitUserName"];
        private static readonly string RabbitPassword = ConfigurationManager.AppSettings["RabbitPassword"];

        static void Main(string[] args)
        {
            var baseAddress = "http://localhost:9111/api/registry/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();

                Console.SetWindowSize(80, 30);
                ConsoleAppHelper.PrintHeader("Header.txt");

                Console.WriteLine(string.Empty);

                Console.WriteLine($"The api is live on {baseAddress}");

                Console.WriteLine(string.Empty);

                ConsoleAppHelper.PrintHeader("Payload.txt",false);

                Console.ReadLine();
            }
        }
    }
}
