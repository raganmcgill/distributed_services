﻿using helpers;
using System;
using System.Configuration;
using System.IO;
using System.Timers;
using hospital.service.consumers;
using messages.commands;
using MassTransit;
using Newtonsoft.Json;

namespace hospital.service
{
    class Program
    {
        private static readonly string RabbitMqAddress = ConfigurationManager.AppSettings["RabbitHost"];
        private static readonly string RabbitUsername = ConfigurationManager.AppSettings["RabbitUserName"];
        private static readonly string RabbitPassword = ConfigurationManager.AppSettings["RabbitPassword"];

        static void Main(string[] args)
        {
            Console.SetWindowSize(50, 30);
            ConsoleAppHelper.PrintHeader("Header.txt");

            var rabbitBusControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri(RabbitMqAddress), h =>
                {
                    h.Username(RabbitUsername);
                    h.Password(RabbitPassword);
                });

                sbc.ReceiveEndpoint(host, "Heartbeat", ep =>
                {
                    ep.Consumer(() => new HeartbeatConsumer());
                });

            });
            rabbitBusControl.Start();


            Timer timer = new Timer(2000);
            timer.Elapsed += TimerTick;
            timer.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            timer.Stop();

            rabbitBusControl.Stop();
        }

        static void TimerTick(Object obj, ElapsedEventArgs e)
        {
            ConsoleAppHelper.PrintHeader("Header.txt");

            GetStatuses();
        }

        public static void PrintTL(string file, ConsoleColor colour)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            ConsoleAppHelper.PrintHeader("Header.txt");

            Console.ForegroundColor = colour;

            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(file);

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                Console.WriteLine(string.Empty);

                //close the file
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }
            finally
            {
                ///Console.WriteLine("Executing finally block.");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        static void GetStatuses()
        {

            string root = $@"C:\dev\Stores\hospital\";

            DirectoryInfo rootInfo = new DirectoryInfo(root);

            if (rootInfo.Exists)
            {
                var files = rootInfo.GetFiles();

                foreach (var fileInfo in files)
                {
                    var item = JsonConvert.DeserializeObject<Beat>(System.IO.File.ReadAllText(fileInfo.FullName));

                    if (item.Age <= 5)
                    {
                        PrintTL("TL_Green.txt", ConsoleColor.Green);
                    }
                    else if (item.Age > 5 && item.Age <= 15)
                    {
                        PrintTL("TL_Amber.txt", ConsoleColor.Yellow);
                    }
                    else
                    {
                        PrintTL("TL_Red.txt", ConsoleColor.Red);
                    }
                }
            }

        }

        public class Beat : Heartbeat
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public DateTime DateTime { get; set; }

            public double Age
            {
                get
                {
                    var diffInSeconds = (DateTime.Now - DateTime).TotalSeconds;
                    return diffInSeconds;
                }
            }
        }
    }
}
