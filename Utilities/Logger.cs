
using System;
using System.Configuration;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web;

namespace Utilities
{
    public class Logger:ILogger
    {
       // public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void LogInfo(string controller, string route, string message)
        {
           // log.Info($"Level:{controller}. Action: {route}. Message: {message}. Page:{HttpContext.Current.Request.Url.ToString()}");
            //SendLog(new LogModel
            //{
            //    Level = controller,
            //    IPAddress = GetIPAddress(),
            //    Action = route,
            //    Message = message,
            //    Page = HttpContext.Current.Request.Url.ToString(),
            //    UserAgent = GetUserAgent(),
            //    UserName = "BETAPI", // TODO: Get name using platform auth
            //    ApplicationName = Config.LoggerAppName
            //});
        }

        public void LogError(string controller, string route, string errorMessage)
        {
           // log.Info($"Level:{controller}. Action: {route}. Message: {errorMessage}. Page:{HttpContext.Current.Request.Url.ToString()}");
            //SendLog(new LogModel
            //{
            //    Level = controller,
            //    IPAddress = GetIPAddress(),
            //    Action = route,
            //    Message = errorMessage,
            //    Page = HttpContext.Current.Request.Url.ToString(),
            //    UserAgent = GetUserAgent(),
            //    UserName = "BETAPi", // TODO: Get name using platform auth
            //    ApplicationName = Config.LoggerAppName
            //});
        }

        //public void SendLog(LogModel lm)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["LoggerURL"]);

        //        var json = Newtonsoft.Json.JsonConvert.SerializeObject(lm);

        //        HttpResponseMessage res =  client.PostAsync("/api/log", new StringContent(json, Encoding.UTF8, "application/json")).Result;
        //        res.EnsureSuccessStatusCode();
        //    }
        //}

        private static string GetIPAddress()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
        }

        private static string GetUserAgent()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
        }
    }
}
