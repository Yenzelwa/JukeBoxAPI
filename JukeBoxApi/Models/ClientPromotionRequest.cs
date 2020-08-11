using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class ClientPromotionRequest
    {
        public int? PromotionCategoryMapId { get; set; }
        public int? PromotionCategoryId { get; set; }
        public int? ClientId { get; set; }
        public bool? Enabled { get; set; }
    }
}