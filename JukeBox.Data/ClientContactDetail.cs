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
    
    public partial class ClientContactDetail
    {
        public int ClientContactDetailID { get; set; }
        public int FK_ClientID { get; set; }
        public short FK_ClientContactTypeID { get; set; }
        public string ContactInformation { get; set; }
        public string AddressLine { get; set; }
        public string AddressSuburb { get; set; }
        public string AddressCity { get; set; }
        public Nullable<int> FK_RegionID { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
    
        public virtual ClientContactType ClientContactType { get; set; }
        public virtual Client Client { get; set; }
    }
}
