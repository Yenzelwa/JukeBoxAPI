using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL.Response
{
   public class ApiClientOneVoucherRedeemResponse
    {
        public string actionCode { get; set; }
       // {"actionCode":"1821","screenMessage":"Invalid One Voucher PIN provided.","sequenceNumber":202005071695178,"receipt":null,"amountAuthorised":0,"balance":29100,"availableBalance":29100,"currency":null,"transactionReference":"732559183","transactionDate":null,"changeVoucher":null}
    public string screenMessage { get; set; }
        public string transactionReference { get; set; }
        public long sequenceNumber { get; set; }
        public string receipt { get; set; }

        public decimal? balance { get; set; }

        public decimal amountAuthorised { get; set; }
        public string currency { get; set; }
        public DateTime? transactionDate { get; set; }
        [JsonProperty("changeVoucher")]
        public ChangeVoucher changeVoucher { get; set; }


        public class ChangeVoucher
        {
            
            public int? amout { get; set; }

            public long? voucherPin { get; set; }
            public long? voucherSerial { get; set; }
            public DateTime? expiryDate { get; set; }
        }

    }
}
