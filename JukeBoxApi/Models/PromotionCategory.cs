using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class PromotionCategory
    {
        public int PromotionCategoryId { get; set; }
        public int PromotionTypeId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public void Bind(JukeBox.Data.GetPromotionCategoryByPromoTypeId_Result cr)
        {
            PromotionCategoryId = cr.PromotionCategoryId;
            PromotionTypeId = cr.FK_PromotionTypeId;
            CategoryName = cr.PromotionCategoryName;
            CategoryImage = cr.CategoryImage;
        }
    }
}