using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using JukeBoxApi.Models;
using JukeBoxApi.Providers;
using JukeBoxApi.Results;
using static JukeBoxApi.Models.ApiAccount;
using JukeBoxApi.Providers.Security;
using JukeBoxApi.Filters.JwtAuthFilters;
using JukeBox.Data;
using User = JukeBoxApi.Models.User;
using System.Web.Http.Cors;
using JukeBox.BLL.Response;
using JukeBox.BLL.Request;

namespace JukeBoxApi.Controllers
{
    
    [RoutePrefix("api/account")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        [AllowAnonymous]
        [Route("user/login")]
        [HttpPost]
        public ApiLoginUserResponse LoginUser([FromBody]ApiLoginRequest uf)
        {
            var apiResp = new ApiLoginUserResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = (new JukeBox.BLL.Account()).LoginUser(uf.username, uf.password);

            string incomingHash = (new JukeBox.BLL.Account()).HashAndObfuscate(uf.password);

            if (retVal ==null)
            {
                apiResp.ResponseMessage = "Invalid Username";
                apiResp.ResponseObject = null;
                return apiResp;
            }
            //check to see if the password is valid
            if (String.CompareOrdinal(incomingHash, retVal.Password) != 0)
            {
                apiResp.ResponseMessage = "Password invalid";
                apiResp.ResponseObject = null;
                return apiResp;
            }

            if (retVal != null)
            {
                var apiLoginUser = new ApiUser();
                apiLoginUser.Bind(retVal);
                apiResp.ResponseObject = apiLoginUser;
                apiResp.ResponseType = 1;
                apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("customer")]
        [HttpPost]
        public Response saveCustomer([FromBody]User user)
        {
            var apiLoginClient = new Response {  IsSuccess = false , Message ="Failed"};
         var retVal = (new JukeBox.BLL.Account()).SaveCustomer( 
                new JukeBox.Data.Customer {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CellPhone = user.Telephone,
                    DateCreated = DateTime.Now,
                    Email = user.Email,
                    BalanceAvailable=user.BalanceAvailable,
                    CustomerID=user.UserId,
                    ClientPassword= user.Password,
                    ImageFilePath=user.ImagePath

                    

                });

            if (retVal == "Sucess")
            {
                apiLoginClient.IsSuccess = true;
                apiLoginClient.Message = retVal;
                
                return apiLoginClient;

            }
            apiLoginClient.IsSuccess = false;
            apiLoginClient.Message = retVal;
            return apiLoginClient;
            
        }
        [AllowAnonymous]
        [Route("customer/login")]
        [HttpPost]
        public TokenResponse LoginCustomer([FromBody]ApiClientLoginRequest client)
        {
            var apiResp = new TokenResponse ();

            var retVal = (new JukeBox.BLL.Account()).LoginClient(client.username, client.password);

            if (retVal != null)
            {
                apiResp = JwtProvider.GetTokenResponse(retVal.FirstName, retVal);
                //var apiLoginClient = new ApiClient();
                //apiLoginClient.Bind(retVal);
                //apiResp.ResponseObject = apiLoginClient;
                //apiResp.ResponseType = 1;
                //apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }

        [AllowAnonymous]
        [Route("client")]
        [HttpPost]
        public Response saveClient([FromBody]JukeBoxApi.Models.Client user)
        {
            var apiLoginClient = new Response { IsSuccess = false, Message = "Failed" };
            var retVal = (new JukeBox.BLL.Account()).SaveClient(
                   new JukeBox.Data.Client
                   {
                       ClientID =  user.ClientID,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       ClientPassword = user.ClientPassword,
                       CellPhone = user.CellPhone,
                       FK_ClientStatusID = user.FK_ClientStatusID,
                       DateCreated = DateTime.Now,
                       Email = user.Email,
                       BalanceAvailable =user.BalanceAvailable,
                       ClientTitle = user.ClientTitle,
                       FK_CompanyID = 1,
                       FK_CountryID = 1,
                       DateOfBirth = user.DateOfBirth,
                       FK_IdentityTypeID =1,
                       Initials = user.Initials,
                       Gender = user.Gender,
                       IdentityTypeValue = user.IdentityTypeValue,
                       CreatedBy = 1
                   });

            if (retVal > 0)
            {
                apiLoginClient.IsSuccess = true;
                apiLoginClient.Message = "Sucess";

                return apiLoginClient;

            }
            return apiLoginClient;

        }
        [AllowAnonymous]
        [Route("client/login")]
        [HttpPost]
        public TokenResponse LoginClient([FromBody]ApiClientLoginRequest client)
        {
            var apiResp = new TokenResponse();

            var retVal = (new JukeBox.BLL.Account()).LoginClient(client.username, client.password);

            if (retVal != null)
            {
                apiResp = JwtProvider.GetTokenResponse(retVal.FirstName, retVal);
                //var apiLoginClient = new ApiClient();
                //apiLoginClient.Bind(retVal);
                //apiResp.ResponseObject = apiLoginClient;
                //apiResp.ResponseType = 1;
                //apiResp.ResponseMessage = "Success";
            }
            return apiResp;
        }
        [AllowAnonymous]
        [Route("clients")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public ApiClientResponse GeAllClients()
        {
            var apiClents = new ApiClientResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = (new JukeBox.BLL.Account()).GetAllClient();

            if (retVal != null)
            {
                apiClents.ResponseObject = new List<apiClient>();
                foreach (var item in retVal)
                {
                    var client = new apiClient();
                    client.Bind(item);
                    apiClents.ResponseObject.Add(client);
                }

                apiClents.ResponseType = 1;
                apiClents.ResponseMessage = "Success";
               

            }
            return apiClents;
        }

        [Route("customer/getcustomer")]
        [HttpPost]
        public User GeCustomer(UserRequest userRequest)
        {
            var apiLoginClient = new User();
            var clientId = Convert.ToInt32(userRequest.ClientId);
            var retVal = (new JukeBox.BLL.Account()).GetCustomerById(clientId);

            if (retVal != null)
            {
                apiLoginClient.Bind(retVal);

            }
            return apiLoginClient;
        }
        [AllowAnonymous]
        [Route("customer/redeem")]
        [HttpPost]
        public JukeBox.BLL.Response.ApiResponse RedeemVoucher(VoucherRequest request)
        {
            
            var apiResp = new JukeBox.BLL.Response.ApiResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var retVal = (new JukeBox.BLL.Account()).RedeemVoucher(request.VoucherPin, request.CustomerId);

       
            return retVal;
        }
        [AllowAnonymous]
        [Route("customer/checkottvoucher")]
        [HttpPost]
        public OttVoucherResponse ChechOTTVoucherStatus(OttVoucherStatusRequest request)
        {
       
            var retVal = (new JukeBox.BLL.Account()).CheckOTTVoucherStatus(request.UniqueReference, request.CustomerId);

            return retVal;
        }


        [AllowAnonymous]
        [Route("forgotpassword")]
        [HttpPost]
        public  Response ForgotPassword(ForgotPasswordRequest passwordRequest)
        {
            var apiLoginClient = new Response { IsSuccess = false, Message = "Failed" };

            var retVal =  (new JukeBox.BLL.Account()).SearchCustomer(passwordRequest.Email);

            if (retVal != null)
            {
                (new JukeBox.BLL.Account()).SendCode(retVal.CustomerID , passwordRequest.Email);
                apiLoginClient.IsSuccess = true;
                apiLoginClient.Message = "Sucess";

                return apiLoginClient;

            }
            apiLoginClient.Message = "Email doesn't exists";
            return apiLoginClient;
        }

        [AllowAnonymous]
        [Route("resetpassword/{password}")]
        [HttpGet]
        public Response ResetPassword(string password , string code)
        {
            var apiLoginClient = new Response { IsSuccess = false, Message = "Incorrect Code" };

            var retVal = (new JukeBox.BLL.Account()).ResetPasword(password, code);

            if (retVal > 0)
            {
                apiLoginClient.IsSuccess = true;
                apiLoginClient.Message = "Sucess";

                return apiLoginClient;

            }
            return apiLoginClient;
        }
        [AllowAnonymous]
        [Route("flash/token")]
        [HttpPost]
        public FlashTokenResponse GetToken()
        {
            var apiLoginClient = new Response { IsSuccess = false, Message = "Incorrect Code" };

            var retVal = JukeBox.BLL.ExternalApi.Voucher.GetTokenAsync();

            if (!String.IsNullOrEmpty(retVal.access_token))
            {
                apiLoginClient.IsSuccess = true;
                apiLoginClient.Message = retVal.access_token;

                return new FlashTokenResponse
                {
                    access_token = retVal.access_token,
                     expires_in=retVal.expires_in
                     
            };

            }
              return new FlashTokenResponse
            {
            }; 
        }


        [AllowAnonymous]
        [Route("delete/client/{id}")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public ApiClientResponse DeleteClient(int id)
        {
            var apiClents = new ApiClientResponse { ResponseType = -1, ResponseMessage = "Failed" };

            var deleted = (new JukeBox.BLL.Account()).DeleteClient(id,1);
            if (deleted)
            {
                var retVal = (new JukeBox.BLL.Account()).GetAllClient();

                if (retVal != null)
                {
                    apiClents.ResponseObject = new List<apiClient>();
                    foreach (var item in retVal)
                    {
                        var client = new apiClient();
                        client.Bind(item);
                        apiClents.ResponseObject.Add(client);
                    }

                    apiClents.ResponseType = 1;
                    apiClents.ResponseMessage = "Success";


                }
            }
            return apiClents;
        }

















        //    public AccountController()
        //    {
        //    }

        //    public AccountController(ApplicationUserManager userManager,
        //        ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        //    {
        //        UserManager = userManager;
        //        AccessTokenFormat = accessTokenFormat;
        //    }

        //    public ApplicationUserManager UserManager
        //    {
        //        get
        //        {
        //            return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //        }
        //        private set
        //        {
        //            _userManager = value;
        //        }
        //    }

        //    public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        //    // GET api/Account/UserInfo
        //    [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        //    [Route("UserInfo")]
        //    public UserInfoViewModel GetUserInfo()
        //    {
        //        ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

        //        return new UserInfoViewModel
        //        {
        //            Email = User.Identity.GetUserName(),
        //            HasRegistered = externalLogin == null,
        //            LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
        //        };
        //    }

        //    // POST api/Account/Logout
        //    [Route("Logout")]
        //    public IHttpActionResult Logout()
        //    {
        //        Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
        //        return Ok();
        //    }

        //    // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        //    [Route("ManageInfo")]
        //    public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        //    {
        //        IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

        //        if (user == null)
        //        {
        //            return null;
        //        }

        //        List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

        //        foreach (IdentityUserLogin linkedAccount in user.Logins)
        //        {
        //            logins.Add(new UserLoginInfoViewModel
        //            {
        //                LoginProvider = linkedAccount.LoginProvider,
        //                ProviderKey = linkedAccount.ProviderKey
        //            });
        //        }

        //        if (user.PasswordHash != null)
        //        {
        //            logins.Add(new UserLoginInfoViewModel
        //            {
        //                LoginProvider = LocalLoginProvider,
        //                ProviderKey = user.UserName,
        //            });
        //        }

        //        return new ManageInfoViewModel
        //        {
        //            LocalLoginProvider = LocalLoginProvider,
        //            Email = user.UserName,
        //            Logins = logins,
        //            ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
        //        };
        //    }

        //    // POST api/Account/ChangePassword
        //    [Route("ChangePassword")]
        //    public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
        //            model.NewPassword);

        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }

        //        return Ok();
        //    }

        //    // POST api/Account/SetPassword
        //    [Route("SetPassword")]
        //    public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }

        //        return Ok();
        //    }

        //    // POST api/Account/AddExternalLogin
        //    [Route("AddExternalLogin")]
        //    public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

        //        AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

        //        if (ticket == null || ticket.Identity == null || (ticket.Properties != null
        //            && ticket.Properties.ExpiresUtc.HasValue
        //            && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
        //        {
        //            return BadRequest("External login failure.");
        //        }

        //        ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

        //        if (externalData == null)
        //        {
        //            return BadRequest("The external login is already associated with an account.");
        //        }

        //        IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
        //            new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }

        //        return Ok();
        //    }

        //    // POST api/Account/RemoveLogin
        //    [Route("RemoveLogin")]
        //    public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        IdentityResult result;

        //        if (model.LoginProvider == LocalLoginProvider)
        //        {
        //            result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
        //        }
        //        else
        //        {
        //            result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
        //                new UserLoginInfo(model.LoginProvider, model.ProviderKey));
        //        }

        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }

        //        return Ok();
        //    }

        //    // GET api/Account/ExternalLogin
        //    [OverrideAuthentication]
        //    [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        //    [AllowAnonymous]
        //    [Route("ExternalLogin", Name = "ExternalLogin")]
        //    public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        //    {
        //        if (error != null)
        //        {
        //            return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
        //        }

        //        if (!User.Identity.IsAuthenticated)
        //        {
        //            return new ChallengeResult(provider, this);
        //        }

        //        ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

        //        if (externalLogin == null)
        //        {
        //            return InternalServerError();
        //        }

        //        if (externalLogin.LoginProvider != provider)
        //        {
        //            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //            return new ChallengeResult(provider, this);
        //        }

        //        ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
        //            externalLogin.ProviderKey));

        //        bool hasRegistered = user != null;

        //        if (hasRegistered)
        //        {
        //            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

        //             ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
        //                OAuthDefaults.AuthenticationType);
        //            ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
        //                CookieAuthenticationDefaults.AuthenticationType);

        //            AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
        //            Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
        //        }
        //        else
        //        {
        //            IEnumerable<Claim> claims = externalLogin.GetClaims();
        //            ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
        //            Authentication.SignIn(identity);
        //        }

        //        return Ok();
        //    }

        //    // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        //    [AllowAnonymous]
        //    [Route("ExternalLogins")]
        //    public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        //    {
        //        IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
        //        List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

        //        string state;

        //        if (generateState)
        //        {
        //            const int strengthInBits = 256;
        //            state = RandomOAuthStateGenerator.Generate(strengthInBits);
        //        }
        //        else
        //        {
        //            state = null;
        //        }

        //        foreach (AuthenticationDescription description in descriptions)
        //        {
        //            ExternalLoginViewModel login = new ExternalLoginViewModel
        //            {
        //                Name = description.Caption,
        //                Url = Url.Route("ExternalLogin", new
        //                {
        //                    provider = description.AuthenticationType,
        //                    response_type = "token",
        //                    client_id = Startup.PublicClientId,
        //                    redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
        //                    state = state
        //                }),
        //                State = state
        //            };
        //            logins.Add(login);
        //        }

        //        return logins;
        //    }

        //    // POST api/Account/Register
        //    [AllowAnonymous]
        //    [Route("Register")]
        //    public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

        //        IdentityResult result = await UserManager.CreateAsync(user, model.Password);

        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }

        //        return Ok();
        //    }

        //    // POST api/Account/RegisterExternal
        //    [OverrideAuthentication]
        //    [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        //    [Route("RegisterExternal")]
        //    public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var info = await Authentication.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return InternalServerError();
        //        }

        //        var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

        //        IdentityResult result = await UserManager.CreateAsync(user);
        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }

        //        result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result); 
        //        }
        //        return Ok();
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing && _userManager != null)
        //        {
        //            _userManager.Dispose();
        //            _userManager = null;
        //        }

        //        base.Dispose(disposing);
        //    }

        //    #region Helpers

        //    private IAuthenticationManager Authentication
        //    {
        //        get { return Request.GetOwinContext().Authentication; }
        //    }

        //    private IHttpActionResult GetErrorResult(IdentityResult result)
        //    {
        //        if (result == null)
        //        {
        //            return InternalServerError();
        //        }

        //        if (!result.Succeeded)
        //        {
        //            if (result.Errors != null)
        //            {
        //                foreach (string error in result.Errors)
        //                {
        //                    ModelState.AddModelError("", error);
        //                }
        //            }

        //            if (ModelState.IsValid)
        //            {
        //                // No ModelState errors are available to send, so just return an empty BadRequest.
        //                return BadRequest();
        //            }

        //            return BadRequest(ModelState);
        //        }

        //        return null;
        //    }

        //    private class ExternalLoginData
        //    {
        //        public string LoginProvider { get; set; }
        //        public string ProviderKey { get; set; }
        //        public string UserName { get; set; }

        //        public IList<Claim> GetClaims()
        //        {
        //            IList<Claim> claims = new List<Claim>();
        //            claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

        //            if (UserName != null)
        //            {
        //                claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
        //            }

        //            return claims;
        //        }

        //        public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
        //        {
        //            if (identity == null)
        //            {
        //                return null;
        //            }

        //            Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

        //            if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
        //                || String.IsNullOrEmpty(providerKeyClaim.Value))
        //            {
        //                return null;
        //            }

        //            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
        //            {
        //                return null;
        //            }

        //            return new ExternalLoginData
        //            {
        //                LoginProvider = providerKeyClaim.Issuer,
        //                ProviderKey = providerKeyClaim.Value,
        //                UserName = identity.FindFirstValue(ClaimTypes.Name)
        //            };
        //        }
        //    }

        //    private static class RandomOAuthStateGenerator
        //    {
        //        private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

        //        public static string Generate(int strengthInBits)
        //        {
        //            const int bitsPerByte = 8;

        //            if (strengthInBits % bitsPerByte != 0)
        //            {
        //                throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
        //            }

        //            int strengthInBytes = strengthInBits / bitsPerByte;

        //            byte[] data = new byte[strengthInBytes];
        //            _random.GetBytes(data);
        //            return HttpServerUtility.UrlTokenEncode(data);
        //        }
        //    }

        //    #endregion
        //}
    }
}