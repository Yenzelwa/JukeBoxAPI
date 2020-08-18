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
    
        public virtual DbSet<ClientContactDetail> ClientContactDetails { get; set; }
        public virtual DbSet<ClientContactType> ClientContactTypes { get; set; }
        public virtual DbSet<ClientStatu> ClientStatus { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CustomerStatu> CustomerStatus { get; set; }
        public virtual DbSet<CustomerTransaction> CustomerTransactions { get; set; }
        public virtual DbSet<CustomerTransactionType> CustomerTransactionTypes { get; set; }
        public virtual DbSet<LibraryStatu> LibraryStatus { get; set; }
        public virtual DbSet<LibraryType> LibraryTypes { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleUserMap> RoleUserMaps { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Library> Libraries { get; set; }
        public virtual DbSet<LibraryDetail> LibraryDetails { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<PromotionCategory> PromotionCategories { get; set; }
        public virtual DbSet<PromotionType> PromotionTypes { get; set; }
        public virtual DbSet<PromotionMap> PromotionMaps { get; set; }
    
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
    
        public virtual ObjectResult<Nullable<short>> sp_NumberOfMembers_Report(Nullable<int> memberTypeId)
        {
            var memberTypeIdParameter = memberTypeId.HasValue ?
                new ObjectParameter("MemberTypeId", memberTypeId) :
                new ObjectParameter("MemberTypeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<short>>("sp_NumberOfMembers_Report", memberTypeIdParameter);
        }
    
        public virtual ObjectResult<sp_SalesPerAlbum_Result> sp_SalesPerAlbum(Nullable<int> libraryType, Nullable<long> clientId)
        {
            var libraryTypeParameter = libraryType.HasValue ?
                new ObjectParameter("LibraryType", libraryType) :
                new ObjectParameter("LibraryType", typeof(int));
    
            var clientIdParameter = clientId.HasValue ?
                new ObjectParameter("ClientId", clientId) :
                new ObjectParameter("ClientId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_SalesPerAlbum_Result>("sp_SalesPerAlbum", libraryTypeParameter, clientIdParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> sp_Sales_Report(Nullable<int> saleTypeId)
        {
            var saleTypeIdParameter = saleTypeId.HasValue ?
                new ObjectParameter("SaleTypeId", saleTypeId) :
                new ObjectParameter("SaleTypeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("sp_Sales_Report", saleTypeIdParameter);
        }
    
        public virtual ObjectResult<InsertVote_Result> InsertVote(Nullable<int> promotionTypeId, Nullable<int> promotionMapId, Nullable<int> clientId, Nullable<int> customerId)
        {
            var promotionTypeIdParameter = promotionTypeId.HasValue ?
                new ObjectParameter("PromotionTypeId", promotionTypeId) :
                new ObjectParameter("PromotionTypeId", typeof(int));
    
            var promotionMapIdParameter = promotionMapId.HasValue ?
                new ObjectParameter("PromotionMapId", promotionMapId) :
                new ObjectParameter("PromotionMapId", typeof(int));
    
            var clientIdParameter = clientId.HasValue ?
                new ObjectParameter("ClientId", clientId) :
                new ObjectParameter("ClientId", typeof(int));
    
            var customerIdParameter = customerId.HasValue ?
                new ObjectParameter("CustomerId", customerId) :
                new ObjectParameter("CustomerId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InsertVote_Result>("InsertVote", promotionTypeIdParameter, promotionMapIdParameter, clientIdParameter, customerIdParameter);
        }
    
        public virtual ObjectResult<GetPromotionResultByType_Result> GetPromotionResultByType(Nullable<int> promotionTypeId, Nullable<int> promotionCategoryId)
        {
            var promotionTypeIdParameter = promotionTypeId.HasValue ?
                new ObjectParameter("PromotionTypeId", promotionTypeId) :
                new ObjectParameter("PromotionTypeId", typeof(int));
    
            var promotionCategoryIdParameter = promotionCategoryId.HasValue ?
                new ObjectParameter("PromotionCategoryId", promotionCategoryId) :
                new ObjectParameter("PromotionCategoryId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPromotionResultByType_Result>("GetPromotionResultByType", promotionTypeIdParameter, promotionCategoryIdParameter);
        }
    
        public virtual ObjectResult<sp__VoucherRedeemProcedure_Result> sp__VoucherRedeemProcedure(Nullable<long> clientId, string voucherPin, Nullable<long> voucherTypeId, Nullable<long> voucherTransactionTypeId, Nullable<short> voucherStatusId, Nullable<System.DateTime> redeemDateTime, Nullable<long> voucherReferenceId, Nullable<decimal> amount, Nullable<bool> isTxComplete, string referenceComment)
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
    
            var referenceCommentParameter = referenceComment != null ?
                new ObjectParameter("ReferenceComment", referenceComment) :
                new ObjectParameter("ReferenceComment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp__VoucherRedeemProcedure_Result>("sp__VoucherRedeemProcedure", clientIdParameter, voucherPinParameter, voucherTypeIdParameter, voucherTransactionTypeIdParameter, voucherStatusIdParameter, redeemDateTimeParameter, voucherReferenceIdParameter, amountParameter, isTxCompleteParameter, referenceCommentParameter);
        }
    
        public virtual ObjectResult<Create_PromotionCategory_Result> Create_PromotionCategory(Nullable<int> promotionCategoryId, Nullable<int> fK_PromotionTypeId, string promotionCategoryName, string categoryImage, Nullable<bool> enabled, Nullable<bool> allArtistSelected)
        {
            var promotionCategoryIdParameter = promotionCategoryId.HasValue ?
                new ObjectParameter("PromotionCategoryId", promotionCategoryId) :
                new ObjectParameter("PromotionCategoryId", typeof(int));
    
            var fK_PromotionTypeIdParameter = fK_PromotionTypeId.HasValue ?
                new ObjectParameter("FK_PromotionTypeId", fK_PromotionTypeId) :
                new ObjectParameter("FK_PromotionTypeId", typeof(int));
    
            var promotionCategoryNameParameter = promotionCategoryName != null ?
                new ObjectParameter("PromotionCategoryName", promotionCategoryName) :
                new ObjectParameter("PromotionCategoryName", typeof(string));
    
            var categoryImageParameter = categoryImage != null ?
                new ObjectParameter("CategoryImage", categoryImage) :
                new ObjectParameter("CategoryImage", typeof(string));
    
            var enabledParameter = enabled.HasValue ?
                new ObjectParameter("Enabled", enabled) :
                new ObjectParameter("Enabled", typeof(bool));
    
            var allArtistSelectedParameter = allArtistSelected.HasValue ?
                new ObjectParameter("AllArtistSelected", allArtistSelected) :
                new ObjectParameter("AllArtistSelected", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Create_PromotionCategory_Result>("Create_PromotionCategory", promotionCategoryIdParameter, fK_PromotionTypeIdParameter, promotionCategoryNameParameter, categoryImageParameter, enabledParameter, allArtistSelectedParameter);
        }
    
        public virtual ObjectResult<GetPromotionCategoryByPromoTypeId_Result> GetPromotionCategoryByPromoTypeId(Nullable<int> promotionTypeId)
        {
            var promotionTypeIdParameter = promotionTypeId.HasValue ?
                new ObjectParameter("PromotionTypeId", promotionTypeId) :
                new ObjectParameter("PromotionTypeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPromotionCategoryByPromoTypeId_Result>("GetPromotionCategoryByPromoTypeId", promotionTypeIdParameter);
        }
    
        public virtual ObjectResult<Create_PromotionType_Result> Create_PromotionType(Nullable<int> promotionTypeId, string promotionTypeName, Nullable<decimal> promotionAmount, string promotionImage, Nullable<System.DateTime> promotionEndDate, Nullable<System.DateTime> promotionStartDate, Nullable<bool> hasCategory, Nullable<bool> enabled, Nullable<bool> allArtistSelected)
        {
            var promotionTypeIdParameter = promotionTypeId.HasValue ?
                new ObjectParameter("PromotionTypeId", promotionTypeId) :
                new ObjectParameter("PromotionTypeId", typeof(int));
    
            var promotionTypeNameParameter = promotionTypeName != null ?
                new ObjectParameter("PromotionTypeName", promotionTypeName) :
                new ObjectParameter("PromotionTypeName", typeof(string));
    
            var promotionAmountParameter = promotionAmount.HasValue ?
                new ObjectParameter("PromotionAmount", promotionAmount) :
                new ObjectParameter("PromotionAmount", typeof(decimal));
    
            var promotionImageParameter = promotionImage != null ?
                new ObjectParameter("PromotionImage", promotionImage) :
                new ObjectParameter("PromotionImage", typeof(string));
    
            var promotionEndDateParameter = promotionEndDate.HasValue ?
                new ObjectParameter("PromotionEndDate", promotionEndDate) :
                new ObjectParameter("PromotionEndDate", typeof(System.DateTime));
    
            var promotionStartDateParameter = promotionStartDate.HasValue ?
                new ObjectParameter("PromotionStartDate", promotionStartDate) :
                new ObjectParameter("PromotionStartDate", typeof(System.DateTime));
    
            var hasCategoryParameter = hasCategory.HasValue ?
                new ObjectParameter("HasCategory", hasCategory) :
                new ObjectParameter("HasCategory", typeof(bool));
    
            var enabledParameter = enabled.HasValue ?
                new ObjectParameter("Enabled", enabled) :
                new ObjectParameter("Enabled", typeof(bool));
    
            var allArtistSelectedParameter = allArtistSelected.HasValue ?
                new ObjectParameter("AllArtistSelected", allArtistSelected) :
                new ObjectParameter("AllArtistSelected", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Create_PromotionType_Result>("Create_PromotionType", promotionTypeIdParameter, promotionTypeNameParameter, promotionAmountParameter, promotionImageParameter, promotionEndDateParameter, promotionStartDateParameter, hasCategoryParameter, enabledParameter, allArtistSelectedParameter);
        }
    
        public virtual ObjectResult<GetAllPromotionType_Result> GetAllPromotionType()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllPromotionType_Result>("GetAllPromotionType");
        }
    
        public virtual ObjectResult<Get_ClientPromotion_Result> Get_ClientPromotion(Nullable<int> promotionCategoryId, Nullable<int> promotionTypeId)
        {
            var promotionCategoryIdParameter = promotionCategoryId.HasValue ?
                new ObjectParameter("PromotionCategoryId", promotionCategoryId) :
                new ObjectParameter("PromotionCategoryId", typeof(int));
    
            var promotionTypeIdParameter = promotionTypeId.HasValue ?
                new ObjectParameter("PromotionTypeId", promotionTypeId) :
                new ObjectParameter("PromotionTypeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_ClientPromotion_Result>("Get_ClientPromotion", promotionCategoryIdParameter, promotionTypeIdParameter);
        }
    
        public virtual ObjectResult<Add_ClientPromotion_Result> Add_ClientPromotion(Nullable<int> promotionCategoryMapId, Nullable<int> fK_PromotionCategoryId, Nullable<int> fK_PromotionTypeId, Nullable<int> fK_ClientID, Nullable<bool> enabled)
        {
            var promotionCategoryMapIdParameter = promotionCategoryMapId.HasValue ?
                new ObjectParameter("PromotionCategoryMapId", promotionCategoryMapId) :
                new ObjectParameter("PromotionCategoryMapId", typeof(int));
    
            var fK_PromotionCategoryIdParameter = fK_PromotionCategoryId.HasValue ?
                new ObjectParameter("FK_PromotionCategoryId", fK_PromotionCategoryId) :
                new ObjectParameter("FK_PromotionCategoryId", typeof(int));
    
            var fK_PromotionTypeIdParameter = fK_PromotionTypeId.HasValue ?
                new ObjectParameter("FK_PromotionTypeId", fK_PromotionTypeId) :
                new ObjectParameter("FK_PromotionTypeId", typeof(int));
    
            var fK_ClientIDParameter = fK_ClientID.HasValue ?
                new ObjectParameter("FK_ClientID", fK_ClientID) :
                new ObjectParameter("FK_ClientID", typeof(int));
    
            var enabledParameter = enabled.HasValue ?
                new ObjectParameter("Enabled", enabled) :
                new ObjectParameter("Enabled", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Add_ClientPromotion_Result>("Add_ClientPromotion", promotionCategoryMapIdParameter, fK_PromotionCategoryIdParameter, fK_PromotionTypeIdParameter, fK_ClientIDParameter, enabledParameter);
        }
    
        public virtual ObjectResult<GetPromoionClientMap_Result> GetPromoionClientMap(Nullable<int> promotionTypeId, Nullable<int> promotionCategoryId)
        {
            var promotionTypeIdParameter = promotionTypeId.HasValue ?
                new ObjectParameter("PromotionTypeId", promotionTypeId) :
                new ObjectParameter("PromotionTypeId", typeof(int));
    
            var promotionCategoryIdParameter = promotionCategoryId.HasValue ?
                new ObjectParameter("PromotionCategoryId", promotionCategoryId) :
                new ObjectParameter("PromotionCategoryId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPromoionClientMap_Result>("GetPromoionClientMap", promotionTypeIdParameter, promotionCategoryIdParameter);
        }
    }
}
