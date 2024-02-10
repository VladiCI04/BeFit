namespace BeFit.Common
{
    public class GeneralApplicationConstants
    {
        // Pages Constants
        public const int DefaultPage = 1;
        public const int EntitiesPerPage = 3;

        // Addmin Constants
        public const string AdminAreaName = "Admin";
        public const string AdminRoleName = "Administrator";
        public const string AdminEmail = "admin@befit.bg";

        // User Constants
        public const string UserCacheKey = "UsersCache";
        public const int UsersCacheDurationMinutes = 5;

        // Cookie Constants
        public const string OnlineUsersCookieName = "IsOnline";
        public const int LastActivityBeforeOfflineMinutes = 10;

        // SignalR Constant
        public const string SignalrRemoteMethod = "ReceiveMessage";
    }
}