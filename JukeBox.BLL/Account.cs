﻿using JukeBox.BLL.ExternalApi;
using JukeBox.BLL.Request;
using JukeBox.BLL.Response;
using JukeBox.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JukeBox.BLL
{
    public class Account
    {
        private string htMessage = string.Empty, sniMessage = string.Empty;
        public  User LoginUser(string username , string password)
        {
            using (var db = new JukeBoxEntities())
            {
                return db.Users.Where(x=>x.UserName == username).FirstOrDefault();


            }
        }
        public Customer LoginClient(string username, string password)
        {
            using (var db = new JukeBoxEntities())
            {
                return db.Customers.Where(x => x.Email == username  || x.CellPhone == username && x.ClientPassword == password).FirstOrDefault();
            }
        }
        public Customer GetCustomerById(int customerId)
        {
            using (var db = new JukeBoxEntities())
            {
                return db.Customers.Where(x => x.CustomerID ==customerId).FirstOrDefault();
            }
        }
        public List<Client> GetAllClient()
        {
            using (var db = new JukeBoxEntities())
            {
                return db.Clients.ToList();
            }
        }

        public Customer SearchCustomer(string email)
        {
            using (var db = new JukeBoxEntities())
            {
                return db.Customers.Where(x=>x.Email == email).FirstOrDefault();
            }
        }
        public int ResetPasword(string password , string code)
        {
            using (var db = new JukeBoxEntities())
            {
                var clientToUpdate = db.Customers.Where(x => x.ClientPassword == code).FirstOrDefault();
                clientToUpdate.ClientPassword = password;
                db.SaveChanges();
                return 1;
            }
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public bool SendCode(int customerId , string email)
        {
            string code;
            code = RandomPassword();
            var clientCode = "";
            using (var db = new JukeBoxEntities())
            {
                var clientToUpdate = db.Customers.Find(customerId);
                clientToUpdate.ClientPassword = code;  
                db.SaveChanges();
                clientCode = db.Customers.Where(x => x.CustomerID == customerId).FirstOrDefault().ClientPassword;
            }

            StringBuilder sbody = new StringBuilder();
            // here i am sendind a image as logo with the path http://usingaspdotnet.blogspot.com
            sbody.Append("<a href=http://usingaspdotnet.blogspot.com><img src=http://a1.twimg.com/profile_images/1427057726/asp_image.jpg/></a></br>");
            // here i am sending a link to the user's mail address with the three values email,code,uname
            // these three values i am sending  this link with the values using querystring method.
            sbody.Append("<a href=http://usingasp.net/reset_pwd.aspx?email=" + email);
            sbody.Append("&code=" + clientCode);
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("khanyods3@gmail.com", email, "Reset Your Password", sbody.ToString());

            SmtpClient mailclient = new SmtpClient();
            mailclient.Credentials = new NetworkCredential("khanyods3@gmail.com", "NoksD79925", "smtp.gmail.com");
            mailclient.Host = "smtp.gmail.com";
            mailclient.Port = 587;
            mailclient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailclient.EnableSsl = true;
            mailclient.UseDefaultCredentials = true;
            // here am setting the property IsBodyHtml true because i am using html tags inside the mail's code
            mail.IsBodyHtml = true;
          //  mailclient.Send(mail);
            return true;
        }
        public int  SaveCustomer(Customer client)
        {
            using (var db = new JukeBoxEntities())
            {
                try
                {
                    if (client.CustomerID > 0) {

                        var customerToUpdate = db.Customers.Find(client.CustomerID);
                        customerToUpdate.FirstName = client.FirstName;
                        customerToUpdate.LastName = client.LastName;
                        customerToUpdate.CellPhone = client.CellPhone;
                        customerToUpdate.Email = client.Email;
                        db.SaveChanges();
                        return 1;


                    }
                    else
                    {

                        var _customer = new Customer
                        {
                            FirstName = client.FirstName,
                            LastName = client.LastName,
                            ClientPassword = client.ClientPassword,
                            CellPhone = client.CellPhone,
                            FK_CustomerStatusID = 1,
                            DateCreated = DateTime.Now,
                            Email = client.Email,
                            BalanceAvailable = 0

                        };
                        db.Customers.Add(_customer);
                        db.SaveChanges();
                        return 1;
                    }
                }
                catch(Exception e)
                {
                    // TO DO'
                    return -1;
                }
            }
          
        }
        public int SaveClient(Client client)
        {
            using (var db = new JukeBoxEntities())
            {
                try
                {
                    if (client.ClientID > 0)
                    {
                        var clientToUpdate = db.Clients.Find(client.ClientID);
                        clientToUpdate.Initials = client.Initials;
                        clientToUpdate.FirstName = client.FirstName;
                        clientToUpdate.LastName = client.LastName;
                        clientToUpdate.Email = client.Email;
                        clientToUpdate.DateOfBirth = client.DateOfBirth;
                        clientToUpdate.Gender = client.Gender;
                        clientToUpdate.IdentityTypeValue = client.IdentityTypeValue;
                        clientToUpdate.CellPhone = client.CellPhone;
                        clientToUpdate.BalanceAvailable = client.BalanceAvailable;
                        clientToUpdate.ClientPassword = client.ClientPassword;
                        clientToUpdate.ClientTitle = client.ClientTitle;
                        db.SaveChanges();
                        return 1;

                    }
                    else
                    {

                        var _client = new Client
                        {   ClientTitle = client.ClientTitle,
                            FK_ClientStatusID = client.FK_ClientStatusID,
                            FK_CompanyID = client.FK_CompanyID,
                            FK_CountryID = client.FK_CountryID,
                             DateOfBirth = client.DateOfBirth,
                             FK_IdentityTypeID = client.FK_IdentityTypeID,
                             Initials= client.Initials,
                             Gender = client.Gender,
                             IdentityTypeValue = client.IdentityTypeValue,
                              CreatedBy = client.CreatedBy,
                            FirstName = client.FirstName,
                            LastName = client.LastName,
                            ClientPassword = client.ClientPassword,
                            CellPhone = client.CellPhone,
                            DateCreated = DateTime.Now,
                            Email = client.Email,
                            BalanceAvailable = 0

                        };
                        db.Clients.Add(_client);
                        db.SaveChanges();
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    // TO DO'
                    return -1;
                }
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

        public ApiResponse FlashRedeem(string voucherpin , long clientId)
        {
            var response = new ApiResponse
            {
                ResponseMessage = "Failed",
                ResponseType = -1

            };
            string ClientID = clientId.ToString();
            long UserID = 1;

            string trackingId = Guid.NewGuid().ToString();
            bool voucherState = false;

            //User Class
            string authUserName = Convert.ToString(ConfigurationManager.AppSettings["AuthUserName"]);
            string authPassWord = Convert.ToString(ConfigurationManager.AppSettings["AuthPassWord"]);

            //Acquirer Class
            string id = Convert.ToString(ConfigurationManager.AppSettings["Id"]);
            string password = Convert.ToString(ConfigurationManager.AppSettings["Password"]);

            //Device Class
            System.Web.HttpBrowserCapabilities browserInformation = new System.Web.HttpBrowserCapabilities
            {
                  
            };

            string channelId = Convert.ToString(ConfigurationManager.AppSettings["ChannelId"]);
            long appType = Convert.ToInt64(ConfigurationManager.AppSettings["AppType"]);
            string msisdn = Convert.ToString(ConfigurationManager.AppSettings["Msisdn"]);
            string platform = null;// (browserInformation.Platform);
            string platformVersion = null;// (browserInformation.Version);

            string accountNumber = Convert.ToString(ConfigurationManager.AppSettings["AccountNumber"]);
            Random random = new Random();
            long sequenceNumber = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssf")) + random.Next(1, 99999);

            var guid = Guid.NewGuid();
            string transactionGuid = guid.ToString();

            ApiClientUser User = new ApiClientUser
            {
                authUserName = authUserName,
                authPassWord = authPassWord
            };

            ApiClientAcquirer Acquirer = new ApiClientAcquirer
            {
                id = id,
                password = password,
                reference = ClientID
            };

            ApiClientDevice Device = new ApiClientDevice
            {
                msisdn = msisdn,
                channelId = channelId,
                platform = platform,
                platformVersion = platformVersion,
                appType = appType,
            };

            try
            {

                if (string.IsNullOrEmpty(voucherpin))
                {
                    string errorMessage = "Please enter a valid voucher code.";
                    response.ResponseMessage = errorMessage;
                }

                else
                {
                    TrackVoucher(string.Format("Tracking Id: {2} - {1} Start redeeming Flash Pin: {0}", voucherpin, clientId, trackingId));
                    var flash =  GetOneVoucher(User, accountNumber, sequenceNumber, voucherpin, Acquirer, Device, transactionGuid);
                    var flashRandValue = Convert.ToDecimal(flash.amountAuthorised.value) / 100;
                    if (flash.actionCode == "0000") //Activated/Success
                    {
                       TrackVoucher($"Tracking Id: {trackingId} - Flash API Success response code: {flash.actionCode} for voucher pin: {voucherpin} and Client ID: {ClientID}");
                       var updateBalance =  verifyVoucher(Convert.ToInt32(clientId), voucherpin, 1, 1, 1, DateTime.Now, Convert.ToInt64(flash.transactionReference), flashRandValue, false);
                        if(updateBalance == -1)
                        {
                            response.ResponseMessage = "Successfull";
                            response.ResponseType = 1;
                        }
                        else
                        {
                            string errorMessage = "An error has occurred. Please try again.";
                            response.ResponseMessage = errorMessage;
                            TrackVoucher(string.Format("Tracking Id: {3} - {2} Error redeeming Flash Pin: {0}, Reason: {1}", voucherpin, "API returned failed response", clientId, trackingId));
                            verifyVoucher(Convert.ToInt32(clientId), voucherpin, 1, 1, 2, DateTime.Now, Convert.ToInt64(flash.transactionReference), flashRandValue, false);
                        }

                    }
                    else if (flash.actionCode == "1824")
                    {
                        string errorMessage = "Voucher has already been used.";
                        response.ResponseMessage = errorMessage;  
                        TrackVoucher($"Tracking Id: {trackingId} - Flash API response code: {flash.actionCode} for voucher pin: {voucherpin} and Client ID: {ClientID}");
                    }
                    else
                    {
                        string errorMessage = "Invalid 1voucher code";
                        response.ResponseMessage = errorMessage;
                        TrackVoucher($"Tracking Id: {trackingId} - Flash API response code: {flash.actionCode} for voucher pin: {voucherpin} and Client ID: {ClientID}");
                        verifyVoucher(Convert.ToInt32(clientId), voucherpin, 1, 1, 2, DateTime.Now, Convert.ToInt64(flash.transactionReference), flashRandValue, false);
                    }
                }
            }

            catch (Exception ex)
            {
                TrackVoucher(string.Format("Tracking Id: {0} - {2} Exception, Reason: {1}", trackingId, ex.Message, clientId));
                TrackVoucher(string.Format("Tracking Id: {1} - {2} End redeeming Flash Pin: {0}", voucherpin, trackingId, clientId));
            }

            return response;
        }
        public static ApiClientOneVoucherRedeemResponse GetOneVoucher(ApiClientUser user, string accountNumber, long sequenceNumber, string voucherPin, ApiClientAcquirer acquirer, ApiClientDevice device, string transactionGuid)
        {
            try
            {
                var data = JukeBox.BLL.ExternalApi.Voucher.GetApiClientOneVoucher(new ApiClientOneVoucherRedeemFilter()
                {

                    user = user,
                    accountNumber = accountNumber,
                    sequenceNumber = sequenceNumber,
                    voucherPin = voucherPin,
                    acquirer = acquirer,
                    device = device,
                    transactionGuid = transactionGuid

                });

                return data;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void TrackVoucher(string details)
        {
         //   SystemLog.Log("Vouchers.Redeem", "Redeem Voucher", details, string.Empty);
        }
        private int verifyVoucher(int ClientID, string voucherpin, int voucherTypeId, int voucherTransactionTypeId, short voucherStatusId, DateTime redeemDateTime, long voucherReferenceId, decimal flashRandValue, bool isTxComplete)
        {
            using (var db = new JukeBoxEntities())
            {
                return db.sp__VoucherRedeemProcedure(ClientID,voucherpin,voucherTypeId,voucherTransactionTypeId,voucherStatusId,redeemDateTime,voucherReferenceId,flashRandValue,isTxComplete);
            }
        }
    }
}
