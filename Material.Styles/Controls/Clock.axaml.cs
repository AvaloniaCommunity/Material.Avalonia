using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.VisualTree;
using Material.Styles.Enums;

namespace Material.Styles.Controls;

[TemplatePart("PART_HoursBox", typeof(NumericUpDown))]
[TemplatePart("PART_MinutesBox", typeof(NumericUpDown))]
[TemplatePart("PART_SecondsBox", typeof(NumericUpDown))]
[TemplatePart("PART_AmSelector", typeof(RadioButton))]
[TemplatePart("PART_PmSelector", typeof(RadioButton))]
[TemplatePart("PART_CircleClockCarousel", typeof(Carousel))]
public class Clock : TemplatedControl {
    /// <summary>
    /// Defines the <see cref="SelectedTime"/> property
    /// </summary>
    public static readonly StyledProperty<TimeSpan?> SelectedTimeProperty =
        TimePicker.SelectedTimeProperty.AddOwner<Clock>();

    /// <summary>
    /// Defines the <see cref="TimeFormat"/> property
    /// </summary>
    public static readonly StyledProperty<TimeFormat> TimeFormatProperty =
        AvaloniaProperty.Register<Clock, TimeFormat>(nameof(TimeFormat), TimeFormat.TwentyFourHour);

    /// <summary>
    /// Defines the <see cref="CanSelectSeconds"/> property
    /// </summary>
    public static readonly StyledProperty<bool> CanSelectSecondsProperty =
        AvaloniaProperty.Register<Clock, bool>(nameof(CanSelectSeconds));

    /// <summary>
    /// Defines the <see cref="TimeSeparator"/> property
    /// </summary>
    public static readonly StyledProperty<string> TimeSeparatorProperty =
        AvaloniaProperty.Register<Clock, string>(nameof(TimeSeparator), ":");
    private Carousel _carousel = null!;


    private NumericUpDown _hoursTextBox = null!;
    private List<NumericUpDown> _inputBoxes = null!;
    internal bool _isUpdating;
    private NumericUpDown _minutesTextBox = null!;
    private NumericUpDown _secondsTextBox = null!;
    private NumericUpDown _selectedInputBox = null!;

    static Clock() {
        SelectedTimeProperty.Changed.AddClassHandler<Clock>((o, _) => o.UpdateSelectedTime());
    }

    /// <summary>
    /// Gets or sets the selected time. Can be null.
    /// </summary>
    public TimeSpan? SelectedTime {
        get { return GetValue(SelectedTimeProperty); }
        set { SetValue(SelectedTimeProperty, value); }
    }

    /// <summary>
    /// Gets or sets the time format
    /// </summary>
    public TimeFormat TimeFormat {
        get { return GetValue(TimeFormatProperty); }
        set { SetValue(TimeFormatProperty, value); }
    }

    /// <summary>
    /// Gets or sets is seconds selector is displayed
    /// </summary>
    public bool CanSelectSeconds {
        get { return GetValue(CanSelectSecondsProperty); }
        set { SetValue(CanSelectSecondsProperty, value); }
    }

    /// <summary>
    /// Gets or sets the string that separates the components of time, that is, the hour, minutes, and seconds.
    /// </summary>
    public string TimeSeparator {
        get { return GetValue(TimeSeparatorProperty); }
        set { SetValue(TimeSeparatorProperty, value); }
    }

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
        base.OnApplyTemplate(e);

        _hoursTextBox = e.NameScope.Find<NumericUpDown>("PART_HoursBox")!;
        _minutesTextBox = e.NameScope.Find<NumericUpDown>("PART_MinutesBox")!;
        _secondsTextBox = e.NameScope.Find<NumericUpDown>("PART_SecondsBox")!;
        _inputBoxes = new List<NumericUpDown> { _hoursTextBox, _minutesTextBox, _secondsTextBox };
        _selectedInputBox = _hoursTextBox;

        _carousel = e.NameScope.Find<Carousel>("PART_CircleClockCarousel")!;

        foreach (var circleClockPicker in _carousel.Items
                     .OfType<CircleClockPicker>()) {
            circleClockPicker.PointerReleased += CircleClockPickerOnPointerReleased;
        }

        _hoursTextBox.GotFocus += InputBoxOnGotFocusHandler;
        _minutesTextBox.GotFocus += InputBoxOnGotFocusHandler;
        _secondsTextBox.GotFocus += InputBoxOnGotFocusHandler;

