using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class AccessTokenModel
    {
        
            public string Type { get; set; }
            public string Token { get; set; }
            public DateTime ExpiryDateTimeUTC { get; set; }
        
    }
}