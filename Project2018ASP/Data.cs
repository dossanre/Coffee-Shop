using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration; 

namespace Project2018ASP
{
    public class Data
    {
        public static string getConnectionString(string connectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }
    }
}