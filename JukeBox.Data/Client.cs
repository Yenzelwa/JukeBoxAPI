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
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.ClientContactDetails = new HashSet<ClientContactDetail>();
            this.Libraries = new HashSet<Library>();
        }
    
        public int ClientID { get; set; }
        public short FK_ClientStatusID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string ClientTitle { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public short FK_IdentityTypeID { get; set; }
        public string IdentityTypeValue { get; set; }
        public decimal BalanceAvailable { get; set; }
        public short FK_CompanyID { get; set; }
        public string ClientPassword { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public Nullable<short> FK_CountryID { get; set; }
        public string Gender { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<decimal> CreditAmount { get; set; }
        public Nullable<bool> Enabled { get; set; }
        public string ArtistImage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientContactDetail> ClientContactDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Library> Libraries { get; set; }
    }
}
