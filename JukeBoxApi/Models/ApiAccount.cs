using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JukeBox.Data;

namespace JukeBoxApi.Models
{
    public class ApiAccount
    {
        public class ApiClient
        {
            public int ClientID { get; set; }
            public short ClientStatusID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Initials { get; set; }
            public string  Title { get; set; }
            public decimal BalanceAvailable { get; set; }
            public string CellPhone { get; set; }
            public string Email { get; set; }
            public void Bind(JukeBox.Data.Client cr)
            {

                ClientID = cr.ClientID;
                ClientStatusID = cr.FK_ClientStatusID;
                FirstName = cr.FirstName;
                LastName = cr.LastName;
                Initials = cr.Initials;
                Title = cr.ClientTitle;
                BalanceAvailable = cr.BalanceAvailable;
                CellPhone = cr.CellPhone;
                Email = cr.Email;

            }


        }
        public class ApiUser
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool Enabled { get; set; }
            public string Email { get; set; }
            public string CellPhone { get; set; }
            public Nullable<int> CompanyID { get; set; }
            public void Bind(JukeBox.Data.User cr)
            {

                UserID = cr.UserID;
                UserName = cr.UserName;
                FirstName = cr.FirstName;
                LastName = cr.LastName;
                Enabled = cr.Enabled;
                Email = cr.EmailAddress;
                CellPhone = cr.CellPhone;
                CompanyID = cr.FK_CompanyID;

            }


        }
    }
}