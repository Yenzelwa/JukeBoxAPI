using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class ClientPromotion
    {
        public int PromotionMapId { get; set; }
        public string ArtistName { get; set; }
        public string NameType { get; set; }
        public void Bind(JukeBox.Data.Get_ClientPromotion_Result cr)
            
        {
            PromotionMapId = cr.PromotionMapId;
            ArtistName = cr.ArtistName;
            NameType = cr.NameType;
        }

    }
}