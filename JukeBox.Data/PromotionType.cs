//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JukeBox.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PromotionType
    {
        public int PromotionTypeId { get; set; }
        public string PromotionTypeName { get; set; }
        public bool Enabled { get; set; }
        public Nullable<decimal> PromotionAmount { get; set; }
        public string PromotionImage { get; set; }
        public Nullable<System.DateTime> PromotionEndDate { get; set; }
        public bool HasCategory { get; set; }
        public Nullable<bool> AllArtistSelected { get; set; }
        public Nullable<System.DateTime> PromotionStartDate { get; set; }
    }
}