        OnSelectedInputBoxChange();
        UpdateSelectedTime();
    }

    private void CircleClockPickerOnPointerReleased(object sender, PointerReleasedEventArgs e) {
        var circleClockPicker = (CircleClockPicker)sender;
        if (circleClockPicker.Value is null) {
            return;
        }

        var nextInputBox = _inputBoxes
            .Where(x => x.IsVisible)
            .SkipWhile(x => x != _selectedInputBox)
            .Skip(1)
            .DefaultIfEmpty(_inputBoxes[0])
            .First();

        // Setting focus to numeric up down TextBox
        nextInputBox
            .GetVisualDescendants()
            .OfType<IInputElement>()
            .FirstOrDefault(element => element.IsEffectivelyVisible && element.Focusable)
            ?.Focus();
    }

    private void InputBoxOnGotFocusHandler(object sender, GotFocusEventArgs e) {
        _selectedInputBox = (NumericUpDown)sender;
        OnSelectedInputBoxChange();
    }

    private void OnSelectedInputBoxChange() {
        foreach (var inputBox in _inputBoxes) inputBox.Classes.Remove("active");

        _selectedInputBox.Classes.Add("active");

        _carousel.SelectedIndex = _inputBoxes.IndexOf(_selectedInputBox);
    }

    private void UpdateSelectedTime() {
        _isUpdating = true;
        if (SelectedTime is null) {
            ClockInternals.SetIsAm(this, true);
            ClockInternals.SetHours(this, null);
            ClockInternals.SetMinutes(this, null);
            ClockInternals.SetSeconds(this, null);
            _isUpdating = false;
            return;
        }

        var time = SelectedTime.Value;
        var isAm = ClockInternals.IsAm(time.Hours);
        var hours = TimeFormat switch {
            TimeFormat.TwelveHour     => ClockInternals.ConvertTo12FormattedHours(time.Hours),
            TimeFormat.TwentyFourHour => time.Hours,
            _                         => throw new ArgumentOutOfRangeException()
        };
        ClockInternals.SetIsAm(this, isAm);
        ClockInternals.SetHours(this, hours);
        ClockInternals.SetMinutes(this, time.Minutes);
        ClockInternals.SetSeconds(this, time.Seconds);

        _isUpdating = false;
    }
}
public static class ClockInternals {
    public static readonly AttachedProperty<int?> HoursProperty =
        AvaloniaProperty.RegisterAttached<Clock, int?>("Hours", typeof(ClockInternals), defaultBindingMode: BindingMode.TwoWay);

    public static readonly AttachedProperty<int?> MinutesProperty =
        AvaloniaProperty.RegisterAttached<Clock, int?>("Minutes", typeof(ClockInternals), defaultBindingMode: BindingMode.TwoWay);

    public static readonly AttachedProperty<int?> SecondsProperty =
        AvaloniaProperty.RegisterAttached<Clock, int?>("Seconds", typeof(ClockInternals), defaultBindingMode: BindingMode.TwoWay);

    public static readonly AttachedProperty<bool> IsAmProperty =
        AvaloniaProperty.RegisterAttached<Clock, bool>("IsAm", typeof(ClockInternals));
    static ClockInternals() {
        HoursProperty.Changed.AddClassHandler<Clock>(OnValuesChanged);
        MinutesProperty.Changed.AddClassHandler<Clock>(OnValuesChanged);
        SecondsProperty.Changed.AddClassHandler<Clock>(OnValuesChanged);
        IsAmProperty.Changed.AddClassHandler<Clock>(OnValuesChanged);
    }
    public static TimeFormatToCellShiftConverter TimeFormatToCellShiftConverterInstance { get; } = new();

    private static void OnValuesChanged(Clock clock, AvaloniaPropertyChangedEventArgs args) {
        if (clock._isUpdating) {
            return;
        }

        if (GetHours(clock) is not { } hours) return;
        if (GetMinutes(clock) is not { } minutes) return;
        var seconds = GetSeconds(clock);
        if (clock.CanSelectSeconds && seconds is null) return;

        if (clock.TimeFormat == TimeFormat.TwelveHour) {
            hours = ConvertTo24FormattedHours(hours, GetIsAm(clock));
        }
        clock.SelectedTime = new TimeSpan(hours, minutes, seconds ?? 0);
    }

    public static int? GetHours(Clock element) {
        return element.GetValue(HoursProperty);
    }

    public static void SetHours(Clock element, int? value) {
        element.SetValue(HoursProperty, value);
    }

    public static int? GetMinutes(Clock element) {
        return element.GetValue(MinutesProperty);
    }

    public static void SetMinutes(Clock element, int? value) {
        element.SetValue(MinutesProperty, value);
    }

    public static int? GetSeconds(Clock element) {
        return element.GetValue(SecondsProperty);
    }

    public static void SetSeconds(Clock element, int? value) {
        element.SetValue(SecondsProperty, value);
    }

    public static bool GetIsAm(Clock element) {
        return element.GetValue(IsAmProperty);
    }

    public static void SetIsAm(Clock element, bool value) {
        element.SetValue(IsAmProperty, value);
    }

    public static int ConvertTo12FormattedHours(int twentyFourFormattedHours) {
        if (twentyFourFormattedHours >= 12) twentyFourFormattedHours -= 12;
        if (twentyFourFormattedHours == 0) twentyFourFormattedHours = 12;
        return twentyFourFormattedHours;
    }

    public static int ConvertTo24FormattedHours(int twelveFormattedHours, bool isAm) {
        if (isAm) {
            return twelveFormattedHours == 12 ? 0 : twelveFormattedHours;
        }
        if (twelveFormattedHours == 12) {
            return 12;
        }
        return twelveFormattedHours + 12;
    }

    public static bool IsAm(int twentyFourFormattedHours)
        => twentyFourFormattedHours < 12;

    public sealed class TimeFormatToCellShiftConverter : IValueConverter {
        /// <inheritdoc />
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            return value switch {
                TimeFormat.TwelveHour     => 1,
                TimeFormat.TwentyFourHour => 0,
                _                         => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }
        /// <inheritdoc />
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}