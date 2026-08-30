using AwesomeAssertions;
using System;

namespace Soenneker.Extensions.DateTime.Hour.Tests;

public class DateTimeHourExtensionTests
{
    [Test]
    [Arguments(2024, 5, 20, 14, "Eastern Standard Time", "10:00 AM")]
    [Arguments(2024, 5, 20, 9, "Eastern Standard Time", "5:00 AM")]
    [Arguments(2024, 5, 20, 0, "Pacific Standard Time", "5:00 PM")]
    [Arguments(2024, 5, 20, 23, "Pacific Standard Time", "4:00 PM")]
    [Arguments(2024, 5, 20, 0, "India Standard Time", "5:30 AM")]
    public void ToTzHourFormatFromUtc_ShouldReturnCorrectHourFormat(int year, int month, int day, int utcHour, string timeZoneId, string expected)
    {
        // Arrange
        System.DateTime utcNow = new System.DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        System.TimeZoneInfo timeZoneInfo = System.TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

        // Act
        string result = utcNow.ToTzHourFormatFromUtc(utcHour, timeZoneInfo);

        // Assert
        result.Should().Be(expected);
    }

    [Test]
    public void ToTzHoursFromUtc_wraps_positive_offsets_to_zero_through_twenty_three()
    {
        var utcNow = new System.DateTime(2024, 5, 20, 0, 0, 0, DateTimeKind.Utc);
        System.TimeZoneInfo tokyo = System.TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

        int result = utcNow.ToTzHoursFromUtc(23, tokyo);

        result.Should().Be(8);
    }
}

