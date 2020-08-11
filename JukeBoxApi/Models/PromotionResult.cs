using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class PromotionResult
    {
        public int PromoNumber { get; set; }
        public int? PromotionTypeId { get; set; }
        public int PromoMapId { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistImage { get; set; }
        public int? NumberOfVotes { get; set; }
        public int? Duration { get; set; }
        public void Bind(JukeBox.Data.GetPromotionResultByType_Result cr)
        {
            PromotionTypeId = cr.FK_PromotionTypeId;
            PromoMapId = cr.PromotionCategoryMapId;
            ArtistId = cr.ClientID;
            ArtistName = cr.ArtistName;
            ArtistImage = cr.ArtistImage;
            NumberOfVotes = cr.NumberOfVotes;
            Duration = cr.Duration;
        }
    }
}