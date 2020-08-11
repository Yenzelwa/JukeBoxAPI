using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class PromotionCategoryRequest
    {
        public int PromotionCategoryId { get; set; }
        public int PromotionTypeId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public bool? Enabled { get; set; }
    }
}