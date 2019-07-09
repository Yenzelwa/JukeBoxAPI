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

        public object errorMessage { get; set; }
        public string transactionReference { get; set; }

        public LedgerBalance ledgerBalance { get; set; }

        public AvailableBalance availableBalance { get; set; }

        public string receipt { get; set; }

        public AmountAuthorised amountAuthorised { get; set; }

        public class LedgerBalance
        {
            public string currency { get; set; }

            public int value { get; set; }
        }

        public class AvailableBalance
        {
            public string currency { get; set; }

            public int value { get; set; }
        }

        public class AmountAuthorised
        {
            public string currency { get; set; }

            public int value { get; set; }
        }
    }
}
