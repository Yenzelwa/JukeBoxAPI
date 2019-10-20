using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class ApiLibrary
    {
        public long Id { get; set; }
        public short TypeId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverFilePath { get; set; }
        public string FilePath { get; set; }
        public decimal? Price { get; set; }
        public string Type { get; set; }
        public string Artist { get; set; }

        public bool? AlbumDownload { get; set; }
        public DateTime DateCreated { get; set; }

        public void Bind(JukeBox.Data.GetLibrary_Result cr)
        {
            Id = cr.LibraryID;
            TypeId = cr.FK_LibraryTypeID;
            ClientId = cr.FK_ClientID;
            Name = cr.LibraryName;
            Description = cr.LibraryDescription;
            CoverFilePath = cr.LibraryCoverFilePath;
            FilePath = cr.LibraryFilePath;
            Price = cr.Price;
            Type = cr.LibraryTypeName;
            Artist = cr.ClientName;
            AlbumDownload = cr.AlbumDownload;
            DateCreated = cr.DateCreated;

        }
       
    }
}