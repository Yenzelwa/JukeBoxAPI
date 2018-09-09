using JukeBoxApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;
using static JukeBoxApi.Models.ApiLibrary;

namespace JukeBoxApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/library")]
    public class LibraryController : ApiController
    {
        [AllowAnonymous]
        [Route("")]
        [HttpGet]
        public ApiLibraryResponse GetLibrary()
        {
            var apiResp = new ApiLibraryResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = (new JukeBox.BLL.Library()).GetLibrary();

            if (retVal.Count > 0)
            {
                apiResp.ResponseObject = new List<ApiLibrary>();
                foreach (var _library in retVal)
                {
                    var library = new ApiLibrary();
                        library.Bind(_library);
                    apiResp.ResponseObject.Add(library);

                    library.Client = new ApiClient();
                    library.Client.Bind(_library.Client);

                    library.Detail = new List<ApiLibraryDetail>();
                    foreach (var detail in _library.LibraryDetails)
                    {
                        var libraryDetail = new ApiLibraryDetail();
                        libraryDetail.Bind(detail);
                        library.Detail.Add(libraryDetail);
                    }

                }
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }

    }
}
