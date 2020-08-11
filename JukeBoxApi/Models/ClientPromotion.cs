using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class ClientPromotion
    {
        public int PromotionCategoryMapId { get; set; }
        public string ArtistName { get; set; }
        public string PromotionCategoryName { get; set; }
        public void Bind(JukeBox.Data.Get_ClientPromotion_Result cr)
            
        {
            PromotionCategoryMapId = cr.PromotionCategoryMapId;
            ArtistName = cr.ArtistName;
            PromotionCategoryName = cr.PromotionCategoryName;
        }

    }
}