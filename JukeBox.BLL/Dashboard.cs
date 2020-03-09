using JukeBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL
{
   public class Dashboard
    {
        public async Task<short?> getMembersReport(int? TypeId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.sp_NumberOfMembers_Report(TypeId).FirstOrDefault();
            }
        }

        public async Task<decimal?> getSalesReport(int? TypeId)
        {
            using (var db = new JukeBoxEntities())
            {

                return db.sp_Sales_Report(TypeId).FirstOrDefault();
            }
        }
    }
}
