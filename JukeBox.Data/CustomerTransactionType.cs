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
    
    public partial class CustomerTransactionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerTransactionType()
        {
            this.CustomerTransactions = new HashSet<CustomerTransaction>();
        }
    
        public short CustomerTransactionTypeID { get; set; }
        public string CustomerTransactionTypeName { get; set; }
        public bool Enabled { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerTransaction> CustomerTransactions { get; set; }
    }
}
