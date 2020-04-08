using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class LibraryRequest
    {
        public long LibraryID { get; set; }
        public short   FK_LibraryTypeID { get; set; }
        public short FK_ClientID { get; set; }
        public string LibraryName { get; set; }
        public string LibraryCoverFilePath { get; set; }
        public string LibraryDescription { get; set; }
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
    }
}