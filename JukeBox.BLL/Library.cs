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
        public async Task<List<JukeBox.Data.GetLibraryDetail_Result>> GetLibraryDetail(long libraryId, int? clientId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.GetLibraryDetail(libraryId, clientId).ToList();
            }
        }
        public async Task<sp__Purchase_Result> PurchaseOrder(long libraryId , long libraryDetailId , int clientId, int userId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.sp__Purchase(libraryId,libraryDetailId,clientId,userId).FirstOrDefault();
            }
        }
        public async Task<Create_Library_Result> CreateLibrary(long libraryId, int clientId, 
            short libraryType, string libraryName, string libraryDes ,string librayFilePath, decimal price, int userId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.Create_Library(libraryId, clientId,libraryType,libraryName,librayFilePath,libraryDes,price,userId).FirstOrDefault();
            }
        }
        public async Task<Create_Library_Detail_Result> CreateLibraryDetail(long libraryDetailId, int libraryId, 
            short librarySatus, string libraryDetailName, string librayFilePath, decimal price, int userId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.Create_Library_Detail(libraryDetailId, libraryId, librarySatus, libraryDetailName, librayFilePath, price, userId).FirstOrDefault();
            }
        }

    }
}
