using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EMS_IMD.DAL.Gateway
{
    public class DBConnection
    {
        string connectionString = "";
        public DBConnection()
        {
           
            SAConnStrReader();
           
        }

      


        public string SAConnStrReader()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            return connectionString;
        }
       

    }
}