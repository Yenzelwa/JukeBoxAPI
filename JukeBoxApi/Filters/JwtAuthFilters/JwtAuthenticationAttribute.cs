
using JukeBoxApi.Providers.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;


namespace JukeBoxApi.Filters.JwtAuthFilters
{
    public class JwtAuthorizeAttribute : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;


            if (authorization == null || !string.Equals(authorization.Scheme, "Bearer", StringComparison.OrdinalIgnoreCase))
                return;

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
                return;
            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtToken(token);

            if (principal == null)
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);

            else
                context.Principal = principal;
        }

        private static bool ValidateToken(ClaimsIdentity identity, out string username)
        {
            username = null;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(Utilities.Constants.ClaimType.Username);
            username = usernameClaim?.Value;

            return !string.IsNullOrEmpty(username);
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            string username;
            var simplePrinciple = JwtProvider.GetPrincipal(token);
            var claimIdentity = simplePrinciple.Identity as ClaimsIdentity;

            var isTokenValid = ValidateToken(claimIdentity, out username);

            if (!isTokenValid)
                return Task.FromResult<IPrincipal>(null);

            var claims = JwtProvider.GetClaims(token);
            var identity = new ClaimsIdentity(claims, "Jwt");
            IPrincipal user = new ClaimsPrincipal(identity);

            return Task.FromResult(user);
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