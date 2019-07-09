using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace JukeBox.BLL.ExternalApi
{
    public static class RestFlash
    {
        private static bool _ranOnce;
        private static readonly RestClient client = new RestClient(ConfigurationManager.AppSettings["Flash"]);


        private static int hitCounter = 0;

        private static void InitializeOnce()
        {
            if (_ranOnce) return;
            client.AddDefaultHeader("Content-Type", "application/json");
            client.AddHandler("application/json", JsonSerializer.Default);
            client.AddHandler("text/json", JsonSerializer.Default);
            client.AddHandler("text/x-json", JsonSerializer.Default);
            client.AddHandler("text/javascript", JsonSerializer.Default);
            client.AddHandler("*+json", JsonSerializer.Default);
            _ranOnce = true;
        }

        public static IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
        {
            hitCounter++;
            Debug.WriteLine("Web Request made to URI: " + request.Resource + " with hit counter: " + hitCounter);
            InitializeOnce();

            //Ignore bad certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;

            return client.Execute<T>(request);
        }

        public static async Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request) where T : new()
        {
            hitCounter++;
            Debug.WriteLine("Web Async Request made to URI: " + request.Resource + " with hit counter: " + hitCounter);
            InitializeOnce();

            //ignore bad certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;

            return await client.ExecuteTaskAsync<T>(request);
        }

        private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
