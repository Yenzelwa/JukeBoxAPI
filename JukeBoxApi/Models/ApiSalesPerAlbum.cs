using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class ApiSalesPerAlbum
    {
        public long LibraryId { get; set; }
        public short StatusId { get; set; }
        public string AlbumName { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? Price { get; set; }
        public DateTime DateCreated { get; set; }
        public int? NoOfAlbumSold { get; set; }
        public void Bind(JukeBox.Data.sp_SalesPerAlbum_Result cr)
        {
            LibraryId = cr.LibraryID;
            AlbumName = cr.LibraryName;
            CreditAmount = cr.CreditAmount;
            Price = cr.Amount;
            NoOfAlbumSold = cr.NoOfAlbumSold;
            DateCreated = cr.DateCreated;

        }
    }
}