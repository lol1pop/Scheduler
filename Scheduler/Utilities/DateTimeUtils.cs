using System;
using System.Linq;

namespace Scheduler.Utilities {
    public static class DateTimeUtils
    {
        private static readonly double UpperTimeZone = TimeZoneInfo.GetSystemTimeZones().Last().BaseUtcOffset.TotalSeconds;
        private static readonly double LowerTimeZone = TimeZoneInfo.GetSystemTimeZones().First().BaseUtcOffset.TotalSeconds;

        /// <summary>
        /// Convert time zone to UTC date time.
        /// </summary>
        /// <param name="timeZone">Time zone offset in seconds (example: 3600 or -18000).
        /// Can be between -43200 and 50400 seconds. See <see cref="TimeZoneInfo.GetSystemTimeZones()"/> method in <see cref="TimeZoneInfo"/> class.</param>
        /// <param name="isClientTime">Set this parameter in true, when you need return client date time.</param>
        /// <returns>Server or client date time in UTC.</returns>
        public static DateTime GetDateTimeByTimeZone(int timeZone, bool isClientTime = false)
        {
            try
            {
                ValidateTimeZone(Convert.ToDouble(timeZone));

                var timeSpan = TimeSpan.FromSeconds(timeZone);

                var result = DateTime.UtcNow;

                if (isClientTime)
                {
                    result = result + timeSpan;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Convert time zone to UTC date time.
        /// </summary>
        /// <param name="timeZone">Time zone offset in seconds (example: 3600 or -18000).
        /// Can be between -43200 and 50400 seconds. See <see cref="TimeZoneInfo.GetSystemTimeZones()"/> method in <see cref="TimeZoneInfo"/> class.</param>
        /// <param name="isClientTime">Set this parameter in true, when you need return client date time.</param>
        /// <returns>Server or client date time in UTC.</returns>
        public static DateTime GetDateTimeByTimeZone(string timeZone, bool isClientTime = false)
        {
            try
            {
                if (string.IsNullOrEmpty(timeZone))
                {
                    throw new ArgumentNullException(nameof(timeZone));
                }

                var isParseTimeZonOffset = int.TryParse(timeZone, out var time);
                if (!isParseTimeZonOffset)
                {
                    throw new ArgumentException($"Failed to parse \"{nameof(timeZone)}\". Value: \"{timeZone}\".");
                }

                return GetDateTimeByTimeZone(time, isClientTime);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Convert time zone to UTC date time.
        /// </summary>
        /// <param name="timeZone">Time zone offset in seconds (example: 3600 or -18000).
        /// Can be between -43200 and 50400 seconds. See <see cref="TimeZoneInfo.GetSystemTimeZones()"/> method in <see cref="TimeZoneInfo"/> class.</param>
        /// <param name="localDateTime">Client date time in UTC.</param>
        /// <returns>Server or client date time in UTC.</returns>
        public static DateTime GetServerDateTimeByTimeZoneAndLocalDateTime(int timeZone, DateTime localDateTime)
        {
            try
            {
                ValidateTimeZone(Convert.ToDouble(timeZone));

                var timeSpan = TimeSpan.FromSeconds(timeZone);

                var result = localDateTime + timeSpan;

                return result.ToUniversalTime();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Get time zone by local date time.
        /// </summary>
        /// <param name="localDateTime">Local date time.</param>
        /// <returns>Time zone in integer format. <see cref="TimeZoneInfo.GetSystemTimeZones()"/>.</returns>
        public static int GetTimeZoneByLocalTime(DateTime localDateTime)
        {
            try
            {
                var localDateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(localDateTime);
                var serverDateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(DateTime.UtcNow);

                var localDateTimeWithoutSeconds = new DateTime(localDateTimeUtc.Year, localDateTimeUtc.Month, localDateTimeUtc.Day, localDateTimeUtc.Hour, localDateTimeUtc.Minute, localDateTimeUtc.Second);
                var serverDateTimeWithoutSeconds = new DateTime(serverDateTimeUtc.Year, serverDateTimeUtc.Month, serverDateTimeUtc.Day, serverDateTimeUtc.Hour, serverDateTimeUtc.Minute, serverDateTimeUtc.Second);

                var timeSpan = (localDateTimeWithoutSeconds - serverDateTimeWithoutSeconds).TotalSeconds;

                ValidateTimeZone(Convert.ToDouble(timeSpan));

                var result = Convert.ToInt32(timeSpan);
                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Validate time zone.
        /// </summary>
        /// <param name="timeZone">Time zone offset in seconds (example: 3600 or -18000).
        /// Can be between -43200 and 50400 seconds. See <see cref="TimeZoneInfo.GetSystemTimeZones()"/> method in <see cref="TimeZoneInfo"/> class.</param>
        public static void ValidateTimeZone(double timeZone)
        {
            if (timeZone < LowerTimeZone || timeZone > UpperTimeZone)
            {
                throw new ArgumentException($"Variable \"{nameof(timeZone)}\" can be between {LowerTimeZone} and {UpperTimeZone} seconds. Current value is: \"{timeZone}\".");
            }
        }
    }
}