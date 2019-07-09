using JukeBox.BLL.Request;
using JukeBox.BLL.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace JukeBox.BLL.ExternalApi
{
   public class Voucher
    {
        public static ApiClientOneVoucherRedeemResponse GetApiClientOneVoucher(ApiClientOneVoucherRedeemFilter data)
        {
            IRestRequest request = new RestRequest("v1/transaction/onevoucher/redeem", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = JsonSerializer.Default;
            request.AddBody(data);

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
