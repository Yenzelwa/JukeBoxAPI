using JukeBoxApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;
using static JukeBoxApi.Models.ApiLibrary;
using System.Threading.Tasks;

namespace JukeBoxApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/library")]
    public class LibraryController : ApiController
    {
        [AllowAnonymous]
        [Route("")]
        [HttpGet]
        public async Task<ApiLibraryResponse> GetLibrary()
        {
            var apiResp = new ApiLibraryResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).GetLibrary();

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
        public async Task<ApiLibraryDetailResponse> GetLibraryDetail(long id)
        {
            var apiResp = new ApiLibraryDetailResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Library()).GetLibraryDetail(id);

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
    }
}
