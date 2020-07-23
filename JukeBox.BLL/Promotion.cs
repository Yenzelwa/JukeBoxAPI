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
        public async Task<List<JukeBox.Data.GetPromotionResultByType_Result>> GetPromotionTypeResult(int? promotionTypeId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.GetPromotionResultByType(promotionTypeId).ToList();
            }
        }
        public async Task<JukeBox.Data.InsertVote_Result> Vote(int? promotionTypeId , int clientId, int customerId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.InsertVote(promotionTypeId,clientId,customerId).FirstOrDefault();
            }
        }
    }
}
