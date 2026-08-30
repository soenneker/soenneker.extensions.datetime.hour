[![](https://img.shields.io/nuget/v/soenneker.extensions.datetime.hour.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.hour/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.hour/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.hour/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.datetime.hour.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.datetime.hour/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.datetime.hour/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.datetime.hour/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.DateTime.Hour

Provides hour boundaries, previous/next hour navigation, and time-zone-aware hour formatting for `DateTime`.

## Installation

```bash
dotnet add package Soenneker.Extensions.DateTime.Hour
```

## Hour boundaries

```csharp
using Soenneker.Extensions.DateTime.Hour;

System.DateTime value = new(2026, 8, 29, 16, 42, 30, DateTimeKind.Utc);

System.DateTime start = value.ToStartOfHour();
System.DateTime end = value.ToEndOfHour();
System.DateTime previousStart = value.ToStartOfPreviousHour();
System.DateTime nextEnd = value.ToEndOfNextHour();
```

| Method | Result for `16:42:30` |
| --- | --- |
| `ToStartOfHour()` | `16:00:00` |
| `ToEndOfHour()` | One tick before `17:00:00` |
| `ToStartOfPreviousHour()` | `15:00:00` |
| `ToEndOfPreviousHour()` | One tick before `16:00:00` |
| `ToStartOfNextHour()` | `17:00:00` |
| `ToEndOfNextHour()` | One tick before `18:00:00` |

These methods operate on the existing clock fields. They do not perform time-zone conversion and preserve the input `Kind`.

## Formatting

```csharp
string clock = value.ToHourFormat(); // 4:42 PM

TimeZoneInfo eastern = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
string easternClock = value.ToTzHourFormat(eastern);
string easternHour = value.ToTzHourFormatWithTrim(eastern);
```

All three methods use the `h:mm tt` format and the current culture. `ToTzHourFormat()` converts the UTC input to the target wall clock. `ToTzHourFormatWithTrim()` also resets local minutes and seconds to zero.

## Convert a UTC hour

```csharp
System.DateTime referenceDate = new(2026, 8, 29, 0, 0, 0, DateTimeKind.Utc);
TimeZoneInfo india = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");

string localHour = referenceDate.ToTzHourFormatFromUtc(0, india); // 5:30 AM
```

`ToTzHourFormatFromUtc()` combines the date from the reference value with `utcHour`, converts that UTC instant to the supplied zone, and preserves fractional-hour offsets in the formatted result. Values outside `0–23` roll into the adjacent UTC date through normal `DateTime.AddHours()` behavior.

`ToTzHoursFromUtc()` returns only an integer hour from `0` through `23`. It uses the whole-hour component of the zone offset applicable on the reference date, so it cannot represent the minutes in half-hour or quarter-hour zones. Use the formatted method when those minutes matter.
