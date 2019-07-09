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

        public bool FlashRedeem(string voucherpin , int clientId)
        {
            //string Sess = Application["SessionToken"].ToString();
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
                  //  UserAlerts("Please enter a valid voucher code.", false);
                }

                else
                {

                    TrackVoucher(string.Format("Tracking Id: {2} - {1} Start redeeming Flash Pin: {0}", voucherpin, clientId, trackingId));

                    var flash =  GetOneVoucher(User, accountNumber, sequenceNumber, voucherpin, Acquirer, Device, transactionGuid);

                    var flashRandValue = Convert.ToDecimal(flash.amountAuthorised.value) / 100;

                    if (flash.actionCode == "0000") //Activated/Success
                    {
                        TrackVoucher($"Tracking Id: {trackingId} - Flash API Success response code: {flash.actionCode} for voucher pin: {voucherpin} and Client ID: {ClientID}");

                        verifyVoucher(Convert.ToInt32(clientId), voucherpin, 3, 1, 3, DateTime.Now, flash.transactionReference, flashRandValue, "0");


                     //   var update = SyXWebApiHelper.Client.UpdateClientBalance(sessionToken, punter.ClientId, 48, flashRandValue, 1, "Mobile Voucher Redeem - Flash OneVoucher (Web) - " + flash.transactionReference, userID);
                        //if (update.ResponseType == 1)
                        //{

                        //    TrackVoucher(string.Format("Tracking Id: {3} - {2} Successful Redeem on Flash Pin: {0}, Value: {1}", voucherpin, flashRandValue, punter.ClientId, trackingId));
                        //    verifyVoucher(Convert.ToInt32(clientId), voucherpin, 3, 1, 1, DateTime.Now, flash.transactionReference, flashRandValue, "0");

                        //    string successMessage = string.Format("Voucher Redeemed Successfully for {0}", flashRandValue.ToString("C"));
                        //    UserAlerts(successMessage, true);
                        //    voucherState = true;

                        //    //Flash - BONUS
                        //    //check if punter/bookie is in exception list
                        //    var bonusAllowedPunter = SqlTools.GetDataTableSP("spCheckBonusException", new List<SqlParameter> { new SqlParameter("PunterID", punter.ClientId) }, ConfigurationManager.ConnectionStrings["HollyTopUpVoucher"].ConnectionString);
                        //    if (bonusAllowedPunter.Rows.Count > 0)
                        //    {
                        //        bool excep = Convert.ToBoolean(bonusAllowedPunter.Rows[0][0]);
                        //        if (excep != true)
                        //        {
                        //            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["StartTopUpBonus"]) && bool.Parse(ConfigurationManager.AppSettings["StartTopUpBonus"]))
                        //            {
                        //                var bonus = CheckVoucherHasBonus(DateTime.Now);
                        //                if (bonus != null)
                        //                {
                        //                    if (bonus.BonusID != 0)
                        //                    {
                        //                        ApplyBonus(bonus, Convert.ToInt32(flashRandValue), punter.ClientId, voucherpin, Convert.ToInt64(1), 1);
                        //                        TrackVoucher(string.Format("Tracking Id: {4} - {2} Successful Bonus for Pin: {0}, VoucherID: {3}, Value: {1}", voucherpin, flashRandValue.ToString(), punter.ClientId, 1, trackingId));
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}// End if - Successful credit on api

                        //else // Api call failed
                        //{
                            string errorMessage = "An error has occurred. Please try again.";
                            htMessage = errorMessage;
                         //   UserAlerts(errorMessage, false);
                            TrackVoucher(string.Format("Tracking Id: {3} - {2} Error redeeming Flash Pin: {0}, Reason: {1}", voucherpin, "API returned failed response", clientId, trackingId));
                            //Insert to db and to ensure we have the record in case SyX API is down.
                            verifyVoucher(Convert.ToInt32(clientId), voucherpin, 3, 1, 3, DateTime.Now, flash.transactionReference, flashRandValue, "0");

                        //}

                    }
                    else if (flash.actionCode == "1824")
                    {
                        string errorMessage = "Voucher has already been used.";
                        htMessage = errorMessage;
//UserAlerts(errorMessage, false);
                        TrackVoucher($"Tracking Id: {trackingId} - Flash API response code: {flash.actionCode} for voucher pin: {voucherpin} and Client ID: {ClientID}");
                    }
                    else
                    {
                        string errorMessage = "Invalid 1voucher code";
                        htMessage = errorMessage;
                    //    UserAlerts(errorMessage, false);
                        TrackVoucher($"Tracking Id: {trackingId} - Flash API response code: {flash.actionCode} for voucher pin: {voucherpin} and Client ID: {ClientID}");
                    }
                }
            }

            catch (Exception ex)
            {
              //  UserAlerts(ex.Message, false);
                TrackVoucher(string.Format("Tracking Id: {0} - {2} Exception, Reason: {1}", trackingId, ex.Message, clientId));
                TrackVoucher(string.Format("Tracking Id: {1} - {2} End redeeming Flash Pin: {0}", voucherpin, trackingId, clientId));
            }

            return voucherState;
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
        private DataTable verifyVoucher(int ClientID, string voucherpin, int voucherTypeId, int voucherTransactionTypeId, int voucherStatusId, DateTime redeemDateTime, string voucherReferenceId, decimal flashRandValue, string isTxComplete)
        {
            return SqlTools.GetDataTableSP("sp_ VoucherRedeemProcedure", new List<SqlParameter> {new SqlParameter("clientId", ClientID),
                new SqlParameter("voucherPin", voucherpin),
                new SqlParameter("voucherTypeId", voucherTypeId),
                new SqlParameter("voucherTransactionTypeId", voucherTransactionTypeId),
                new SqlParameter("voucherStatusId", voucherStatusId),
                new SqlParameter("redeemDateTime", redeemDateTime),
                new SqlParameter("voucherReferenceId", voucherReferenceId),
                new SqlParameter("amount", flashRandValue),
                new SqlParameter("isTxComplete", isTxComplete)},
            ConfigurationManager.ConnectionStrings["HollywoodbetsConnectionString"].ConnectionString);
        }
    }
}
