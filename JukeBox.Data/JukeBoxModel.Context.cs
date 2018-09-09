﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JukeBoxEntities : DbContext
    {
        public JukeBoxEntities()
            : base("name=JukeBoxEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientContactDetail> ClientContactDetails { get; set; }
        public virtual DbSet<ClientContactType> ClientContactTypes { get; set; }
        public virtual DbSet<ClientStatu> ClientStatus { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerStatu> CustomerStatus { get; set; }
        public virtual DbSet<CustomerTransaction> CustomerTransactions { get; set; }
        public virtual DbSet<CustomerTransactionType> CustomerTransactionTypes { get; set; }
        public virtual DbSet<Library> Libraries { get; set; }
        public virtual DbSet<LibraryDetail> LibraryDetails { get; set; }
        public virtual DbSet<LibraryStatu> LibraryStatus { get; set; }
        public virtual DbSet<LibraryType> LibraryTypes { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleUserMap> RoleUserMaps { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
