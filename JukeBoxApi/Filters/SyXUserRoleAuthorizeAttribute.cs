
using JukeBoxApi.Filters.JwtAuthFilters;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using Utilities;

namespace BETInternalWebServices.Filters
{
    public class SyXUserRoleAuthorizeAttribute : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }

        public bool AllowMultiple => false;
        public string ReasonPhrase { get; private set; }
        public HttpRequestMessage Request { get; private set; }
        
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {

            var request = context.Request;
            int roleId = Convert.ToInt32(HttpContext.Current.Application["SyxRoleId"]);
            if (roleId == Config.CacheDurationHours)
            {
                context.ErrorResult = new AuthenticationFailureResult("Access Denied!!!", request);
                return;
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }
    }
}