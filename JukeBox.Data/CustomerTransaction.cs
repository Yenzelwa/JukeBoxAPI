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
    
    public partial class CustomerTransaction
    {
        public long CustomerTransactionID { get; set; }
        public long FK_PurchaseD { get; set; }
        public int FK_CustomerID { get; set; }
        public short FK_CustomerTransactionTypeID { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal BalanceAvailable { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string ReferenceComment { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual CustomerTransactionType CustomerTransactionType { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
