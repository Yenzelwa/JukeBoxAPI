using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Config
    {
        //JWT Token
        public static int TokenExpiresMins => int.Parse(System.Configuration.ConfigurationManager.AppSettings["TokenExpiresMins"]);
        public static string JwtTokenSecret => System.Configuration.ConfigurationManager.AppSettings["JwtTokenSecret"];
        public static int CacheDurationHours => int.Parse(System.Configuration.ConfigurationManager.AppSettings["CacheDurationHours"]);

        //External API's
        public static string FlashConsumerSecretKey => System.Configuration.ConfigurationManager.AppSettings["FlashConsumerSecretKey"];
    

    //Logging 
    public static string LoggerAppName => System.Configuration.ConfigurationManager.AppSettings["LoggerAppName"];

        
    }

}
