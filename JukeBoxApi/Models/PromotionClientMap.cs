using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class PromotionClientMap
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PromotionMapId { get; set; }
        public bool? Remove { get; set; }
        public void Bind(JukeBox.Data.GetPromoionClientMap_Result cr)
        {
            ClientId = cr.ClientID;
            FirstName = cr.FirstName;
            LastName = cr.LastName;
            PromotionMapId = cr.PromotionMapId;
            Remove = cr.RemoveEnabled;
        }
    }
}