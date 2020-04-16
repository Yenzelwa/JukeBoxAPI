
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static JukeBoxApi.Models.ApiLibrary;
using System.Threading.Tasks;
using JukeBoxApi.Models;
using System.Web.Http.Cors;
using System.IO;
using System.Web;
using System.Diagnostics;
using System.Windows.Media;



namespace JukeBoxApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/library")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LibraryController : ApiController
    {
        [AllowAnonymous]
        [Route("library/type")]
        [HttpGet]
        public async Task<ApiLibraryTypeResponse> GetLibraryType()
        {
            var apiResp = new ApiLibraryTypeResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).GetLibraryType();

            if (retVal.Count > 0)
            {
                var model = new ApiLibraryType
                {
                    TypeId = 0,
                    TypeName = "ALL"
                };
                apiResp.ResponseObject = new List<ApiLibraryType>();
                apiResp.ResponseObject.Add(model);
                foreach (var _library in retVal)
                {
                    var libraryType = new ApiLibraryType();
                    libraryType.Bind(_library);
                    apiResp.ResponseObject.Add(libraryType);

                }
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("{filter}")]
        [HttpGet]
        public async Task<ApiLibraryResponse> GetLibrary(int filter, int? clientid = null)
        {
            var apiResp = new ApiLibraryResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).GetLibrary(filter, clientid);

            if (retVal.Count > 0)
            {
                apiResp.ResponseObject = new List<ApiLibrary>();
                foreach (var _library in retVal)
                {
                    var library = new ApiLibrary();
                    library.Bind(_library);
                    apiResp.ResponseObject.Add(library);

                }
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }

        [AllowAnonymous]
        [Route("detail/{id}")]
        [HttpGet]
        public async Task<ApiLibraryDetailResponse> GetLibraryDetail(long id, int? clientid = null)
        {
            var apiResp = new ApiLibraryDetailResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).GetLibraryDetail(id, clientid);

            if (retVal.Count > 0)
            {
                apiResp.ResponseObject = new List<ApiLibraryDetail>();
                foreach (var _library in retVal)
                {
                    var libraryDetail = new ApiLibraryDetail();
                    libraryDetail.Bind(_library);
                    apiResp.ResponseObject.Add(libraryDetail);

                }
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("purchase")]
        [HttpPost]
        public async Task<ApiResponse> PurchaseLibrary([FromBody]PurchaseOrderRequest request)
        {
            var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).PurchaseOrder(request.LibraryId, request.LibraryDetailId, request.ClientId, request.UserId);

            if (retVal != null)
            {

                apiResp.ResponseType = Convert.ToInt16(retVal.Success);
                apiResp.ResponseMessage = retVal.Message;
            }
            return apiResp;
        }


        [AllowAnonymous]
        [Route("library")]
        [HttpPost]
        public async Task<ApiResponse> CreateLibrary([FromBody]LibraryRequest request)
        {
            var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };
            request.CreatedBy = 1;
            request.LibraryCoverFilePath = "http://www.apigagasimedia.co.za/JukeBoxStore/Album/" + request.LibraryCoverFilePath;
            var retVal = await (new JukeBox.BLL.Library()).CreateLibrary(request.LibraryID, request.FK_ClientID, request.FK_LibraryTypeID,
                request.LibraryName, request.LibraryDescription, request.LibraryCoverFilePath, request.Price, request.CreatedBy); ;


            if (retVal.Success.HasValue)
            {
                apiResp.ResponseType = Convert.ToInt16(retVal.Success);
                apiResp.ResponseMessage = retVal.Message;
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("postfile")]
        [HttpPost]
        public async void SaveAsync()
        {
            try
            {
                //var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Length > 0)
                {
                    var httpPostedFile = System.Web.HttpContext.Current.Request.Files["file"];


                    if (System.Web.HttpContext.Current.Request.Headers.AllKeys.Length > 0)
                    {
                        var fileFolderName = System.Web.HttpContext.Current.Request.Headers["file-type"];
                        
                        if (httpPostedFile != null)
                        {
                            var filePath = fileFolderName == null ? Path.Combine(@"C:/inetpub/wwwroot/drex/Documents") :
                                                              Path.Combine(@"C:/inetpub/wwwroot/JukeBoxApi/JukeBoxStore/Album");
                            string savePath = "";
                            if (!System.IO.File.Exists(filePath))
                            {
                                Directory.CreateDirectory(filePath);

                                 savePath = filePath + "/" + httpPostedFile.FileName;
                                
                                httpPostedFile.SaveAs(savePath);
                            }
                            else
                            {
                                 savePath = filePath + "/" + httpPostedFile.FileName;
                                httpPostedFile.SaveAs(savePath);
                            }
                          
                               // apiResp.ResponseType = 1;
                            //    apiResp.ResponseMessage = savePath.Replace("C:", "http://www.apigagasimedia.co.za");
                           
                        }
                    }
                  
                   
                }
              //  return apiResp;
            }
            catch (Exception e)
            {
                var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };
              //  return apiResp;
            }
        }

        [AllowAnonymous]
        [Route("librarydetail")]
        [HttpPost]
        public async Task<ApiResponse> CreateLibraryDetail([FromBody]LibraryDetailRequest request)
        {
            var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };
            request.CreatedBy = 1;
            request.FK_LibraryStatusID = 1;
            request.FilePath =   "http://www.apigagasimedia.co.za/JukeBoxStore/Songs/" + request.FilePath;

            var retVal = await (new JukeBox.BLL.Library()).CreateLibraryDetail(request.LibraryDetailID, request.FK_LibraryID, request.FK_LibraryStatusID,
                request.LibraryDetailName, request.FilePath, request.Price, request.CreatedBy);


            if (retVal.Success.HasValue)
            {

                apiResp.ResponseType = Convert.ToInt16(retVal.Success);
                apiResp.ResponseMessage = retVal.Message;
            }
            return apiResp;
        }

        [AllowAnonymous]
        [Route("album/sales/{type}/{clientid}")]
        [HttpGet]
        public async Task<ApiSalesPerAlbumResponse> GetLibrarySales( int type , long clientid)
        {
            var apiResp = new ApiSalesPerAlbumResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).GetAlbumSales(type , clientid);

            if (retVal.Count > 0)
            {
                apiResp.ResponseObject = new List<ApiSalesPerAlbum>();
                foreach (var _album in retVal)
                {
                    var albumSales = new ApiSalesPerAlbum();
                    albumSales.Bind(_album);
                    apiResp.ResponseObject.Add(albumSales);

                }
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("albums/{clientid}")]
        [HttpGet]
        public async Task<ApiClientLibraryResponse> GetClientAlbums(long clientid)
        {
            var apiResp = new ApiClientLibraryResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).GetLibraryByClientId(clientid);

            if (retVal.Count > 0)
            {
                apiResp.ResponseObject = new List<ApiClientLibrary>();
                foreach (var _album in retVal)
                {
                    var albumlist = new ApiClientLibrary();
                    albumlist.Bind(_album);
                    apiResp.ResponseObject.Add(albumlist);

                }
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }

        [AllowAnonymous]
        [Route("delete/{id}")]
        [HttpGet]
        public async Task<ApiLibraryResponse> DeleteLibrary(long id )
        {
            var apiResp = new ApiLibraryResponse { ResponseType = -1, ResponseMessage = "Failed" };
            var createdBy = 1;
            var deleted = await (new JukeBox.BLL.Library()).DeleteLibrary(id, createdBy);
            if (deleted)
            {
                var retVal = await (new JukeBox.BLL.Library()).GetLibrary(0, 0);

                if (retVal.Count > 0)
                {
                    apiResp.ResponseObject = new List<ApiLibrary>();
                    foreach (var _library in retVal)
                    {
                        var library = new ApiLibrary();
                        library.Bind(_library);
                        apiResp.ResponseObject.Add(library);

                    }
                    apiResp.ResponseType = 1;
                    apiResp.ResponseMessage = "Success";
                }
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("detail/delete/{id}")]
        [HttpGet]
        public async Task<ApiLibraryDetailResponse> DeleteLibraryDetail(long id)
        {
            var apiResp = new ApiLibraryDetailResponse { ResponseType = -1, ResponseMessage = "Failed" };
            var createdBy = 1;
            var libaryId = await (new JukeBox.BLL.Library()).DeleteLibraryDetail(id, createdBy);
            if(libaryId > 1) {
                var retVal = await (new JukeBox.BLL.Library()).GetLibraryDetail(libaryId, 0);

                if (retVal.Count > 0)
                {
                    apiResp.ResponseObject = new List<ApiLibraryDetail>();
                    foreach (var _library in retVal)
                    {
                        var libraryDetail = new ApiLibraryDetail();
                        libraryDetail.Bind(_library);
                        apiResp.ResponseObject.Add(libraryDetail);

                    }
                    apiResp.ResponseType = 1;
                    apiResp.ResponseMessage = "Success";
                }
            }
           
            return apiResp;
        }
    }
}

