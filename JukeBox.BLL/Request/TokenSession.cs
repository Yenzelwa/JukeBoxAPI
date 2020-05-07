using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL.Request
{
    public class TokenSession
    {
        public string accessToken { get; set; }
        public DateTime ExpDate { get; set; }

        public string refreshToken { get; set; }
    }
}
