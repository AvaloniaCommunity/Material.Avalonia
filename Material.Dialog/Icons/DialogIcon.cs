﻿using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Material.Dialog.Icons
{
    public class DialogIcon : TemplatedControl
    {
        static DialogIcon()
        {
        }

        public static readonly StyledProperty<DialogIconKind> KindProperty
            = AvaloniaProperty.Register<DialogIcon, DialogIconKind>(nameof(Kind),
                notifying: KindPropertyChangedCallback);

        private static void KindPropertyChangedCallback(IAvaloniaObject sender, bool before)
        {
            ((DialogIcon) sender).UpdateData();
            ((DialogIcon) sender).UpdateColor();
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public DialogIconKind Kind
        {
            get => GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        public static readonly StyledProperty<StreamGeometry> DataProperty
            = AvaloniaProperty.Register<DialogIcon, StreamGeometry>(nameof(Data));

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary> 
        public StreamGeometry Data
        {
            get => GetValue(DataProperty);
            private set => SetValue(DataProperty, value);
        }

        public static readonly StyledProperty<bool> UseRecommendColorProperty
            = AvaloniaProperty.Register<DialogIcon, bool>(nameof(UseRecommendColor), true,
                notifying: UseRecommendColorPropertyChangedCallback);

        public bool UseRecommendColor
        {
            get => GetValue(UseRecommendColorProperty);
            set => SetValue(UseRecommendColorProperty, value);
        }

        private static void UseRecommendColorPropertyChangedCallback(IAvaloniaObject sender, bool before)
        {
            ((DialogIcon) sender).UpdateColor();
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateData();
        }

        private void UpdateData()
        {
            string data = null;
            DialogIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
            var g = StreamGeometry.Parse(data);
            this.Data = g;
        }

        private void UpdateColor()
        {
            if (UseRecommendColor == true)
            {
                string color = null;
                DialogIconsDataFactory.RecommendColorIndex.Value?.TryGetValue(Kind, out color);
                Foreground = SolidColorBrush.Parse(color);
            }
        }
    }
}