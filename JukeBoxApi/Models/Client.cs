using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class Client
    {
     public int ClientID { get; set; }

    public short FK_ClientStatusID { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string Initials { get; set; }


        public string ClientTitle { get; set; }


        public DateTime DateOfBirth { get; set; }
        public short? FK_IdentityTypeID { get; set; }


        public string IdentityTypeValue { get; set; }
        public decimal BalanceAvailable { get; set; }

        public short? FK_CompanyID { get; set; }

        public string ClientPassword { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public short? FK_CountryID { get; set; }
        public string Gender { get; set; }
        public string ArtistImage { get; set; }
        public DateTime? DateCreated { get; set; }

        public int? CreatedBy { get; set; }
    }
}