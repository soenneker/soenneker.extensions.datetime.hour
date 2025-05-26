using AwesomeAssertions;
using System;
using Xunit;

namespace Soenneker.Extensions.DateTime.Hour.Tests;

public class DateTimeHourExtensionTests
{
    [Theory]
    [InlineData(2024, 5, 20, 14, "Eastern Standard Time", "10:00 AM")]
    [InlineData(2024, 5, 20, 9, "Eastern Standard Time", "5:00 AM")]
    [InlineData(2024, 5, 20, 0, "Pacific Standard Time", "5:00 PM")]
    [InlineData(2024, 5, 20, 23, "Pacific Standard Time", "4:00 PM")]
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
}
