using System.ComponentModel;

namespace Material.Styles.Enums; 

/// <summary>
/// Represents the time format options.
/// </summary>
public enum TimeFormat
{
    /// <summary>
    /// 12-hour time format.
    /// </summary>
    [Description("[12H] 12-hour format")]
    TwelveHour,

    /// <summary>
    /// 24-hour time format.
    /// </summary>
    [Description("[24H] 24-hour format")]
    TwentyFourHour
}
