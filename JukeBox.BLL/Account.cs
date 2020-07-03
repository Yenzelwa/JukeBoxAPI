using JukeBox.BLL.ExternalApi;
using JukeBox.BLL.Request;
using JukeBox.BLL.Response;
using JukeBox.Data;
using RestSharp;
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
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Xml;
using System.Xml.Linq;

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
        public bool DeleteClient(int clientId, int userId)
        {
            using (var db = new JukeBoxEntities())
            {
                try
                {
                    var client = db.Clients.Where(x => x.ClientID == clientId).FirstOrDefault();
                    client.Enabled = false;
                    client.CreatedBy = userId;
                    db.SaveChanges();

                    return true;
                    
                }
                catch (Exception)
                {

                    return false;
                }

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
                return db.Clients.Where(x=>x.Enabled == true).ToList();
            }
        }
        public Client  GetClientById(long id)
        {
            using (var db = new JukeBoxEntities())
            {
                return db.Libraries.Where(x => x.LibraryID == id || x.FK_ClientID == id).Select(u => u.Client).FirstOrDefault() ;
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
            sbody.Append("Dear  User, <br/><br/> We received a request to reset your password. Your  verification code is: <br/><br/>  ");
            sbody.Append( clientCode + " <br/><br/>");
            sbody.Append("If you did not request this code, it is possible that someone else is trying to access the  Account . Do not forward or give this code to anyone. <br/><br/>");
            sbody.Append("Sincerely yours, <br/> JukeBox World Team");
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("igagasimediaapp@gmail.com", email, "Reset Your Password", sbody.ToString());

            SmtpClient mailclient = new SmtpClient();
            mailclient.Host = "smtp.gmail.com";
            mailclient.EnableSsl = true;
            mailclient.UseDefaultCredentials = true;
            mailclient.UseDefaultCredentials = true;
            mailclient.Credentials = new NetworkCredential("igagasimediaapp@gmail.com", "NoksD1990");
            mailclient.Port = 587;
   
            // here am setting the property IsBodyHtml true because i am using html tags inside the mail's code
            mail.IsBodyHtml = true;
           mailclient.Send(mail);
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
                        customerToUpdate.ImageFilePath = client.ImageFilePath;
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
                            BalanceAvailable = 0,
                            ImageFilePath=client.ImageFilePath

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
                        var clientToUpdate = db.Clients.Where(x=>x.ClientID == client.ClientID).FirstOrDefault();
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
                            BalanceAvailable = 0,
                            Enabled=true
                            

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

        public ApiResponse RedeemVoucher(string voucherpin , long clientId)
        {

            var ott = redeemOTTVoucher(voucherpin, clientId);
            if(ott.ResponseMessage != "Success")
            {
             var flash =  FlashVoucherRedeem(voucherpin, clientId);
                return flash;
            }
            return ott;
        }

        private ApiResponse FlashVoucherRedeem(string voucherpin, long clientId)
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


            ApiClientUser User = new ApiClientUser
            {
                authUserName = authUserName,
                authPassWord = authPassWord,
                purseAccountNumber = accountNumber
            };

            ApiClientAcquirer Acquirer = new ApiClientAcquirer
            {
                id = id,
                password = password,
                reference = ClientID
            };

            try
            {
                var reference = "Flash - Voucher";
                if (string.IsNullOrEmpty(voucherpin))
                {
                    string errorMessage = "Please enter a valid voucher code.";
                    response.ResponseMessage = errorMessage;
                }

                else
                {
                    TrackVoucher(string.Format("Tracking Id: {2} - {1} Start redeeming Flash Pin: {0}", voucherpin, clientId, trackingId));
                    var flash = GetOneVoucher(User, sequenceNumber, voucherpin, Acquirer);
                    var flashRandValue = Convert.ToDecimal(flash.amountAuthorised) / 100;
                    if (flash.actionCode == "0000") //Activated/Success
                    {

                        TrackVoucher($"Tracking Id: {trackingId} - Flash API Success response code: {flash.actionCode} for voucher pin: {voucherpin} and Client ID: {ClientID}");
                        var updateBalance = verifyVoucher(Convert.ToInt32(clientId), voucherpin, 1, 1, 1, DateTime.Now, Convert.ToInt64(flash.transactionReference), flashRandValue, false, reference);
                        if (updateBalance == null)
                        {
                            response.ResponseMessage = "Successfull";
                            response.ResponseType = 1;
                        }
                        else
                        {
                            string errorMessage = "An error has occurred. Please try again.";
                            response.ResponseMessage = errorMessage;
                            TrackVoucher(string.Format("Tracking Id: {3} - {2} Error redeeming Flash Pin: {0}, Reason: {1}", voucherpin, "API returned failed response", clientId, trackingId));
                            var voucher = verifyVoucher(Convert.ToInt32(clientId), voucherpin, 1, 1, 2, DateTime.Now, Convert.ToInt64(flash.transactionReference), flashRandValue, false, reference);
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
                      //  verifyVoucher(Convert.ToInt32(clientId), voucherpin, 1, 1, 2, DateTime.Now, Convert.ToInt64(flash.transactionReference), flashRandValue, false, reference);
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

        public static OttVoucherResponse GetOTTVoucher(OttVoucherRequest  ottVoucherRequest)
        {
            try
            {
               
                var data = JukeBox.BLL.ExternalApi.OTT.VoucherOTT.RedeemOTT(ottVoucherRequest);
                return data;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static ApiClientOneVoucherRedeemResponse GetOneVoucher(ApiClientUser user, long sequenceNumber, string voucherPin, ApiClientAcquirer acquirer)
        {
            try
            {
                var accessToken = "";
                var refreshToken = "";
                var token = HttpContext.Current.Application["TokenSession"] as TokenSession;
                if (token !=null)
                {
                    accessToken = token.accessToken;
                       if (Convert.ToDateTime(token.ExpDate) <= DateTime.Now)
                            {
                        var refreshTokenFlash = JukeBox.BLL.ExternalApi.Voucher.GetTokenAsync();
                        accessToken = refreshTokenFlash.access_token;
                        refreshToken = refreshTokenFlash.refresh_token;
                        var date = DateTime.Now.AddMinutes(55);
                        HttpContext.Current.Application.Remove("TokenSession");
                        var tokenSession = new TokenSession
                        {
                            accessToken = accessToken,
                            refreshToken = refreshToken,
                            ExpDate = date
                        };
                        HttpContext.Current.Application["TokenSession"] = tokenSession;

                    }
    
                        
                    
                }
                else
                {
                 
                     var tokenFlash = JukeBox.BLL.ExternalApi.Voucher.GetTokenAsync();
                     accessToken = tokenFlash.access_token;
                    refreshToken = tokenFlash.refresh_token;
                    var date = DateTime.Now.AddMinutes(55);
                    var tokenSession = new TokenSession
                    {
                        accessToken = accessToken,
                        refreshToken = refreshToken,
                        ExpDate = date
                    };
                    HttpContext.Current.Application["TokenSession"] = tokenSession;
                   



                }

                var data = JukeBox.BLL.ExternalApi.Voucher.GetApiClientOneVoucher(new ApiClientOneVoucherRedeemFilter()
                {

                    user = user,
                    sequenceNumber = sequenceNumber,
                    voucherPin = voucherPin,
                    acquirer = acquirer,
                    amountRequested = 0,
                    currency = "ZAR"

                } , accessToken);
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
        private sp__VoucherRedeemProcedure_Result verifyVoucher(int ClientID, string voucherpin, int voucherTypeId, int voucherTransactionTypeId, short voucherStatusId, DateTime redeemDateTime, long voucherReferenceId, decimal flashRandValue, bool isTxComplete , string ReferenceComment)
        {
            using (var db = new JukeBoxEntities())
            {
                return db.sp__VoucherRedeemProcedure(ClientID,voucherpin,voucherTypeId,voucherTransactionTypeId,voucherStatusId,redeemDateTime,voucherReferenceId,flashRandValue,isTxComplete, ReferenceComment).FirstOrDefault();
            }
        }
        public ApiResponse redeemOTTVoucher(string voucherpin, long clientId)
        {
            var response = new ApiResponse
            {
                ResponseMessage = "Failed",
                ResponseType = -1

            };
            string trackingId = Guid.NewGuid().ToString();
            string userName = Convert.ToString(ConfigurationManager.AppSettings["OTTUserName"]);
            string passWord = Convert.ToString(ConfigurationManager.AppSettings["OTTPassWord"]);
            int vendorId = Convert.ToInt32(ConfigurationManager.AppSettings["OTTVendorId"]);
            try
            {
                var xmlDoc = new XDocument( new XElement("RedeemVoucher",
            new XElement("userName", userName),
            new XElement("password", passWord),
            new XElement("unique_reference", trackingId.ToString()),
            new XElement("VendorID",vendorId ),
            new XElement("pinCode", ""),
            new XElement("accountCode", clientId.ToString()),
            new XElement("clientID", "1234"),
            new XElement("msisdn", "")));
                var ottVoucherRequest = new OttVoucherRequest
                {
                     body =  new OttVoucherRequest.Body
                     {
                          redeemVoucher =  new OttVoucherRequest.RedeemVoucher
                          {
                              UserName = userName,
                              Password = passWord,
                              VendorId = vendorId,
                              ClientId = clientId.ToString(),
                              AccountCode = "",
                              msisdn = "",
                              PinCode = "1234",
                              UniqueReference = trackingId.ToString()
                          }
                     }

                };
                var reference = "OTT - Voucher";
                TrackVoucher(string.Format("Tracking Id: {2} - {1} Start redeeming Flash Pin: {0}", voucherpin, clientId, trackingId));
                var ott = GetOTTVoucher(ottVoucherRequest);

                if (ott.body.voucherResponse.redeemVoucherResult.Message == "Success") //Activated/Success
                {
                    
                    TrackVoucher($"Tracking Id: {trackingId} - Flash API Success response code: {ott.body.voucherResponse.redeemVoucherResult.Message} for voucher pin: {voucherpin} and Client ID: {clientId}");
                    var updateBalance = verifyVoucher(Convert.ToInt32(clientId), voucherpin, 1, 1, 1, DateTime.Now, Convert.ToInt64(ott.body.voucherResponse.redeemVoucherResult.Reference), ott.body.voucherResponse.redeemVoucherResult.Amount, false, reference);
                    if (updateBalance == null)
                    {
                        response.ResponseMessage = "Successfull";
                        response.ResponseType = 1;
                    }
                    else
                    {
                        string errorMessage = "An error has occurred. Please try again.";
                        response.ResponseMessage = errorMessage;
                        TrackVoucher(string.Format("Tracking Id: {3} - {2} Error redeeming Flash Pin: {0}, Reason: {1}", voucherpin, "API returned failed response", clientId, trackingId));
                        var voucher = verifyVoucher(Convert.ToInt32(clientId), voucherpin, 1, 1, 2, DateTime.Now, Convert.ToInt64(ott.body.voucherResponse.redeemVoucherResult.Reference), ott.body.voucherResponse.redeemVoucherResult.Amount, false, reference);
                    }

                }
                else if (ott.body.voucherResponse.redeemVoucherResult.ErrorCode == "1824")
                {
                    string errorMessage = "Voucher has already been used.";
                    response.ResponseMessage = errorMessage;
                    TrackVoucher($"Tracking Id: {trackingId} - OTT API response code: {ott.body.voucherResponse.redeemVoucherResult.ErrorCode} for voucher pin: {voucherpin} and Client ID: {clientId}");
                }
                else
                {
                    string errorMessage = "Invalid 1voucher code";
                    response.ResponseMessage = errorMessage;
                    TrackVoucher($"Tracking Id: {trackingId} - OTT API response code: {ott.body.voucherResponse.redeemVoucherResult.ErrorCode} for voucher pin: {voucherpin} and Client ID: {clientId}");
                    //verifyVoucher(Convert.ToInt32(clientId), voucherpin, 2, 1, 2, DateTime.Now, Convert.ToInt64(ott.body.voucherResponse.redeemVoucherResult.Reference), ott.body.voucherResponse.redeemVoucherResult.Amount, false, reference);
                }
                return response;
            }

            catch (Exception ex)
            {
                TrackVoucher(string.Format("Tracking Id: {0} - {2} Exception, Reason: {1}", trackingId, ex.Message, clientId));
                TrackVoucher(string.Format("Tracking Id: {1} - {2} End redeeming Flash Pin: {0}", voucherpin, trackingId, clientId));

                return response;
            }


            }
        public OttVoucherResponse CheckOTTVoucherStatus(string uniqueReference , long clientId)
        {
            var response = new OttVoucherResponse
            {
                 body = new OttVoucherResponse.Body
                 {
                      voucherResponse = new OttVoucherResponse.Body.VoucherResponse
                      {
                          redeemVoucherResult = new OttVoucherResponse.RedeemVoucherResult
                          {
                              Message = "Failed",
                              Amount = 0,
                              ErrorCode = "4",

                          }
                      }
                 }
            };
            string trackingId = Guid.NewGuid().ToString();
            string userName = Convert.ToString(ConfigurationManager.AppSettings["OTTUserName"]);
            string passWord = Convert.ToString(ConfigurationManager.AppSettings["OTTPassWord"]);
            int vendorId = Convert.ToInt32(ConfigurationManager.AppSettings["OTTVendorId"]);
            try
            {
                var xmlDoc = new XDocument(new XElement("RedeemVoucher",
            new XElement("userName", userName),
            new XElement("password", passWord),
            new XElement("unique_reference", trackingId.ToString()),
            new XElement("VendorID", vendorId),
            new XElement("pinCode", ""),
            new XElement("accountCode", clientId.ToString()),
            new XElement("clientID", "1234"),
            new XElement("msisdn", "")));
                var ottVoucherRequest = new OttCheckStatusVoucherRequest
                {
                    body = new OttCheckStatusVoucherRequest.Body
                    {
                        getStatus = new OttCheckStatusVoucherRequest.GetStatus
                        {
                            UserName = userName,
                            Password = passWord,
                            UniqueReference = uniqueReference
                        }
                    }

                };
                var reference = "OTT - Voucher";
                TrackVoucher(string.Format("Tracking Id: {2} - {1} Start redeeming Flash Pin: {0}", uniqueReference, clientId, uniqueReference));
                var ott = JukeBox.BLL.ExternalApi.OTT.VoucherOTT.CheckVoucherStatusOTT(ottVoucherRequest);

                if (ott.body.voucherStatusResponse.statusResult.Message == "Success") //Activated/Success
                {

                    TrackVoucher($"Tracking Id: {trackingId} - Flash API Success response code: {ott.body.voucherStatusResponse.statusResult.Message} for voucher pin: {uniqueReference} and Client ID: {clientId}");
                    var updateBalance = verifyVoucher(Convert.ToInt32(clientId), uniqueReference, 1, 1, 1, DateTime.Now, Convert.ToInt64(ott.body.voucherStatusResponse.statusResult.Reference), ott.body.voucherStatusResponse.statusResult.Amount, false, reference);
                    if (updateBalance == null)
                    {
                        response.body.voucherResponse.redeemVoucherResult.Message = "Successfull";
                        response.body.voucherResponse.redeemVoucherResult.ErrorCode = "0";
                        response.body.voucherResponse.redeemVoucherResult.Amount = updateBalance.Amount;
                    }
                    else
                    {
                        string errorMessage = "An error has occurred. Please try again.";
                        response.body.voucherResponse.redeemVoucherResult.Message = errorMessage;
                        TrackVoucher(string.Format("Tracking Id: {3} - {2} Error redeeming Flash Pin: {0}, Reason: {1}", uniqueReference, "API returned failed response", clientId, trackingId));
                        var voucher = verifyVoucher(Convert.ToInt32(clientId), uniqueReference, 1, 1, 2, DateTime.Now, Convert.ToInt64(ott.body.voucherStatusResponse.statusResult.Reference), ott.body.voucherStatusResponse.statusResult.Amount, false, reference);
                    }

                }
                else if (ott.body.voucherStatusResponse.statusResult.ErrorCode == "1824")
                {
                    string errorMessage = "Voucher has already been used.";
                    response.body.voucherResponse.redeemVoucherResult.Message = errorMessage;
                    TrackVoucher($"Tracking Id: {trackingId} - OTT API response code: {ott.body.voucherStatusResponse.statusResult.ErrorCode} for voucher pin: {uniqueReference} and Client ID: {clientId}");
                }
                else
                {
                    string errorMessage = "Invalid";
                    response.body.voucherResponse.redeemVoucherResult.Message = errorMessage;
                    TrackVoucher($"Tracking Id: {trackingId} - OTT API response code: {ott.body.voucherStatusResponse.statusResult.ErrorCode} for voucher pin: {uniqueReference} and Client ID: {clientId}");
                   // verifyVoucher(Convert.ToInt32(clientId), uniqueReference, 2, 1, 2, DateTime.Now, Convert.ToInt64(ott.body.voucherStatusResponse.statusResult.Reference), ott.body.voucherStatusResponse.statusResult.Amount, false, reference);
                }
                return response;
            }

            catch (Exception ex)
            {
                TrackVoucher(string.Format("Tracking Id: {0} - {2} Exception, Reason: {1}", trackingId, ex.Message, clientId));
                TrackVoucher(string.Format("Tracking Id: {1} - {2} End redeeming Flash Pin: {0}", uniqueReference, trackingId, clientId));

                return response;
            }


        }

    }
}
