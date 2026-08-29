[![](https://img.shields.io/nuget/v/soenneker.extensions.datetime.hour.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.hour/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.hour/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.hour/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.datetime.hour.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.hour/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.hour/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.hour/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.DateTime.Hour

A collection of helpful DateTime hour-based extension methods.

## Installation

```bash
dotnet add package Soenneker.Extensions.DateTime.Hour
```

## Quick start

```csharp
using Soenneker.Extensions.DateTime.Hour;

DateTime dateTime = DateTime.UtcNow;
var result = dateTime.ToStartOfHour();
```

## Common operations

- `ToStartOfHour()` - Adjusts the specified `System.DateTime` to the start of the current hour, effectively setting minutes, seconds, and milliseconds to zero. Returns a new `System.DateTime` representing the start of the hour for the provided datetime. The returned datetime will retain the original `DateTimeKind`.
- `ToStartOfNextHour()` - Adjusts the specified `System.DateTime` to the start of the next hour. Returns a new `System.DateTime` representing the start of the next hour. The returned datetime will retain the original `DateTimeKind`. This method does not take into account the time zone of the provided DateTime.
- `ToStartOfPreviousHour()` - Adjusts the specified `System.DateTime` to the start of the previous hour. Returns a new `System.DateTime` representing the start of the previous hour. The returned datetime will retain the original `DateTimeKind`. This method does not account for the time zone of the provided DateTime and retains the original DateTimeKind.
- `ToEndOfHour()` - Adjusts the specified `System.DateTime` to the end of the current hour, setting minutes and seconds to 59 and 59 respectively, and milliseconds to 999. Returns a new `System.DateTime` representing the end of the current hour. The returned datetime will retain the original `DateTimeKind`. This method disregards the time zone of the provided DateTime.
- `ToEndOfNextHour()` - Adjusts the given `System.DateTime` instance to the end of the next hour following the hour of the specified DateTime. Returns a `System.DateTime` that represents the last moment (one tick before the start of the next following hour) of the next hour after the one in which the specified DateTime falls.
- `ToEndOfPreviousHour()` - Adjusts the given `System.DateTime` instance to the end of the hour immediately preceding the hour of the specified DateTime.
- `ToTzHourFormat()` - Converts the specified UTC `System.DateTime` to the specified time zone and returns the time in "h:mmtt" format.
- `ToTzHourFormatWithTrim()` - Converts the specified UTC `System.DateTime` to the start of the hour in the specified time zone and returns the time in "h:mmtt" format.
- `ToHourFormat()` - Converts the specified `System.DateTime` to a string in "h:mmtt" format. Returns a string representing the time in "h:mmtt" format.
- `ToTzHoursFromUtc()` - Converts a specific hour in UTC to its corresponding hour in a specified time zone. Returns the corresponding hour in the specified time zone, adjusted to a positive number in the 24-hour format. This can include returning 24 to indicate midnight.
- `ToTzHourFormatFromUtc()` - Converts the given UTC hour to a specific time zone's hour format with AM/PM notation. Returns a string representing the hour in the specified time zone with AM/PM format.
