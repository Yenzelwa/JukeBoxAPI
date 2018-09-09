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
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.CustomerTransactions = new HashSet<CustomerTransaction>();
        }
    
        public int CustomerID { get; set; }
        public short FK_CustomerStatusID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal BalanceAvailable { get; set; }
        public string ClientPassword { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual CustomerStatu CustomerStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerTransaction> CustomerTransactions { get; set; }
    }
}
