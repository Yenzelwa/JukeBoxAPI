using JukeBox.BLL.Request;
using JukeBox.BLL.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace JukeBox.BLL.ExternalApi.OTT
{
    class VoucherOTT
    {
        public static OttVoucherResponse RedeemOTT(OttVoucherRequest ottVoucherRequest)
        {
            var body = SerializeToXml<OttVoucherRequest>(ottVoucherRequest);
            IRestRequest request = new RestRequest("retail/Redeem.asmx", Method.POST);
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "http://www.ott-mobile.com/RedeemVoucher");
            request.AddParameter("", body, ParameterType.RequestBody);
            var response = RestOTT.Execute(request);
            var result = DeserializeToObject<OttVoucherResponse>(response.Content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(response.StatusDescription, new Exception(response.Content));
                }
            }
            return result;

        }
        public static OttVoucherStatusResponse CheckVoucherStatusOTT(OttCheckStatusVoucherRequest ottVoucherRequest)
        {
            var body = SerializeToXml<OttCheckStatusVoucherRequest>(ottVoucherRequest);
            IRestRequest request = new RestRequest("retail/CheckRedeemStatus.asmx", Method.POST);
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "http://www.ott-mobile.com/GetStatus");
            request.AddParameter("", body, ParameterType.RequestBody);
            var response = RestOTT.Execute(request);
            var result = DeserializeToObject<OttVoucherStatusResponse>(response.Content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(response.StatusDescription, new Exception(response.Content));
                }
            }
            return result;

        }
        public static T DeserializeToObject<T>(string xml) where T : class
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(xml))
            {             
                return (T)serializer.Deserialize(reader);
            }
        }

        public static string SerializeToXml<T>(object obj) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    ser.Serialize(writer, obj);
                    xml = sww.ToString(); // Your XML
                }
            }
            return xml;
        }
    }
}
