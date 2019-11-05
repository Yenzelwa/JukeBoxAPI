using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JukeBoxApi.Models
{
    public class User
    {
   
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }



        public string Email { get; set; }
        public decimal BalanceAvailable { get; set; }


        public string Telephone { get; set; }

    
        public string ImagePath { get; set; }

        public int UserTypeId { get; set; }         

  
        public byte[] ImageArray { get; set; }

        public string Password { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return "no_image";
                }

                return ImagePath;
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
        public void Bind(JukeBox.Data.Customer cr)
        {

            UserId = cr.CustomerID;
            FirstName = cr.FirstName;
            LastName = cr.LastName;
            BalanceAvailable = cr.BalanceAvailable;
            Telephone = cr.CellPhone;
            Email = cr.Email;
            ImagePath = cr.ImageFilePath;
       
            

        }

    }
}