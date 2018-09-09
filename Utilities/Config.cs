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
        public static string SyXApiUsername => System.Configuration.ConfigurationManager.AppSettings["SyXApiUsername"];
        public static string ReferAFriendApiUsername => System.Configuration.ConfigurationManager.AppSettings["ReferAFriendApiUsername"];
        public static string VoucherTopUpApiUsername => System.Configuration.ConfigurationManager.AppSettings["VoucherTopUpApiUsername"];
        public static string BetTechApiUsername => System.Configuration.ConfigurationManager.AppSettings["BetTechApiUsername"];
        public static string AfricaBetsSMSUsername => System.Configuration.ConfigurationManager.AppSettings["AfricaBetsSMSUsername"];
        public static int ExternalSyxUserId => Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ExternalSyxUserId"]);

        //CMS
        public static string CmsBaseUrl => System.Configuration.ConfigurationManager.AppSettings["CmsBaseUrl"];
        public static string CmsUsername => System.Configuration.ConfigurationManager.AppSettings["CmsUsername"];
        public static string CmsPassword => System.Configuration.ConfigurationManager.AppSettings["CmsPassword"];
        public static string CmsClientId => System.Configuration.ConfigurationManager.AppSettings["CmsClientId"];
        public static string CmsGrantType => System.Configuration.ConfigurationManager.AppSettings["CmsGrantType"];
        public static string CmsWebPlatformId => System.Configuration.ConfigurationManager.AppSettings["CmsWebPlatformId"];
        public static string CmsMobPlatformId => System.Configuration.ConfigurationManager.AppSettings["CmsMobPlatformId"];

        public static string BetGamesApiKey => System.Configuration.ConfigurationManager.AppSettings["BetGamesApiKey"];
        public static string DSVirtualUserName => System.Configuration.ConfigurationManager.AppSettings["DSVirtualUserName"];

        //Africa Bet
        public static string AfricaBetSmtp => System.Configuration.ConfigurationManager.AppSettings["AfricaBetSmtp"];
        public static string AfricaBetFromEmail => System.Configuration.ConfigurationManager.AppSettings["AfricaBetFromEmail"];
        public static string AfricaBetHost => System.Configuration.ConfigurationManager.AppSettings["AfricaBetHost"];

        // Blue Label
    

    //Logging 
    public static string LoggerAppName => System.Configuration.ConfigurationManager.AppSettings["LoggerAppName"];

        
    }

}
