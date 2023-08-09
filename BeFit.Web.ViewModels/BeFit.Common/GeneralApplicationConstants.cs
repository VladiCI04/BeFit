using System.Dynamic;

namespace BeFit.Common
{
    public class GeneralApplicationConstants
    {
        public const int DefaultPage = 1;
        public const int EntitiesPerPage = 3;

        public const string AdminAreaName = "Admin";
        public const string AdminRoleName = "Administrator";
        public const string AdminEmail = "admin@befit.bg";

        public const string UserCacheKey = "UsersCache";
        public const int UsersCacheDurationMinutes = 5;

        public const string OnlineUsersCookieName = "IsOnline";
        public const int LastActivityBeforeOfflineMinutes = 10;
    }
}