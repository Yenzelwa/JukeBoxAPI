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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
        public virtual DbSet<Library> Libraries { get; set; }
    
        public virtual int sp__VoucherRedeemProcedure(Nullable<long> clientId, string voucherPin, Nullable<long> voucherTypeId, Nullable<long> voucherTransactionTypeId, Nullable<short> voucherStatusId, Nullable<System.DateTime> redeemDateTime, Nullable<long> voucherReferenceId, Nullable<decimal> amount, Nullable<bool> isTxComplete)
        {
            var clientIdParameter = clientId.HasValue ?
                new ObjectParameter("clientId", clientId) :
                new ObjectParameter("clientId", typeof(long));
    
            var voucherPinParameter = voucherPin != null ?
                new ObjectParameter("voucherPin", voucherPin) :
                new ObjectParameter("voucherPin", typeof(string));
    
            var voucherTypeIdParameter = voucherTypeId.HasValue ?
                new ObjectParameter("voucherTypeId", voucherTypeId) :
                new ObjectParameter("voucherTypeId", typeof(long));
    
            var voucherTransactionTypeIdParameter = voucherTransactionTypeId.HasValue ?
                new ObjectParameter("voucherTransactionTypeId", voucherTransactionTypeId) :
                new ObjectParameter("voucherTransactionTypeId", typeof(long));
    
            var voucherStatusIdParameter = voucherStatusId.HasValue ?
                new ObjectParameter("voucherStatusId", voucherStatusId) :
                new ObjectParameter("voucherStatusId", typeof(short));
    
            var redeemDateTimeParameter = redeemDateTime.HasValue ?
                new ObjectParameter("redeemDateTime", redeemDateTime) :
                new ObjectParameter("redeemDateTime", typeof(System.DateTime));
    
            var voucherReferenceIdParameter = voucherReferenceId.HasValue ?
                new ObjectParameter("voucherReferenceId", voucherReferenceId) :
                new ObjectParameter("voucherReferenceId", typeof(long));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("amount", amount) :
                new ObjectParameter("amount", typeof(decimal));
    
            var isTxCompleteParameter = isTxComplete.HasValue ?
                new ObjectParameter("isTxComplete", isTxComplete) :
                new ObjectParameter("isTxComplete", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp__VoucherRedeemProcedure", clientIdParameter, voucherPinParameter, voucherTypeIdParameter, voucherTransactionTypeIdParameter, voucherStatusIdParameter, redeemDateTimeParameter, voucherReferenceIdParameter, amountParameter, isTxCompleteParameter);
        }
    
        public virtual ObjectResult<sp__Purchase_Result> sp__Purchase(Nullable<long> libraryId, Nullable<long> libraryDetailId, Nullable<int> clientId, Nullable<int> userId)
        {
            var libraryIdParameter = libraryId.HasValue ?
                new ObjectParameter("libraryId", libraryId) :
                new ObjectParameter("libraryId", typeof(long));
    
            var libraryDetailIdParameter = libraryDetailId.HasValue ?
                new ObjectParameter("libraryDetailId", libraryDetailId) :
                new ObjectParameter("libraryDetailId", typeof(long));
    
            var clientIdParameter = clientId.HasValue ?
                new ObjectParameter("clientId", clientId) :
                new ObjectParameter("clientId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp__Purchase_Result>("sp__Purchase", libraryIdParameter, libraryDetailIdParameter, clientIdParameter, userIdParameter);
        }
    
        public virtual ObjectResult<Create_Library_Result> Create_Library(Nullable<long> libraryId, Nullable<int> fK_ClientId, Nullable<short> libraryTypeId, string libraryName, string libraryCoverFilePath, string libraryDescription, Nullable<decimal> price, Nullable<int> createdBy)
        {
            var libraryIdParameter = libraryId.HasValue ?
                new ObjectParameter("LibraryId", libraryId) :
                new ObjectParameter("LibraryId", typeof(long));
    
            var fK_ClientIdParameter = fK_ClientId.HasValue ?
                new ObjectParameter("FK_ClientId", fK_ClientId) :
                new ObjectParameter("FK_ClientId", typeof(int));
    
            var libraryTypeIdParameter = libraryTypeId.HasValue ?
                new ObjectParameter("LibraryTypeId", libraryTypeId) :
                new ObjectParameter("LibraryTypeId", typeof(short));
    
            var libraryNameParameter = libraryName != null ?
                new ObjectParameter("LibraryName", libraryName) :
                new ObjectParameter("LibraryName", typeof(string));
    
            var libraryCoverFilePathParameter = libraryCoverFilePath != null ?
                new ObjectParameter("LibraryCoverFilePath", libraryCoverFilePath) :
                new ObjectParameter("LibraryCoverFilePath", typeof(string));
    
            var libraryDescriptionParameter = libraryDescription != null ?
                new ObjectParameter("LibraryDescription", libraryDescription) :
                new ObjectParameter("LibraryDescription", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("Price", price) :
                new ObjectParameter("Price", typeof(decimal));
    
            var createdByParameter = createdBy.HasValue ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Create_Library_Result>("Create_Library", libraryIdParameter, fK_ClientIdParameter, libraryTypeIdParameter, libraryNameParameter, libraryCoverFilePathParameter, libraryDescriptionParameter, priceParameter, createdByParameter);
        }
    
        public virtual ObjectResult<Create_Library_Detail_Result> Create_Library_Detail(Nullable<long> libraryDetailId, Nullable<int> fK_LibraryId, Nullable<short> fK_LibraryStatusId, string libraryDetailName, string filePath, Nullable<decimal> price, Nullable<int> createdBy)
        {
            var libraryDetailIdParameter = libraryDetailId.HasValue ?
                new ObjectParameter("LibraryDetailId", libraryDetailId) :
                new ObjectParameter("LibraryDetailId", typeof(long));
    
            var fK_LibraryIdParameter = fK_LibraryId.HasValue ?
                new ObjectParameter("FK_LibraryId", fK_LibraryId) :
                new ObjectParameter("FK_LibraryId", typeof(int));
    
            var fK_LibraryStatusIdParameter = fK_LibraryStatusId.HasValue ?
                new ObjectParameter("FK_LibraryStatusId", fK_LibraryStatusId) :
                new ObjectParameter("FK_LibraryStatusId", typeof(short));
    
            var libraryDetailNameParameter = libraryDetailName != null ?
                new ObjectParameter("LibraryDetailName", libraryDetailName) :
                new ObjectParameter("LibraryDetailName", typeof(string));
    
            var filePathParameter = filePath != null ?
                new ObjectParameter("FilePath", filePath) :
                new ObjectParameter("FilePath", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("Price", price) :
                new ObjectParameter("Price", typeof(decimal));
    
            var createdByParameter = createdBy.HasValue ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Create_Library_Detail_Result>("Create_Library_Detail", libraryDetailIdParameter, fK_LibraryIdParameter, fK_LibraryStatusIdParameter, libraryDetailNameParameter, filePathParameter, priceParameter, createdByParameter);
        }
    
        public virtual ObjectResult<GetLibraryDetail_Result> GetLibraryDetail(Nullable<long> libraryId, Nullable<int> clientId)
        {
            var libraryIdParameter = libraryId.HasValue ?
                new ObjectParameter("LibraryId", libraryId) :
                new ObjectParameter("LibraryId", typeof(long));
    
            var clientIdParameter = clientId.HasValue ?
                new ObjectParameter("clientId", clientId) :
                new ObjectParameter("clientId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetLibraryDetail_Result>("GetLibraryDetail", libraryIdParameter, clientIdParameter);
        }
    
        public virtual ObjectResult<GetLibrary_Result> GetLibrary(Nullable<int> filterType, Nullable<int> clientId)
        {
            var filterTypeParameter = filterType.HasValue ?
                new ObjectParameter("filterType", filterType) :
                new ObjectParameter("filterType", typeof(int));
    
            var clientIdParameter = clientId.HasValue ?
                new ObjectParameter("clientId", clientId) :
                new ObjectParameter("clientId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetLibrary_Result>("GetLibrary", filterTypeParameter, clientIdParameter);
        }
    }
}
