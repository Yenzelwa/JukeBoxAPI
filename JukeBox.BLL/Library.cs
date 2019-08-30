using JukeBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL
{
  public  class Library
    {
        public async Task<List<JukeBox.Data.LibraryType>> GetLibraryType()
        {
            using (var db = new JukeBoxEntities())
            {

                return db.LibraryTypes.ToList();
            }
        }
        public async Task<List<JukeBox.Data.GetLibrary_Result>> GetLibrary(int filter)
        {
            using (var db = new JukeBoxEntities())
            {

                return  db.GetLibrary(filter).ToList();
            }
        }
        public async Task<List<JukeBox.Data.GetLibraryDetail_Result>> GetLibraryDetail(long libraryId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.GetLibraryDetail(libraryId).ToList();
            }
        }
        public async Task<sp__Purchase_Result> PurchaseOrder(long libraryId , long libraryDetailId , int clientId, int userId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.sp__Purchase(libraryId,libraryDetailId,clientId,userId).FirstOrDefault();
            }
        }
        public async Task<sp__Purchase_Result> GetAlbumUrs(long libraryId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.LibraryDetails.Where(x=>x.FK_LibraryID == libraryId).();
            }
        }
    }
}
