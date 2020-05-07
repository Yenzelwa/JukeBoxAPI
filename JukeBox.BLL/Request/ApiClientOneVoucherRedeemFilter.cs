using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL.Request
{
  public  class ApiClientOneVoucherRedeemFilter
    {

        [JsonProperty("user")]
        public ApiClientUser user { get; set; }


        public long sequenceNumber { get; set; }

        public string voucherPin { get; set; }

        [JsonProperty("acquirer")]
        public ApiClientAcquirer acquirer { get; set; }

        public long amountRequested { get; set; }

        public string currency { get; set; }

    }
}
