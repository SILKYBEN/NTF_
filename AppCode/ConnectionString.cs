using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NTF.AppCode
{
    public class ConnectionString
    {
        public static string Connection()
        {
            return ConfigurationManager.ConnectionStrings[DBPointerName].ConnectionString;
        }
        internal const string DBPointerName = "ntfDb";
    }
}