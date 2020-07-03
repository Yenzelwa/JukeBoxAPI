using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JukeBox.BLL.Request
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class OttCheckStatusVoucherRequest
    {
        [XmlElement("Body")]
        public Body body { get; set; }
        public class Body
        {
                [XmlElement("GetStatus", Namespace = "http://www.ott-mobile.com/")]
                public GetStatus getStatus { get; set; }
            
        }
        public class GetStatus
        {
            [XmlElement("userName")]
            public string UserName { get; set; }
            [XmlElement("password")]
            public string Password { get; set; }
            [XmlElement("unique_reference")]
            public string UniqueReference { get; set; }
        }
    }
}
