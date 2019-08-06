﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class ApiLibraryDetail
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public decimal? Price { get; set; }
        public DateTime DateCreated { get; set; }
        public void Bind(JukeBox.Data.GetLibraryDetail_Result cr)
        {
            Id = cr.LibraryDetailID;
            Name = cr.LibraryDetailName;
            FilePath = cr.FilePath;
            Price = cr.Price;
            DateCreated = cr.DateCreated;

        }
    }
}