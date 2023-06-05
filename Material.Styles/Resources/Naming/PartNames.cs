namespace Material.Styles.Resources.Naming {
    /// <summary>
    /// Constant naming collections of templated elements in control. 
    /// </summary>
    public static class PartNames {
        #region Common use, including effects

        public static string PartRootBorder => "PART_RootBorder";

        public static string PartRootPanel => "PART_RootPanel";

        public static string PartContentPresenter => "PART_ContentPresenter";

        public static string PartIconPresenter => "PART_IconPresenter";

        public static string PartHeaderPresenter => "PART_HeaderPresenter";

        public static string PartItemsPresenter => "PART_ItemsPresenter";

        public static string PartInnerPanel => "PART_InnerPanel";

        public static string PartContentPanel => "PART_ContentPanel";

        public static string PartInnerBorder => "PART_InnerBorder";

        /// <summary>
        /// General use.
        /// Please use this name on templated border of controls that have feedback on hovered by cursor
        /// </summary>
        public static string PartHoverEffect => "PART_HoverEffect";

        public static string PartScrollViewer => "PART_ScrollViewer";

        /// <summary>
        /// General use.
        /// Please use this name on templated border of controls that have behaviours (Selected / Clicked behaviour or etc.)
        /// </summary>
        public static string PartBehaviourEffect => "PART_BehaviourEffect";

        /// <summary>
        /// Use this name on Ripple layer control,
        /// which gives visual feedback to user after click control like <see cref="Avalonia.Controls.Button"/>.
        /// </summary>
        public static string PartRipple => "PART_Ripple";

        #endregion

        #region For TextBox, ComboBoxes or etc.

        public static string PartPlaceholderText => "PART_PlaceholderText";

        public static string PartTextPresenter => "PART_TextPresenter";

        public static string PartDataValidation => "PART_DataValidation";

        public static string PartHintsText => "PART_HintsText";

        public static string PartUnderline => "PART_Underline";

        public static string PartLabelText => "PART_LabelText";

        public static string PartLabelRootBorder => "PART_LabelRootBorder";

        public static string PartErrorPresenter => "PART_ErrorPresenter";

        #endregion

        #region For Slider, ScrollBar

        public static string PartProgressLayer => "PART_ProgressLayer";

        public static string PartSliderTick => "PART_SliderTick";

        /// <summary>
        /// <p>This name is used for AvaloniaUI integration (Required).</p>
        /// Use this name on track of slider.
        /// <b>You should do that otherwise your application will crash after click slider.</b>
        /// </summary>
        public static string PartTrack => "PART_Track";

        public static string PartTrackBorderBar => "PART_TrackBorderBar";

        public static string PartThumb => "PART_Thumb";

        public static string PartSliderThumb => "PART_SliderThumb";

        /// <summary>
        /// Use this name on RepeatButton of track of scrollbar to scroll up (decrease steps view).
        /// </summary>
        public static string PartLineUpButton => "PART_LineUpButton";

        /// <summary>
        /// Use this name on RepeatButton of track of scrollbar to scroll down (increase steps view).
        /// </summary>
        public static string PartLineDownButton => "PART_LineDownButton";

        public static string PartPageUpButton => "PART_PageUpButton";

        public static string PartPageDownButton => "PART_PageDownButton";

        /// <summary>
        /// <p>This name is used for AvaloniaUI integration.</p>
        /// Use this name on RepeatButton of track of slider to get a clickable decrease value track.
        /// </summary>
        public static string PartDecreaseButton => "PART_DecreaseButton";

        /// <summary>
        /// <p>This name is used for AvaloniaUI integration.</p>
        /// Use this name on RepeatButton of track of slider to get a clickable increase value track.
        /// </summary>
        public static string PartIncreaseButton => "PART_IncreaseButton";

        public static string PartThumbGrip => "PART_ThumbGrip";

        #endregion

        #region For ToggleSwitch

        public static string PartSwitchBorder => "PART_SwitchBorder";

        public static string PartSwitchKnobBorder => "PART_SwitchKnobBorder";

        public static string PartKnobOnContentPresenter => "PART_KnobOnContentPresenter";

        public static string PartKnobOffContentPresenter => "PART_KnobOffContentPresenter";

        /// <summary>
        /// <p>This name is used for AvaloniaUI integration.</p>
        /// Use this name on canvas of ToggleSwitch to get a zone for draggable knob
        /// </summary>
        public static string AvaloniaSwitchKnob => "PART_SwitchKnob";

        /// <summary>
        /// <p>This name is used for AvaloniaUI integration.</p>
        /// Use this name on panel under of canvas of ToggleSwitch to get a draggable knob
        /// </summary>
        public static string AvaloniaMovingKnobs => "PART_MovingKnobs";

        #endregion

        #region For ProgressBar

        /// <summary>
        /// <p>This name is used for AvaloniaUI integration (Required).</p>
        /// Use this name on border to make ProgressBar recognize that border as active indicator.
        /// <b>Otherwise ProgressBar will unable to work correctly.</b>
        /// </summary>
        public static string AvaloniaProgressBarIndicator => "PART_Indicator";

        public static string PartInactiveState => "PART_InactiveState";

        public static string PartActiveState => "PART_ActiveState";

        #endregion

        #region For DatePicker

        /// <summary>
        /// <p>This name is used for AvaloniaUI integration.</p>
        /// Use this name on TextBlock for displaying day text by avalonia
        /// </summary>
        public static string AvaloniaDayText = "PART_DayTextBlock";

        /// <summary>
        /// <p>This name is used for AvaloniaUI integration.</p>
        /// Use this name on TextBlock for displaying month text by avalonia
        /// </summary>
        public static string AvaloniaMonthText = "PART_MonthTextBlock";

        /// <summary>
        /// <p>This name is used for AvaloniaUI integration.</p>
        /// Use this name on TextBlock for displaying year text by avalonia
        /// </summary>
        public static string AvaloniaYearText = "PART_YearTextBlock";

        #endregion

        public static string PartInputGestureText => "PART_InputGestureText";

        public static string PartVisualLayer => "VisualLayer";

        public static string PartExpanderButton => "PART_ExpanderButton";

        public static string PartPopup => "PART_Popup";

        public static string PartCard => "PART_Card";

        /// <summary>
        /// Use this name on container pane of tabs, which wrapped ItemsPresenter by pane of TabControl template.
        /// </summary>
        public static string PartTabsPanel => "PART_TabsPanel";

        /// <summary>
        /// Use this name on primary separator of any control template.
        /// </summary>
        public static string PartSeparator => "PART_Separator";
    }
}
