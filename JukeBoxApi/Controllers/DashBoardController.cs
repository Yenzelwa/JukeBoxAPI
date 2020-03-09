using JukeBox.BLL.Response;
using JukeBoxApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JukeBoxApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/dashboard")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DashBoardController : ApiController
    {
        [AllowAnonymous]
        [Route("sales/{id}")]
        [HttpGet]
        public async Task<ApiDashBoardReportResponse> GetNumbeeOfSales(int? id)
        {
            var apiResp = new ApiDashBoardReportResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Dashboard()).getSalesReport(id);

            if (retVal.HasValue)
            {
                apiResp.ResponseObject = new DashBoardReport
                {
                    Amount = retVal.Value,
                    Number = 0
                };
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("members/{id}")]
        [HttpGet]
        public async Task<ApiDashBoardReportResponse> GetNumbeeOfMembers(int? id)
        {
            var apiResp = new ApiDashBoardReportResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Dashboard()).getMembersReport(id);

            if (retVal.HasValue)
            {
                apiResp.ResponseObject = new DashBoardReport
                {
                    Amount = 0,
                    Number = retVal.Value
                };
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
    }
}
