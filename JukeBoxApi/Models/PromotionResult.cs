using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class PromotionResult
    {
        public int PromotionTypeId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistImage { get; set; }
        public int? NumberOfVotes { get; set; }
        public void Bind(JukeBox.Data.GetPromotionResultByType_Result cr)
        {
            PromotionTypeId = cr.FK_PromotionTypeId;
            ArtistName = cr.ArtistName;
            ArtistImage = cr.ArtistImage;
            NumberOfVotes = cr.NumberOfVotes;
        }
    }
}