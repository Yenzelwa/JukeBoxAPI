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
        public List<JukeBox.Data.Library> GetLibrary()
        {
            using (var db = new JukeBoxEntities())
            {
                return db.Libraries.Include("LibraryDetail").ToList();
            }
        }
    }
}
