
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static JukeBoxApi.Models.ApiLibrary;
using System.Threading.Tasks;
using JukeBoxApi.Models;

namespace JukeBoxApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/library")]
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
                apiResp.ResponseObject = new List<ApiLibraryType>();
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
        public async Task<ApiLibraryResponse> GetLibrary(int filter )
        {
            var apiResp = new ApiLibraryResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).GetLibrary(filter);

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
        public async Task<ApiLibraryDetailResponse> GetLibraryDetail(long id , int? clientid=null)
        {
            var apiResp = new ApiLibraryDetailResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).GetLibraryDetail(id , clientid);

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
        public async Task<ApiResponse> PurchaseLibrary([FromBody]PurchaseOrderRequest  request)
        {
            var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).PurchaseOrder(request.LibraryId, request.LibraryDetailId , request.ClientId ,request.UserId);

            if (retVal!= null)
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

            var retVal = await (new JukeBox.BLL.Library()).CreateLibrary(request.LibraryID, request.FK_ClientID , request.FK_LibraryTypeID,
                request.LibraryName , request.LibraryDescription ,request.LibraryCoverFilePath, request.Price,request.CreatedBy);


            if (retVal.Success.HasValue)
            {
                apiResp.ResponseType = Convert.ToInt16(retVal.Success);
                apiResp.ResponseMessage = retVal.Message;
            }
            return apiResp;
        }

        [AllowAnonymous]
        [Route("librarydetail")]
        [HttpPost]
        public async Task<ApiResponse> CreateLibraryDetail([FromBody]LibraryDetailRequest request)
        {
            var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).CreateLibraryDetail(request.LibraryDetailID, request.FK_LibraryID, request.FK_LibraryStatusID,
                request.LibraryDetailName,  request.FilePath, request.Price, request.CreatedBy);


            if (retVal.Success.HasValue)
            {

                apiResp.ResponseType = Convert.ToInt16(retVal.Success);
                apiResp.ResponseMessage = retVal.Message;
            }
            return apiResp;
        }
    }
    }

