using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class apiClient
    {
        public int ClientID { get; set; }
        public short FK_ClientStatusID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string ClientTitle { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string BalanceAvailable { get; set; }
        public string ClientPassword { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public string FullName {get; set; }
        public string ArtistImage { get; set; }
        public void Bind(JukeBox.Data.Client cr)
        {

            ClientID = cr.ClientID;
            FK_ClientStatusID = cr.FK_ClientStatusID;
            FirstName = cr.FirstName;
            LastName = cr.LastName;
            Initials = cr.Initials;
            ClientTitle = cr.ClientTitle;
            DateOfBirth = cr.DateOfBirth;
            BalanceAvailable = (cr.CreditAmount??0).ToString("0.00");
            ClientPassword = cr.ClientPassword;
            CellPhone = cr.CellPhone;
            Email = cr.Email;
            Gender = cr.Gender;
            ArtistImage = cr.ArtistImage;
            FullName = string.Format("{0} {1}", cr.FirstName, cr.LastName);



        }
    }
}