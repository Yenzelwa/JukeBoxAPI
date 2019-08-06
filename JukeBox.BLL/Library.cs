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
        public async Task<List<JukeBox.Data.GetLibrary_Result>> GetLibrary()
        {
            using (var db = new JukeBoxEntities())
            {

                return  db.GetLibrary().ToList();
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
    }
}
