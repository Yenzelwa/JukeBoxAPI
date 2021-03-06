﻿using JukeBox.Data;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL
{
  public  class Library
    {

        public static void UploadSFTPFile(string host, string username,
  string password, string sourcefile, string destinationpath, int port)
        {
            using (SftpClient client = new SftpClient(host, port, username, password))
            {
                client.Connect();
                client.ChangeDirectory(destinationpath);
                using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
                {
                    client.BufferSize = 4 * 1024;
                    client.UploadFile(fs, Path.GetFileName(sourcefile));
                }
            }
        }
        public async Task<List<JukeBox.Data.LibraryType>> GetLibraryType()
        {
            using (var db = new JukeBoxEntities())
            {

                return db.LibraryTypes.ToList();
            }
        }
        public async Task<List<JukeBox.Data.GetLibrary_Result>> GetLibrary(int filter , int? clientId)
        {
            using (var db = new JukeBoxEntities())
            {

                return  db.GetLibrary(filter , clientId).ToList();
            }
        }
        public async Task<List<JukeBox.Data.Library>> GetLibraryByClientId( long? clientId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.Libraries.Where(x=>x.FK_ClientID == clientId).ToList();
            }
        }
        public async Task<List<JukeBox.Data.sp_SalesPerAlbum_Result>> GetAlbumSales(int type , long clientid)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.sp_SalesPerAlbum(type, clientid).ToList();
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
        public async Task<bool> DeleteLibrary(long libraryId, int userId)
        {
            using (var db = new JukeBoxEntities())
            {
                try
                {
                    var library = db.Libraries.Where(x => x.LibraryID == libraryId).FirstOrDefault();
                    library.Enabled = false;
                    library.CreatedBy = userId;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

            }
        }
        public async Task<long> DeleteLibraryDetail(long libraryDetailId, int userId)
        {
            using (var db = new JukeBoxEntities())
            {
                try
                {
                    var libraryDetail = db.LibraryDetails.Where(x => x.LibraryDetailID == libraryDetailId).FirstOrDefault();
                    libraryDetail.Enabled = false;
                    libraryDetail.CreatedBy = userId;
                    db.SaveChanges();
                    if (libraryDetail != null)
                    {
                        return libraryDetail.FK_LibraryID;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (Exception)
                {

                    return -1;
                }
               
            }
        }
    }
}
