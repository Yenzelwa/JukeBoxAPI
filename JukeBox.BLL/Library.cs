using JukeBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL
{
  public  class Library
    {
        public async Task<List<JukeBox.Data.GetLibrary_Result>> GetLibrary()
        {
            using (var db = new JukeBoxEntities())
            {
                return  db.GetLibrary().ToList();
            }
        }
    }
}
