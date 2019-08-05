using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class TokenResponse
    {
        
        public int TokenResponseId { get; set; }

        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }

        public string UserName { get; set; }

        public DateTime Issued { get; set; }

        public DateTime Expires { get; set; }
        public string ErrorDescription { get; set; }

    }
}