using JukeBox.BLL.Request;
using JukeBox.BLL.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace JukeBox.BLL.ExternalApi
{
   public class Voucher
    {
       
        public static FlashTokenResponse GetTokenAsync()

        {
            IRestRequest request = new RestRequest("/token", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Basic {Config.FlashConsumerSecretKey}");
            request.AddParameter("grant_type", "client_credentials");
            var response = RestFlash.Execute<FlashTokenResponse>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(response.StatusDescription, new Exception(response.Content));
                }
            }
            return response.Data;

        }

        public  static FlashTokenResponse RefreshTokenAsync(string token)
        {
            IRestRequest request = new RestRequest("/token", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Basic {Config.FlashConsumerSecretKey}");
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", token);
            var response = RestFlash.Execute<FlashTokenResponse>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(response.StatusDescription, new Exception(response.Content));
                }
            }
            return response.Data;

        }

        public static ApiClientOneVoucherRedeemResponse GetApiClientOneVoucher(ApiClientOneVoucherRedeemFilter data ,  string token)
        {
            IRestRequest request = new RestRequest("partner/1voucher-redemption/1.0.0/voucher/redeem", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(data);
            var response = RestFlash.Execute<ApiClientOneVoucherRedeemResponse>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(response.StatusDescription, new Exception(response.Content));
                }
            }
            return response.Data;


        }
    }
}
