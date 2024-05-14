using System;
using System.Diagnostics.Contracts;
using Soenneker.Enums.UnitOfTime;

namespace Soenneker.Extensions.DateTime.Hour;

/// <summary>
/// Provides extension methods for <see cref="System.DateTime"/> focused on manipulating hour-related aspects of date and time, such as finding the start or end of the current, next, or previous hour. These methods are designed to be timezone-agnostic.
/// </summary>
public static class DateTimeHourExtension
{
    /// <summary>
    /// Adjusts the specified <see cref="System.DateTime"/> to the start of the current hour, effectively setting minutes, seconds, and milliseconds to zero.
    /// </summary>
    /// <param name="dateTime">The <see cref="System.DateTime"/> instance to adjust.</param>
    /// <returns>A new <see cref="System.DateTime"/> representing the start of the hour for the provided datetime. The returned datetime will retain the original <see cref="DateTimeKind"/>.</returns>
    /// <remarks>
    /// This method does not consider the time zone of the provided DateTime and retains the original DateTimeKind. Be cautious of time zone effects when using this method.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfHour(this System.DateTime dateTime)
    {
        return dateTime.ToStartOf(UnitOfTime.Hour);
    }

    /// <summary>
    /// Adjusts the specified <see cref="System.DateTime"/> to the start of the next hour.
    /// </summary>
    /// <param name="dateTime">The <see cref="System.DateTime"/> instance to adjust.</param>
    /// <returns>A new <see cref="System.DateTime"/> representing the start of the next hour. The returned datetime will retain the original <see cref="DateTimeKind"/>.</returns>
    /// <remarks>
    /// This method does not take into account the time zone of the provided DateTime. The returned datetime retains the original DateTimeKind, which could affect its interpretation.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfNextHour(this System.DateTime dateTime)
    {
        return dateTime.ToStartOfHour().AddHours(1);
    }

    /// <summary>
    /// Adjusts the specified <see cref="System.DateTime"/> to the start of the previous hour.
    /// </summary>
    /// <param name="dateTime">The <see cref="System.DateTime"/> instance to adjust.</param>
    /// <returns>A new <see cref="System.DateTime"/> representing the start of the previous hour. The returned datetime will retain the original <see cref="DateTimeKind"/>.</returns>
    /// <remarks>
    /// This method does not account for the time zone of the provided DateTime and retains the original DateTimeKind. Consider time zone implications when using this method.
    /// </remarks>
    [Pure]
    public static System.DateTime ToStartOfPreviousHour(this System.DateTime dateTime)
    {
        return dateTime.ToStartOfHour().AddHours(-1);
    }

    /// <summary>
    /// Adjusts the specified <see cref="System.DateTime"/> to the end of the current hour, setting minutes and seconds to 59 and 59 respectively, and milliseconds to 999.
    /// </summary>
    /// <param name="dateTime">The <see cref="System.DateTime"/> instance to adjust.</param>
    /// <returns>A new <see cref="System.DateTime"/> representing the end of the current hour. The returned datetime will retain the original <see cref="DateTimeKind"/>.</returns>
    /// <remarks>
    /// This method disregards the time zone of the provided DateTime. It retains the original DateTimeKind, which may influence its interpretation. This method is useful for inclusive end-of-hour calculations.
    /// </remarks>
    [Pure]
    public static System.DateTime ToEndOfHour(this System.DateTime dateTime)
    {
        return dateTime.ToEndOf(UnitOfTime.Hour);
    }

    /// <summary>
    /// Adjusts the given <see cref="System.DateTime"/> instance to the end of the next hour following the hour of the specified DateTime.
    /// </summary>
    /// <param name="dateTime">The DateTime instance to be adjusted.</param>
    /// <returns>A <see cref="System.DateTime"/> that represents the last moment (one tick before the start of the next following hour) of the next hour after the one in which the specified DateTime falls.</returns>
    /// <remarks>
    /// This method treats the DateTime as if it were in the UTC timezone, ignoring any embedded timezone information. Use this method with caution in time-sensitive applications, considering timezone differences where necessary.
    /// </remarks>
    /// <example>
    /// <code>
    /// var dateTime = new DateTime(2023, 8, 15, 3, 27, 0); // 3:27 AM
    /// var endOfNextHour = dateTime.ToEndOfNextHour();
    /// Console.WriteLine(endOfNextHour); // Outputs "2023-08-15 04:59:59.9999999"
    /// </code>
    /// </example>
    [Pure]
    public static System.DateTime ToEndOfNextHour(this System.DateTime dateTime)
    {
        System.DateTime result = dateTime.ToEndOfHour().AddHours(1);
        return result;
    }

