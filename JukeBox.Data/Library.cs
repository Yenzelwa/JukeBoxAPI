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
    
    public partial class Library
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Library()
        {
            this.LibraryDetails = new HashSet<LibraryDetail>();
        }
    
        public long LibraryID { get; set; }
        public int FK_ClientID { get; set; }
        public short FK_LibraryTypeID { get; set; }
        public string LibraryName { get; set; }
        public string LibraryCoverFilePath { get; set; }
        public string LibraryDescription { get; set; }
        public string LibraryFilePath { get; set; }
        public Nullable<decimal> Price { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<bool> Enabled { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual LibraryType LibraryType { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryDetail> LibraryDetails { get; set; }
    }
}
