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
    [RoutePrefix("api/promotion")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PromotionController : ApiController
    {
        [AllowAnonymous]
        [Route("type")]
        [HttpGet]
        public async Task<ApiPromotionTypeResponse> GetPromotionType()
        {
            var apiResp = new ApiPromotionTypeResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Promotion()).GetAllPromotion();

            if (retVal.Count > 0)
            {
                apiResp.ResponseObject = new List<PromotionType>();
                foreach (var _promo in retVal)
                {
                    var promotion = new PromotionType();
                    promotion.Bind(_promo);
                    apiResp.ResponseObject.Add(promotion);

                }
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("result")]
        [HttpGet]
        public async Task<ApiPromotionResultResponse> GetPromotionResult(int promotionTypeId)
        {
            var apiResp = new ApiPromotionResultResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Promotion()).GetPromotionTypeResult(promotionTypeId);

            if (retVal.Count > 0)
            {
                apiResp.ResponseObject = new List<PromotionResult>();
                int count = 1;
                foreach (var _promo in retVal)
                {
                    var promotion = new PromotionResult();
                    promotion.Bind(_promo);
                    promotion.PromoNumber = count;
                    apiResp.ResponseObject.Add(promotion);
                    count++;

                }
                apiResp.ResponseObject.OrderByDescending(x => x.NumberOfVotes);
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("vote")]
        [HttpPost]
        public async Task<ApiResponse> CreateLibraryDetail([FromBody]PromotionResultRequest request)
        {
            var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await (new JukeBox.BLL.Promotion()).Vote(request.PromotionTypeId, request.ClientId, request.Customer);

            if (retVal.Success.HasValue)
            {

                apiResp.ResponseType = Convert.ToInt16(retVal.Success);
                apiResp.ResponseMessage = retVal.Message;
            }
            return apiResp;
        }
    }
}
