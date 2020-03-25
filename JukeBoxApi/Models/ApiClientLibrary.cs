using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class ApiClientLibrary
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Bind(JukeBox.Data.Library cr)
        {
            Id = cr.LibraryID;
            Name = cr.LibraryName;

        }
    }
}