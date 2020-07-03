using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JukeBox.BLL.Request
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class OttVoucherRequest
    {
        [XmlElement("Body")]
        public Body body { get; set; }
        public class Body
        {
            [XmlElement("RedeemVoucher", Namespace = "http://www.ott-mobile.com/")]
            public RedeemVoucher redeemVoucher { get; set; }

        }
        public class RedeemVoucher
        {
            [XmlElement("userName")]
            public string UserName { get; set; }
            [XmlElement("password")]
            public string Password { get; set; }
            [XmlElement("unique_reference")]
            public string UniqueReference { get; set; }
            [XmlElement("VendorID")]
            public int VendorId { get; set; }
            [XmlElement("pinCode")]
            public string PinCode { get; set; }
            [XmlElement("accountCode")]
            public string AccountCode { get; set; }
            [XmlElement("clientID")]
            public string ClientId { get; set; }
            [XmlElement("msisdn")]
            public string msisdn { get; set; }
        }
    }
}
