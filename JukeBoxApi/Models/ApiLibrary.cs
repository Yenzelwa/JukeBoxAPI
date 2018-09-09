using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class ApiLibrary
    {
        public long Id { get; set; }
        public int ClientId { get; set; }
        public short TypeId { get; set; }
        public string Name { get; set; }
        public string CoverFilePath { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public List<ApiLibraryDetail> Detail { get; set; }
        public ApiClient Client { get; set; }
        public void Bind(JukeBox.Data.Library cr)
        {
            Id = cr.LibraryID;
            ClientId = cr.FK_ClientID;
            TypeId = cr.FK_LibraryTypeID;
            Name = cr.LibraryName;
            CoverFilePath = cr.LibraryCoverFilePath;
            Price = cr.Price;

        }
        public class ApiLibraryDetail
        {
            public long Id { get; set; }
            public long LibraryId { get; set; }
            public short StatusId { get; set; }
            public string Name { get; set; }
            public string FilePath { get; set; }
            public decimal? Price { get; set; }
            public void Bind(JukeBox.Data.LibraryDetail cr)
            {
                Id = cr.LibraryDetailID;
                LibraryId = cr.FK_LibraryID;
                StatusId = cr.FK_LibraryStatusID;
                Name = cr.LibraryDetailName;
                FilePath = cr.FilePath;
                Price = cr.Price;

            }
        }
        public class ApiClient
        {
            public string ClientName { get; set; }
            public string ClientLastName { get; set; }
            public void Bind(JukeBox.Data.Client cr)
            {
                ClientName = cr.FirstName;
                ClientLastName = cr.LastName;
            }
        }
    }
}