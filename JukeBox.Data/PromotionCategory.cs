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
    
    public partial class PromotionCategory
    {
        public int PromotionCategoryId { get; set; }
        public int FK_PromotionTypeId { get; set; }
        public string PromotionCategoryName { get; set; }
        public bool Enabled { get; set; }
        public string CategoryImage { get; set; }
        public bool AllArtistSelected { get; set; }
    }
}
