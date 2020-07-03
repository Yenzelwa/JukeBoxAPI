using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace JukeBox.BLL.Request
{
    [XmlRoot(ElementName = "Envelope",Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class OttVoucherResponse
    {
        [XmlElement("Body")]
        public Body body { get; set; }
        public class Body
        {
            [XmlElement("RedeemVoucherResponse", Namespace = "http://www.ott-mobile.com/")]
            public VoucherResponse voucherResponse { get; set; }
            public class VoucherResponse
            {
                [XmlElement("RedeemVoucherResult")]
                public RedeemVoucherResult redeemVoucherResult { get; set; }
            }
        }
        
        public class RedeemVoucherResult
        {
            [XmlElement("message")]
            public string Message { get; set; }
            [XmlElement("error_code")]
            public string ErrorCode { get; set; }
            [XmlElement("voucherID")]
            public int VoucherId { get; set; }
            [XmlElement("value")]      
            public decimal Amount { get; set; }
            [XmlElement("unique_reference")]
            public string Reference { get; set; }
        }

    }
}