    /// <summary>
    /// Adjusts the given <see cref="System.DateTime"/> instance to the end of the hour immediately preceding the hour of the specified DateTime.
    /// </summary>
    /// <param name="dateTime">The DateTime instance to be adjusted.</param>
    /// <returns>A <see cref="System.DateTime"/> that represents the last moment (one tick before the start of the hour of the original DateTime) of the hour before the one in which the specified DateTime falls.</returns>
    /// <remarks>
    /// This method calculates the end time of the hour before the one containing the input DateTime. The end of an hour is defined as one tick before the start of the following hour. For instance, if the input time is any time during the 3 PM hour, this method returns a DateTime representing 2:59:59.9999999 PM of the same day.
    /// <para>The method does not account for timezone information embedded within the DateTime. It treats the DateTime as if it were in the UTC timezone. Therefore, when working with DateTime instances in a specific timezone, convert them to UTC or otherwise handle timezone differences before using this method.</para>
    /// <example>Here is an example of its use:
    /// <code>
    /// var dateTime = new DateTime(2023, 8, 15, 3, 27, 0); // 3:27 AM
    /// var endOfPreviousHour = dateTime.ToEndOfPreviousHour();
    /// Console.WriteLine(endOfPreviousHour); // Outputs "2023-08-15 02:59:59.9999999"
    /// </code>
    /// </example>
    /// </remarks>
    [Pure]
    public static System.DateTime ToEndOfPreviousHour(this System.DateTime dateTime)
    {
        System.DateTime result = dateTime.ToEndOfHour().AddHours(-1);
        return result;
    }

    /// <summary>
    /// Converts the specified UTC <see cref="System.DateTime"/> to the specified time zone
    /// and returns the time in "h:mmtt" format.
    /// </summary>
    /// <param name="utc">The UTC <see cref="System.DateTime"/> to convert.</param>
    /// <param name="tzInfo">The <see cref="System.TimeZoneInfo"/> representing the desired time zone.</param>
    /// <returns>A string representing the time in "h:mmtt" format.</returns>
    [Pure]
    public static string ToTzHourFormat(this System.DateTime utc, System.TimeZoneInfo tzInfo)
    {
        System.DateTime tz = utc.ToTz(tzInfo);

        return tz.ToHourFormat();
    }

    /// <summary>
    /// Converts the specified UTC <see cref="System.DateTime"/> to the start of the hour
    /// in the specified time zone and returns the time in "h:mmtt" format.
    /// </summary>
    /// <param name="utc">The UTC <see cref="System.DateTime"/> to convert.</param>
    /// <param name="tzInfo">The <see cref="System.TimeZoneInfo"/> representing the desired time zone.</param>
    /// <returns>A string representing the start of the hour in "h:mmtt" format.</returns>
    [Pure]
    public static string ToTzHourFormatWithTrim(this System.DateTime utc, System.TimeZoneInfo tzInfo)
    {
        System.DateTime tz = utc.ToTz(tzInfo).ToStartOfHour();

        return tz.ToHourFormat();
    }

    /// <summary>
    /// Converts the specified <see cref="System.DateTime"/> to a string in "h:mmtt" format.
    /// </summary>
    /// <param name="dateTime">The <see cref="System.DateTime"/> to format.</param>
    /// <returns>A string representing the time in "h:mmtt" format.</returns>
    [Pure]
    public static string ToHourFormat(this System.DateTime dateTime)
    {
        return dateTime.ToString("h:mmtt");
    }
}
