using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class LibraryDetailRequest
    {
        public long LibraryDetailID { get; set; }
        public short  FK_LibraryID { get; set; }
	  public short FK_LibraryStatusID { get; set; }
	  public string	LibraryDetailName { get; set; }
      public string	 FilePath { get; set; }
      public decimal   Price { get; set; }
      public int	 CreatedBy { get; set; }
    }
}