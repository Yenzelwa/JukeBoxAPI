using JukeBox.BLL;
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
        private readonly Promotion _promotion;
      
        public PromotionController()
        {
            _promotion = new Promotion();
        }
        [AllowAnonymous]
        [Route("type")]
        [HttpGet]
        public async Task<ApiPromotionTypeResponse> GetPromotionType(int? platform=null)
        {
            var apiResp = new ApiPromotionTypeResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await _promotion.GetAllPromotion(platform);

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
        [Route("category/{promoTypeId}")]
        [HttpGet]
        public async Task<ApiPromotionCategoryResponse> GetPromotionCategory(int promoTypeId)
        {
            var apiResp = new ApiPromotionCategoryResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await _promotion.GetPromotionCategory(promoTypeId);

            if (retVal.Count > 0)
            {
                apiResp.ResponseObject = new List<PromotionCategory>();
                foreach (var _promo in retVal)
                {
                    var promotion = new PromotionCategory();
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
        public async Task<ApiPromotionResultResponse> GetPromotionResult([FromUri]int promotionTypeId,int? promotionCategoryId = null)
        {
            var apiResp = new ApiPromotionResultResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await _promotion.GetPromotionTypeResult(promotionTypeId, promotionCategoryId);

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
        [Route("client/promotion/{promotionCategoryId}/{promoTypeId}")]
        [HttpGet]
        public async Task<ApiClientPromotionResponse> GetClientPromotion(int promotionCategoryId , int? promoTypeId)
        {
            var apiResp = new ApiClientPromotionResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await _promotion.GetClientPromotion(promoTypeId ,promotionCategoryId);

            if (retVal.Count > 0)
            {
                apiResp.ResponseObject = new List<ClientPromotion>();

                foreach (var _promo in retVal)
                {
                    var promotion = new ClientPromotion();
                    promotion.Bind(_promo);
                    apiResp.ResponseObject.Add(promotion);
                   

                }
                
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("client/promotionmap/{promotionTypeId}/{promotionCategoryId}")]
        [HttpGet]
        public async Task<ApiPromotionClientMapResponse> GetClientPromotion(int? promotionTypeId ,int? promotionCategoryId)
        {
            var apiResp = new ApiPromotionClientMapResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await _promotion.GetPromotionClientMap(promotionTypeId, promotionCategoryId);

            if (retVal.Count > 0)
            {
                apiResp.ResponseObject = new List<PromotionClientMap>();

                foreach (var _promo in retVal)
                {
                    var promotion = new PromotionClientMap();
                    promotion.Bind(_promo);
                    apiResp.ResponseObject.Add(promotion);


                }

                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("vote")]
        [HttpPost]
        public async Task<ApiResponse> VotePromotion([FromBody]PromotionResultRequest request)
        {
            var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = await _promotion.Vote(request.PromotionTypeId, request.PromotionMapId, request.ClientId, request.Customer);

            if (retVal.Success.HasValue)
            {

                apiResp.ResponseType = Convert.ToInt16(retVal.Success);
                apiResp.ResponseMessage = retVal.Message;
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("type")]
        [HttpPost]
        public async Task<ApiResponse> CreatePromotionType([FromBody]PromotionTypeRequest request)
        {
            var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };
            request.Enabled = true;
            request.PromotionImage = "http://www.apigagasimedia.co.za/JukeBoxStore/PromoType/" + request.PromotionImage;
            var retVal = await _promotion.CreatePromotionType(request.PromotionTypeId,request.PromotionTypeName,request.Amount,
                                                              request.PromotionImage, request.PromotionStartDate, request.PromotionEndDate,request.HasCategory,request.Enabled, request.AllArtist);
            if (retVal.Success.HasValue)
            {
                apiResp.ResponseType = Convert.ToInt16(retVal.Success);
                apiResp.ResponseMessage = retVal.Message;
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("category")]
        [HttpPost]
        public async Task<ApiResponse> CreatePromotionCategory([FromBody]PromotionCategoryRequest request)
        {
            var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };
            request.CategoryImage = "http://www.apigagasimedia.co.za/JukeBoxStore/PromoCategory/" + request.CategoryImage;
            request.Enabled = true;
            var retVal = await _promotion.CreatePromotionCategory(request.PromotionCategoryId, request.PromotionTypeId, request.CategoryName,
                                                              request.CategoryImage, request.Enabled , request.AllArtist);
            if (retVal.Success.HasValue)
            {
                apiResp.ResponseType = Convert.ToInt16(retVal.Success);
                apiResp.ResponseMessage = retVal.Message;
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("client/promotion")]
        [HttpPost]
        public async Task<ApiResponse> AddClientPromotion([FromBody]ClientPromotionRequest request)
        {
            var apiResp = new ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };
            var retVal = await _promotion.AddClientPromotion(request.PromotionMapId, request.PromotionCategoryId, request.PromotionTypeId,  request.ClientId, request.Enabled);
            if (retVal.Success.HasValue)
            {
                apiResp.ResponseType = Convert.ToInt16(retVal.Success);
                apiResp.ResponseMessage = retVal.Message;
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("type/delete/{id}")]
        [HttpGet]
        public async Task<ApiPromotionTypeResponse> DeletePromotionType(int id)
        {
            var apiResp = new ApiPromotionTypeResponse { ResponseType = -1, ResponseMessage = "Failed" };
            var promotionTypeId = await _promotion.DeletePromtionType(id);
            if (promotionTypeId)
            {
                var retVal = await _promotion.GetAllPromotion(1);

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
            }

            return apiResp;
        }
        [AllowAnonymous]
        [Route("category/delete/{id}/{typeId}")]
        [HttpGet]
        public async Task<ApiPromotionCategoryResponse> DeletePromotionCategory(int id , int typeId)
        {
            var apiResp = new ApiPromotionCategoryResponse { ResponseType = -1, ResponseMessage = "Failed" };
            var promotionTypeId = await _promotion.DeletePromotionCategory(id);
            if (promotionTypeId)
            {
                var retVal = await _promotion.GetPromotionCategory(typeId);

                if (retVal.Count > 0)
                {
                    apiResp.ResponseObject = new List<PromotionCategory>();
                    foreach (var _promo in retVal)
                    {
                        var promotion = new PromotionCategory();
                        promotion.Bind(_promo);
                        apiResp.ResponseObject.Add(promotion);

                    }
                    apiResp.ResponseType = 1;
                    apiResp.ResponseMessage = "Success";
                }
            }

            return apiResp;
        }
        [AllowAnonymous]
        [Route("client/promotion/delete/{id}/{typeId}/{categoryId}")]
        [HttpGet]
        public async Task<ApiClientPromotionResponse> DeleteClientPromotion(int id, int? typeId ,int?  categoryId)
        {
            var apiResp = new ApiClientPromotionResponse { ResponseType = -1, ResponseMessage = "Failed" };
            var promotionTypeId = await _promotion.DeleteClientPromotion(id);
            if (promotionTypeId)
            {
                var retVal = await _promotion.GetClientPromotion(typeId,categoryId);

                if (retVal.Count > 0)
                {
                    apiResp.ResponseObject = new List<ClientPromotion>();

                    foreach (var _promo in retVal)
                    {
                        var promotion = new ClientPromotion();
                        promotion.Bind(_promo);
                        apiResp.ResponseObject.Add(promotion);


                    }
                    apiResp.ResponseType = 1;
                    apiResp.ResponseMessage = "Success";
                }
            }

            return apiResp;
        }
    }
}
