using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Material.Styles.Enums;

namespace Material.Styles.Controls;

[TemplatePart("PART_HoursBox", typeof(NumericUpDown))]
[TemplatePart("PART_MinutesBox", typeof(NumericUpDown))]
[TemplatePart("PART_SecondsBox", typeof(NumericUpDown))]
[TemplatePart("PART_AmSelector", typeof(RadioButton))]
[TemplatePart("PART_PmSelector", typeof(RadioButton))]
[TemplatePart("PART_CircleClockContainer", typeof(TransitioningContentControl))]
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


    private NumericUpDown _hoursTextBox = null!;
    private NumericUpDown _minutesTextBox = null!;
    private NumericUpDown _secondsTextBox = null!;
    private NumericUpDown _selectedTextBox = null!;
    private List<NumericUpDown>? _inputBoxes;
    private TransitioningContentControl _circleClockContainer = null!;
    private RadioButton _amSelectorRadioButton = null!;
    private RadioButton _pmSelectorRadioButton = null!;
    internal bool _isUpdating;

    static Clock() {
        SelectedTimeProperty.Changed.AddClassHandler<Clock>((o, args) => o.UpdateSelectedTime());
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
        _inputBoxes = new List<NumericUpDown>() { _hoursTextBox, _minutesTextBox, _secondsTextBox };
        _selectedTextBox = _hoursTextBox;

        _amSelectorRadioButton = e.NameScope.Find<RadioButton>("PART_AmSelector");
        _pmSelectorRadioButton = e.NameScope.Find<RadioButton>("PART_PmSelector");

        _circleClockContainer = e.NameScope.Find<TransitioningContentControl>("PART_CircleClockContainer")!;

        _hoursTextBox.GotFocus += TextBoxOnGotFocusHandler;
        _minutesTextBox.GotFocus += TextBoxOnGotFocusHandler;
        _secondsTextBox.GotFocus += TextBoxOnGotFocusHandler;

        OnSelectedTextBoxChange();
        UpdateSelectedTime();
    }

    private void TextBoxOnGotFocusHandler(object sender, GotFocusEventArgs e) {
        _selectedTextBox = (NumericUpDown)sender;
        OnSelectedTextBoxChange();
    }

    private void OnSelectedTextBoxChange() {
        foreach (var inputBox in _inputBoxes) {
            inputBox.Classes.Remove("active");
        }

        _selectedTextBox.Classes.Add("active");

        var circleClockPicker = SetupClockPicker();

        _circleClockContainer.Content = circleClockPicker;
    }

    private CircleClockPicker SetupClockPicker() {
        var circleClockPicker = new CircleClockPicker { Minimum = 0 };
        
        if (_selectedTextBox.Name == "PART_HoursBox") {
            if (TimeFormat == TimeFormat.TwelveHour) {
                circleClockPicker.FirstLabelOverride = "12";
                circleClockPicker.Maximum = 11;
            }
            else {
                circleClockPicker.Maximum = 23;
            }
        }
        else {
            circleClockPicker.Maximum = 59;
            circleClockPicker.StepFrequency = 5;
        }

        circleClockPicker[!CircleClockPicker.ValueProperty] = _secondsTextBox.Name switch {
            "PART_HoursBox"   => this[!ClockInternals.HoursProperty],
            "PART_MinutesBox" => this[!ClockInternals.MinutesProperty],
            "PART_SecondsBox" => this[!ClockInternals.HoursProperty],
            _                     => throw new ArgumentOutOfRangeException()
        };
        
        return circleClockPicker;
    }

    private void UpdateSelectedTime() {
        _isUpdating = true;
        if (SelectedTime is null) {
            ClockInternals.SetIsAm(this, true);
            ClockInternals.SetHours(this, null);
            ClockInternals.SetMinutes(this, null);
            ClockInternals.SetSeconds(this, null);
            return;
        }

        var time = SelectedTime.Value;
        var isAm = ClockInternals.IsAm(time.Hours);
        ClockInternals.SetIsAm(this, isAm);
        ClockInternals.SetHours(this, ClockInternals.ConvertTo12FormattedHours(time.Hours));
        ClockInternals.SetMinutes(this, time.Minutes);
        ClockInternals.SetSeconds(this, time.Seconds);

        _isUpdating = false;
    }
}
public static class ClockInternals {
    static ClockInternals() {
        HoursProperty.Changed.AddClassHandler<Clock>(OnValuesChanged);
        MinutesProperty.Changed.AddClassHandler<Clock>(OnValuesChanged);
        SecondsProperty.Changed.AddClassHandler<Clock>(OnValuesChanged);
    }

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

    public static readonly AttachedProperty<int?> HoursProperty =
        AvaloniaProperty.RegisterAttached<Clock, int?>("Hours", typeof(ClockInternals));

    public static int? GetHours(Clock element) {
        return element.GetValue(HoursProperty);
    }

    public static void SetHours(Clock element, int? value) {
        element.SetValue(HoursProperty, value);
    }

    public static readonly AttachedProperty<int?> MinutesProperty =
        AvaloniaProperty.RegisterAttached<Clock, int?>("Minutes", typeof(ClockInternals));

    public static int? GetMinutes(Clock element) {
        return element.GetValue(MinutesProperty);
    }

    public static void SetMinutes(Clock element, int? value) {
        element.SetValue(MinutesProperty, value);
    }

    public static readonly AttachedProperty<int?> SecondsProperty = AvaloniaProperty.RegisterAttached<Clock, int?>("Seconds", typeof(ClockInternals));

    public static int? GetSeconds(Clock element) {
        return element.GetValue(SecondsProperty);
    }

    public static void SetSeconds(Clock element, int? value) {
        element.SetValue(SecondsProperty, value);
    }

    public static readonly AttachedProperty<bool> IsAmProperty =
        AvaloniaProperty.RegisterAttached<Clock, bool>("IsAm", typeof(ClockInternals));

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
}
