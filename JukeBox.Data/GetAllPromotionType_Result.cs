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
    
    public partial class GetAllPromotionType_Result
    {
        public int PromotionTypeId { get; set; }
        public string PromotionTypeName { get; set; }
        public string PromotionImage { get; set; }
        public Nullable<decimal> PromotionAmount { get; set; }
        public Nullable<System.DateTime> PromotionEndDate { get; set; }
        public Nullable<System.DateTime> PromotionStartDate { get; set; }
        public bool HasCategory { get; set; }
        public Nullable<bool> AllArtistSelected { get; set; }
    }
}
