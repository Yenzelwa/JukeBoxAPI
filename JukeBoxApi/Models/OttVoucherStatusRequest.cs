using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class OttVoucherStatusRequest
    {
        public string UniqueReference { get; set; }
        public long CustomerId { get; set; }
    }
}