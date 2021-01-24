using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Material.Styles
{
    public class PackIcon : TemplatedControl
    {  
        static PackIcon()
        { 
        }

        public static readonly StyledProperty<PackIconKind> KindProperty
            = AvaloniaProperty.Register<PackIcon, PackIconKind>(nameof(Kind), notifying: KindPropertyChangedCallback);

        private static void KindPropertyChangedCallback(IAvaloniaObject sender, bool before)
        {
            ((PackIcon)sender).UpdateData();
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public PackIconKind Kind
        {
            get => GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        public static readonly StyledProperty<StreamGeometry> DataProperty
            = AvaloniaProperty.Register<PackIcon, StreamGeometry>(nameof(Data));

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary> 
        public StreamGeometry Data
        {
            get => GetValue(DataProperty);
            private set => SetValue(DataProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateData();
        } 

        private void UpdateData()
        {
            string data = null; 
            PackIconDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
            var g = StreamGeometry.Parse(data);
            this.Data = g;
        }
    }
}
