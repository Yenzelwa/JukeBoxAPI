using JukeBox.BLL.ExternalApi;
using JukeBox.BLL.Request;
using JukeBox.BLL.Response;
using JukeBox.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        public int  SaveCustomer(Customer client)
        {
            using (var db = new JukeBoxEntities())
            {
                try
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
                        BalanceAvailable =0

                    };
                    db.Customers.Add(_customer);
                    db.SaveChanges();
                    return 1;
                }
                catch(Exception e)
                {
                    // TO DO'
                    return 1;
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
