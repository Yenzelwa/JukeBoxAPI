
using JukeBox.Data;
using JukeBoxApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using Utilities;
using Microsoft.IdentityModel.Tokens;

namespace JukeBoxApi.Providers.Security
{
    public static class JwtProvider
    {
        public static AccessTokenModel GetTokenResponse(string username, Client client)
        {
            var claims = new List<Claim>
            {      CreateClaim(Constants.ClaimType.Username, username),
                   CreateClaim(Constants.ClaimType.PunterId, client.ClientID.ToString()),
                   CreateClaim(Constants.ClaimType.FirstName, client.FirstName),
                   CreateClaim(Constants.ClaimType.LastName, client.LastName),
                   CreateClaim(Constants.ClaimType.CellPhone, client.CellPhone),
                   CreateClaim(Constants.ClaimType.Email, client?.Email ?? "" )
            };

            var now = DateTime.Now;
            var expiresOnUtc = now.AddMinutes(Config.TokenExpiresMins).ToUniversalTime();
            var token = GenerateToken(username, claims, expiresOnUtc);

            var tokenResponse = new AccessTokenModel
            {
                Token = token,
                Type = Constants.Auth.TokenType.Bearer,
                ExpiryDateTimeUTC = expiresOnUtc
            };

            return tokenResponse;
        }

        private static string GenerateToken(string username, List<Claim> claims, DateTime expiresOnUtc)
        {
            var symmetricKey = Convert.FromBase64String(Config.JwtTokenSecret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresOnUtc,

                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(symmetricKey),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Config.JwtTokenSecret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception ex)
            {
                var msg = ex;
                return null;
            }
        }

        private static Claim CreateClaim(string type, string value)
        {
            var claim = new Claim(type, value);
            return claim;
        }

        public static List<Claim> GetClaims(string token)
        {
            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple.Identity as ClaimsIdentity;
            var claims = identity?.Claims.ToList();
            return claims;
        }

        public static BaseResponse<AccessTokenModel> RefreshToken(string token)
        {
            try
            {
                var claims = GetClaims(token);
                var simplePrinciple = GetPrincipal(token).Identity as ClaimsIdentity;
                var username = GetClaimValue<string>(Constants.ClaimType.Username, simplePrinciple);
                var now = DateTime.Now;
                var expiresOnUtc = now.AddMinutes(Config.TokenExpiresMins).ToUniversalTime();
                var newToken = GenerateToken(username, claims, expiresOnUtc);

                var tokenDetails = new BaseResponse<AccessTokenModel>
                {
                    ResponseObject = new AccessTokenModel
                    {
                        Token = newToken,
                        Type = Constants.Auth.TokenType.Bearer,
                        ExpiryDateTimeUTC = expiresOnUtc
                    },
                    ResponseMessage = "Success",
                    ResponseType = ResponseType.Success
                };

                return tokenDetails;
            }

            catch(Exception ex)
            {
                var error = new BaseResponse<AccessTokenModel>
                {
                    ResponseObject = null,
                    ResponseMessage = $"Failed - {ex.Message}",
                    ResponseType = ResponseType.Failed
                };

                return error;
            }

        }

        public static T GetClaimValue<T>(string type, ClaimsIdentity identity)
        {
            var claims = identity?.Claims.ToList();
            var value = claims?.SingleOrDefault(clm => clm.Type == type)?.Value;

            var tType = typeof(T);

            if (tType == typeof(string))
                return (T)(object)value;

            if (tType == typeof(int))
                return (T)(object)Convert.ToInt32(value);

            if (tType == typeof(long))
                return (T)(object)Convert.ToInt64(value);

            if (tType == typeof(DateTime))
                return (T)(object)Convert.ToDateTime(value);

            if (tType == typeof(bool))
                return (T)(object)Convert.ToBoolean(value);

            if (tType == typeof(decimal))
                return (T)(object)Convert.ToDecimal(value);

            //return string by default
            return (T)(object)value;
        }

        public static bool ValidatePunter(AuthenticationHeaderValue authentication, long punterId)
        {
            var token = authentication.Parameter;
            var simplePrincipal = JwtProvider.GetPrincipal(token).Identity as ClaimsIdentity;
            var jwtPunterId = JwtProvider.GetClaimValue<string>(Constants.ClaimType.PunterId, simplePrincipal);

            if (jwtPunterId == punterId.ToString())
            {
                return true;
            }

            return false;
        }

        public static string GetPunterIdByToken(string authentication)
        {

            //var token = authentication.Parameter;
            var simplePrincipal = JwtProvider.GetPrincipal(authentication).Identity as ClaimsIdentity;
            var jwtPunterId = JwtProvider.GetClaimValue<string>(Constants.ClaimType.PunterId, simplePrincipal);


            return jwtPunterId;
        }

        public static string GetPunterEmail(AuthenticationHeaderValue authentication)
        {
            var token = authentication.Parameter;
            var simplePrincipal = JwtProvider.GetPrincipal(token).Identity as ClaimsIdentity;
            var email = JwtProvider.GetClaimValue<string>(Constants.ClaimType.Email, simplePrincipal);
            if (!string.IsNullOrEmpty(email))
            {
                return email;
            }
            return "";
        }

    }
}