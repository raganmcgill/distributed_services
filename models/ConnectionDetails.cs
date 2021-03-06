﻿namespace models
{
    public class ConnectionDetails
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ServerWithoutSlashes
        {
            get { return Server.Replace(@"\", @"~"); }
        }
    }
}