using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;

namespace MaterialXamlToolKit.Avalonia.Assists {
    /// <summary>
    /// Contains attached properties for <code>TabControl</code>.
    /// </summary>
    public static class TabControlAssist {
        /// <summary>
        /// The alignment of the horizontal tab headers in the <code>TabControl</code>.
        /// </summary>
        public static readonly AttachedProperty<HorizontalAlignment> TabHeaderHorizontalAlignmentProperty =
            AvaloniaProperty.RegisterAttached<TabControl, HorizontalAlignment>(
                "TabHeaderHorizontalAlignment", typeof(TabControlAssist), HorizontalAlignment.Left, true
            );

        /// <summary>
        /// Gets the alignment of the horizontal tab headers in the <code>TabControl</code>.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static HorizontalAlignment GetTabHeaderHorizontalAlignment(AvaloniaObject element) {
            return (HorizontalAlignment) element.GetValue(TabHeaderHorizontalAlignmentProperty);
        }

        /// <summary>
        /// Sets the alignment of the horizontal tab headers in the <code>TabControl</code>.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderHorizontalAlignment(AvaloniaObject element, HorizontalAlignment value) {
            element.SetValue(TabHeaderHorizontalAlignmentProperty, value);
        }

        /// <summary>
        /// The alignment of the vertical tab headers in the <code>TabControl</code>.
        /// </summary>
        public static readonly AttachedProperty<VerticalAlignment> TabHeaderVerticalAlignmentProperty =
            AvaloniaProperty.RegisterAttached<TabControl, VerticalAlignment>(
                "TabHeaderVerticalAlignment", typeof(TabControlAssist), VerticalAlignment.Top, true
            );

        /// <summary>
        /// Gets the alignment of the vertical tab headers in the <code>TabControl</code>.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static VerticalAlignment GetTabHeaderVerticalAlignment(AvaloniaObject element) {
            return (VerticalAlignment) element.GetValue(TabHeaderVerticalAlignmentProperty);
        }

        /// <summary>
        /// Sets the alignment of the vertical tab headers in the <code>TabControl</code>.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderVerticalAlignment(AvaloniaObject element, VerticalAlignment value) {
            element.SetValue(TabHeaderVerticalAlignmentProperty, value);
        }

        /// <summary>
        /// The brush for not selected tab headers.
        /// </summary>
        public static readonly AvaloniaProperty<Brush> TabHeaderInactiveBrushProperty = AvaloniaProperty.RegisterAttached<TabControl, Brush>(
            "TabHeaderInactiveBrush", typeof(TabControlAssist), null, true
        );

        /// <summary>
        /// Gets the brush for not selected tab headers.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetTabHeaderInactiveBrush(AvaloniaObject element) {
            return (Brush) element.GetValue(TabHeaderInactiveBrushProperty);
        }

        /// <summary>
        /// Sets the brush for not selected tab headers.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderInactiveBrush(AvaloniaObject element, Brush value) {
            element.SetValue(TabHeaderInactiveBrushProperty, value);
        }

        /// <summary>
        /// The opacity for not selected tab headers.
        /// </summary>
        public static readonly AvaloniaProperty<double> TabHeaderInactiveOpacityProperty = AvaloniaProperty.RegisterAttached<TabControl, double>(
            "TabHeaderInactiveOpacity", typeof(TabControlAssist), 1, true
        );

        /// <summary>
        /// Gets the opacity for not selected tab headers.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetTabHeaderInactiveOpacity(AvaloniaObject element) {
            return (double) element.GetValue(TabHeaderInactiveOpacityProperty);
        }

        /// <summary>
        /// Sets the ppacity for not selected tab headers.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderInactiveOpacity(AvaloniaObject element, double value) {
            element.SetValue(TabHeaderInactiveOpacityProperty, value);
        }

        /// <summary>
        /// The highlight color of the selected tab item header.
        /// </summary>
        public static readonly AvaloniaProperty<Brush> TabHeaderHighlightBrushProperty = AvaloniaProperty.RegisterAttached<TabControl, Brush>(
            "TabHeaderHighlightBrush", typeof(TabControlAssist), null, true
        );

        /// <summary>
        /// Gets the highlight color of the selected tab item header.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetTabHeaderHighlightBrush(AvaloniaObject element) {
            return (Brush) element.GetValue(TabHeaderHighlightBrushProperty);
        }

        /// <summary>
        /// Sets the highlight color of the selected tab item header.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderHighlightBrush(AvaloniaObject element, Brush value) {
            element.SetValue(TabHeaderHighlightBrushProperty, value);
        }

        /// <summary>
        /// The current color of the tab item header. Intended to be read-only.
        /// </summary>
        public static readonly AvaloniaProperty<Brush> TabHeaderForegroundProperty = AvaloniaProperty.RegisterAttached<TabItem, Brush>(
            "TabHeaderForeground", typeof(TabControlAssist), null, true
        );

        /// <summary>
        /// Gets the current color of the tab item header.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetTabHeaderForeground(AvaloniaObject element) {
            return (Brush) element.GetValue(TabHeaderForegroundProperty);
        }

        /// <summary>
        /// Sets the current color of the tab item header.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTabHeaderForeground(AvaloniaObject element, Brush value) {
            element.SetValue(TabHeaderForegroundProperty, value);
        }
    }
}