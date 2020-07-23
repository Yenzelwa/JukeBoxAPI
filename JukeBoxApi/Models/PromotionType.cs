using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class PromotionType
    {
        public int PromotionTypeId { get; set; }
        public string PromotionTypeName { get; set; }
        public string PromotionImage { get; set; }
        public void Bind(JukeBox.Data.GetAllPromotionType_Result cr)
        {
            PromotionTypeId = cr.PromotionTypeId;
            PromotionTypeName = cr.PromotionTypeName;
            PromotionImage = cr.PromotionImage;
        }
    }
}