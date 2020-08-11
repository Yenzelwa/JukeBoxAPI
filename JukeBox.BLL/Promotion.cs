using JukeBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL
{
  public  class Promotion
    {
        public async Task<List<JukeBox.Data.GetAllPromotionType_Result>> GetAllPromotion()
        {
            using (var db = new JukeBoxEntities())
            {

                return  db.GetAllPromotionType().ToList();
            }
        }
        public async Task<List<JukeBox.Data.GetPromotionCategoryByPromoTypeId_Result>> GetPromotionCategory(int? PromoTypeId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.GetPromotionCategoryByPromoTypeId(PromoTypeId).ToList();
            }
        }
        public async Task<List<JukeBox.Data.Get_ClientPromotion_Result>> GetClientPromotion(int promoCategoryId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.Get_ClientPromotion(promoCategoryId).ToList();
            }
        }
        public async Task<List<JukeBox.Data.GetPromotionResultByType_Result>> GetPromotionTypeResult(int? promotionTypeId,int? promotionCategoryId )
        {
            using (var db = new JukeBoxEntities())
            {

                return db.GetPromotionResultByType(promotionTypeId,promotionCategoryId).ToList();
            }
        }
        public async Task<JukeBox.Data.InsertVote_Result> Vote(int? promotionTypeId , int? promoMapId, int clientId, int customerId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.InsertVote(promotionTypeId, promoMapId, clientId,customerId).FirstOrDefault();
            }
        }
        public async Task<Create_PromotionType_Result> CreatePromotionType(int? promotionTypeId, string promotionTypeName,
                              decimal? amount, string promotionImage, DateTime? promotionDateEnd, bool? hasCategory, bool? enabled)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.Create_PromotionType(promotionTypeId, promotionTypeName, amount, promotionImage, promotionDateEnd, hasCategory, enabled).FirstOrDefault();
            }
        }
        public async Task<Create_PromotionCategory_Result> CreatePromotionCategory(int? promotionCategoryId, int? promotionTypeId ,string promotionCategoryName,
                                                                                       string promotionImage, bool? enabled)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.Create_PromotionCategory(promotionCategoryId, promotionTypeId, promotionCategoryName, promotionImage,  enabled).FirstOrDefault();
            }
        }
        public async Task<Add_ClientPromotion_Result> AddClientPromotion(int? promotionCategoryMapId, int? promotionCategoryId, int? clientId, bool? enabled)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.Add_ClientPromotion(promotionCategoryMapId, promotionCategoryId, clientId, enabled).FirstOrDefault();
            }
        }
        public async Task<bool> DeletePromtionType(int promotionTypeId)
        {
            using (var db = new JukeBoxEntities())
            {
                try
                {
                    var library = db.PromotionTypes.Where(x => x.PromotionTypeId == promotionTypeId).FirstOrDefault();
                    library.Enabled = false;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

            }
        }
        public async Task<bool> DeletePromotionCategory(int promotionCategoryId )
        {
            using (var db = new JukeBoxEntities())
            {
                try
                {
                    var library = db.PromotionCategories.Where(x => x.PromotionCategoryId == promotionCategoryId).FirstOrDefault();
                    library.Enabled = false;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

            }
        }
        public async Task<bool> DeleteClientPromotion(int promotionCategoryMapId)
        {
            using (var db = new JukeBoxEntities())
            {
                try
                {
                    var library = db.PromotionCategoryMaps.Where(x => x.PromotionCategoryMapId == promotionCategoryMapId).FirstOrDefault();
                    library.Enabled = false;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

            }
        }
    }
}
