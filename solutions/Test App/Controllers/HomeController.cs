﻿using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using models;

namespace dashboard.ui.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetDatabases()
        {
            var result = new List<Server>();
            string root = $@"C:\dev\Stores\monitor";

            DirectoryInfo rootInfo = new DirectoryInfo(root);

            if (rootInfo.Exists)
            {
                DirectoryInfo[] servers = rootInfo.GetDirectories();

                foreach (var s in servers)
                {
                    var server = new Server
                    {
                        Name = s.Name
                    };

                    DirectoryInfo[] databases = s.GetDirectories();

                    foreach (var d in databases)
                    {
                        var database = new Database
                        {
                            Name = d.Name
                        };

                        var files = d.GetFiles();

                        foreach (var fileInfo in files)
                        {
                            Table table = JsonConvert.DeserializeObject<Table>(System.IO.File.ReadAllText(fileInfo.FullName));

                            database.Tables.Add(table);
                        }

                        server.Databases.Add(database);
                    }

                    result.Add(server);
                }

            }
            
            return PartialView("_DatabaseOverview", result);
        }
    }
}