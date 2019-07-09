using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL.Request
{
   public class ApiClientUser
    {
        [Required]
        public string authUserName { get; set; }

        [Required]
        public string authPassWord { get; set; }
    }
}
