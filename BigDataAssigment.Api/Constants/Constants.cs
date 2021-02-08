using System;
namespace BigDataAssigment.Api.Constants
{
    public static class Constants
    {
        public const string ApiResponseFormat = "json";
        public const string Lang = "tr";
        public static string[] ExcludedBlocks => new string[] { "hourly", "flags" };
        public const int CacheSize = 50;
        public const int CacheExpireTimeInMinutes = 60;
    }
}
