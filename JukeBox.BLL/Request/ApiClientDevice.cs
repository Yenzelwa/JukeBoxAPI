using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL.Request
{
   public class ApiClientDevice
    {
        public string imei { get; set; }

        public string iccid { get; set; }

        public string msisdn { get; set; }

        public string imsi { get; set; }

        public string handsetId { get; set; }

        public string channelId { get; set; }

        public string platform { get; set; }

        public string platformVersion { get; set; }

        public long appType { get; set; }

        public string appTypeVersion { get; set; }

        public string apptypeVersionDesc { get; set; }

        public decimal latitude { get; set; }

        public decimal longitude { get; set; }
    }
}
