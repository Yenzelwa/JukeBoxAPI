using JukeBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL
{
    public class Account
    {
        public  User LoginUser(string username , string password)
        {
            using (var db = new JukeBoxEntities())
            {
                return db.Users.Where(x=>x.UserName == username).FirstOrDefault();


            }
        }
        public Client LoginClient(string username, string password)
        {
            using (var db = new JukeBoxEntities())
            {
                return db.Clients.Where(x => x.Email == username  || x.CellPhone == username && x.ClientPassword == password).FirstOrDefault();
            }
        }

        public  string HashAndObfuscate(string unencryptedData)
        {
            if (unencryptedData == null) return null;
            //Declarations

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] originalBytes = Encoding.Default.GetBytes(unencryptedData);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);

            // transpose the first and last bytes to stop md5 hash dictionaries
            byte firstByte = encodedBytes[0];
            encodedBytes[0] = encodedBytes[encodedBytes.GetUpperBound(0)];
            encodedBytes[encodedBytes.GetUpperBound(0)] = firstByte;

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }
    }
}
