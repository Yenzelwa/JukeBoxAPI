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
    
    public partial class LibraryStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LibraryStatu()
        {
            this.LibraryDetails = new HashSet<LibraryDetail>();
        }
    
        public short LibraryStatusID { get; set; }
        public string LibraryStatusName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryDetail> LibraryDetails { get; set; }
    }
}
