namespace Utilities
{
    public static class Constants
    {
        public static class Auth
        {
            public static class TokenType
            {
                public static string Bearer => "Bearer";
            }
        }

        public static class ApiName
        {
            public static string SyX => "SyX";
            public static string VoucherTopUp => "VoucherTopUp";
            public static string ReferAFriend => "ReferAFriend";
            public static string BetTech => "BetTech";
            public static string CouponReport => "CouponReport";
            public static string Tuv => "HollyTopUp";
            public static string AfricaBetsSMS => "AfricaBetsSMS";
            public static string DSVirtual => "DSVirtual";
        }

        public static class Settings
        {
            public static class SettingEnvironment
            {
                public static string BetGames => "BetGames";
                public static string BetTech => "BetTech";
                public static string Coupon => "Coupon";
                public static string PrintService => "PrintService";
                public static string SyX => "SyX";
                public static string User => "User";
                public static string VoucherTopUp => "VoucherTopUp";
                public static string ReferAFriend => "ReferAFriend";
                public static string DSVirtual => "DSVirtual";
            }
            public static class SettingKey
            {
                public static string BetGamesApiBaseUrl => "BetGamesAPIbaseUrl";
                public static string BetGamesCurrencyCode => "BetGamesCurrencyCode";
                public static string BetGamesKey => "BetGamesKey";
                public static string BetGamesOperatorId => "BetGamesOperatorID";
                public static string BetGamesPartner => "BetGamesPartner";
                public static string BetGamesServer => "BetGamesServer";
                public static string BetGamesApiKey => "BetGamesApiKey";
                public static string DSVirtualSaltKey => "DSVirtualSaltKey";
                //public static string BetTechAPIbaseUrl { get { DSVirtualSaltKey "BetTechAPIbaseUrl"; } }

                public static string CouponHubUrl => "CouponHubUrl";
                public static string CouponRemoteServerHubUrl => "CouponRemoteServerHubUrl";
                public static string FileUploadPath => "FileUploadPath";
                public static string TokenExpiry => "TokenExpiry";
                public static string MessageServiceUrl => "MessageServiceUrl";
                public static string PrintServiceUrl => "PrintServiceUrl";
                public static string ApiClientLoginAttempts => "APIClientLoginAttempts";

                //public static string SyXUrl { get { return "SyXUrl"; } }
                public static string UserInvalidLoginAttempts => "UserInvalidLoginAttempts";
                public static string UserInvalidLoginAttemptsLockoutPeriod => "UserInvalidLoginAttemptsLockoutPeriod";
                //public static string VoucherTopUpUrl { get { return "VoucherTopUpUrl"; } }

                //public static string ReferAFriendUrl { get { return "ReferAFriendUrl"; } }
            }
            
        }
        public static class ClaimType
        {
            public static string Username => "Username";
            public static string PunterId => "PunterId";
            public static string BranchId => "BranchId";
            public static string CountryId => "CountryId";
            public static string FirstName => "FirstName";
            public static string LastName => "LastName";
            public static string CellPhone => "CellPhone";
            public static string Email => "Email";
            public static string TokenCategory => "TokenCategory";
        }
    }
}
