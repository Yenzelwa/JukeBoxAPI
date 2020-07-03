using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JukeBox.BLL.Response
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
   public class OttVoucherStatusResponse
    {
        [XmlElement("Body")]
        public Body body { get; set; }
        public class Body
        {
            [XmlElement("GetStatusResponse", Namespace = "http://www.ott-mobile.com/")]
            public VoucherStatusResponse voucherStatusResponse { get; set; }
            public class VoucherStatusResponse
            {
                [XmlElement("GetStatusResult")]
                public StatusResult statusResult { get; set; }
            }
            public class StatusResult
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
}
