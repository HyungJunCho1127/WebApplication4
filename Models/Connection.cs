using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Connection
    {
        public string GetConnection()
        {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            C:\Users\davch\source\repos\WebApplication4\App_Data\Database1.mdf;Integrated Security=True;
            MultipleActiveResultSets=True;Application Name=EntityFramework";
        }
        
    }
}