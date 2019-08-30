using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class ApiLibraryType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public void Bind(JukeBox.Data.LibraryType ct)
        {
            TypeId = ct.LibraryTypeID;
            TypeName = ct.LibraryTypeName;
          
        }
    }
}