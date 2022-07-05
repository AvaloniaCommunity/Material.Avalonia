﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Material.Styles.Assists
{
    /// <summary>
    ///     Contains attached properties for <code>TabControl</code>.
    /// </summary>
    public static class TabControlAssist
    {
        /// <summary>
        ///     The alignment of the horizontal tab headers in the <code>TabControl</code>.
        /// </summary>
        public static readonly AttachedProperty<HorizontalAlignment> TabHeaderHorizontalAlignmentProperty =
            AvaloniaProperty.RegisterAttached<TabControl, HorizontalAlignment>(
                "TabHeaderHorizontalAlignment", typeof(TabControlAssist), HorizontalAlignment.Left, true
            );

        /// <summary>
        ///     The alignment of the vertical tab headers in the <code>TabControl</code>.
        /// </summary>
        public static readonly AttachedProperty<VerticalAlignment> TabHeaderVerticalAlignmentProperty =
            AvaloniaProperty.RegisterAttached<TabControl, VerticalAlignment>(
                "TabHeaderVerticalAlignment", typeof(TabControlAssist), VerticalAlignment.Top, true
            );

        /// <summary>
        ///     The brush for not selected tab headers.
        /// </summary>
        public static readonly AvaloniaProperty<IBrush?> TabHeaderInactiveBrushProperty =
            AvaloniaProperty.RegisterAttached<TabControl, IBrush?>(
                "TabHeaderInactiveBrush", typeof(TabControlAssist), null, true
            );

        /// <summary>
        ///     The opacity for not selected tab headers.
        /// </summary>
        public static readonly AvaloniaProperty<double> TabHeaderInactiveOpacityProperty =
            AvaloniaProperty.RegisterAttached<TabControl, double>(
                "TabHeaderInactiveOpacity", typeof(TabControlAssist), 1, true
            );

        /// <summary>
        ///     The highlight color of the selected tab item header.
        /// </summary>
        public static readonly AvaloniaProperty<IBrush?> TabHeaderHighlightBrushProperty =
            AvaloniaProperty.RegisterAttached<TabControl, IBrush?>(
                "TabHeaderHighlightBrush", typeof(TabControlAssist), null, true
            );

        /// <summary>
        ///     The current color of the tab item header. Intended to be read-only.
        /// </summary>
        public static readonly AvaloniaProperty<IBrush?> TabHeaderForegroundProperty =
            AvaloniaProperty.RegisterAttached<TabItem, IBrush?>(
                "TabHeaderForeground", typeof(TabControlAssist), null, true
            );

        /// <summary>
        ///     Gets the alignment of the horizontal tab headers in the <code>TabControl</code>.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static HorizontalAlignment GetTabHeaderHorizontalAlignment(AvaloniaObject element) =>
            element.GetValue(TabHeaderHorizontalAlignmentProperty);

        /// <summary>
        ///     Sets the alignment of the horizontal tab headers in the <code>TabControl</code>.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderHorizontalAlignment(AvaloniaObject element, HorizontalAlignment value) =>
            element.SetValue(TabHeaderHorizontalAlignmentProperty, value);

        /// <summary>
        ///     Gets the alignment of the vertical tab headers in the <code>TabControl</code>.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static VerticalAlignment GetTabHeaderVerticalAlignment(AvaloniaObject element) =>
            element.GetValue(TabHeaderVerticalAlignmentProperty);

        /// <summary>
        ///     Sets the alignment of the vertical tab headers in the <code>TabControl</code>.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderVerticalAlignment(AvaloniaObject element, VerticalAlignment value) =>
            element.SetValue(TabHeaderVerticalAlignmentProperty, value);

        /// <summary>
        ///     Gets the brush for not selected tab headers.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush? GetTabHeaderInactiveBrush(AvaloniaObject element) =>
            element.GetValue<IBrush?>(TabHeaderInactiveBrushProperty);

        /// <summary>
        ///     Sets the brush for not selected tab headers.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderInactiveBrush(AvaloniaObject element, IBrush? value) =>
            element.SetValue(TabHeaderInactiveBrushProperty, value);

        /// <summary>
        ///     Gets the opacity for not selected tab headers.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetTabHeaderInactiveOpacity(AvaloniaObject element) =>
            element.GetValue<double>(TabHeaderInactiveOpacityProperty);

        /// <summary>
        ///     Sets the opacity for not selected tab headers.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderInactiveOpacity(AvaloniaObject element, double value) =>
            element.SetValue(TabHeaderInactiveOpacityProperty, value);

        /// <summary>
        ///     Gets the highlight color of the selected tab item header.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush? GetTabHeaderHighlightBrush(AvaloniaObject element) =>
            element.GetValue<IBrush?>(TabHeaderHighlightBrushProperty);

        /// <summary>
        ///     Sets the highlight color of the selected tab item header.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderHighlightBrush(AvaloniaObject element, IBrush? value) =>
            element.SetValue(TabHeaderHighlightBrushProperty, value);

        /// <summary>
        ///     Gets the current color of the tab item header.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IBrush? GetTabHeaderForeground(AvaloniaObject element) =>
            element.GetValue<IBrush?>(TabHeaderForegroundProperty);

        /// <summary>
        ///     Sets the current color of the tab item header.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderForeground(AvaloniaObject element, IBrush? value) =>
            element.SetValue(TabHeaderForegroundProperty, value);
    }
}