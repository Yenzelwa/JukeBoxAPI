using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class VoucherRequest
    {
        public string VoucherPin { get; set; }
        public long CustomerId { get; set; }
    }
}