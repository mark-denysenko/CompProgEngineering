using CustomMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatrixWeb.Patterns
{
    public class Singleton
    {
        public string ServerName { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"Server={ServerName};Database={DbName};User Id={UserName};Password={Password};";
            }
        }

        private static Singleton instance = new Singleton();
        
        private Singleton() { }

        public static Singleton GetInstance() => instance;
    }
}