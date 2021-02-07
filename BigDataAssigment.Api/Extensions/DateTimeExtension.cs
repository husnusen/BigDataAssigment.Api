using System;
namespace BigDataAssigment.Api.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime GetDateTime(this long timeOffset) {

            var datetimeOffSet = DateTimeOffset.FromUnixTimeSeconds(timeOffset);
            return datetimeOffSet.UtcDateTime;
        }
    }
}
