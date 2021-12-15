using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Material.Icons.Avalonia;
using Material.Styles.Internal;

namespace Material.Styles.Controls
{
    public class MaterialInternalIcon : TemplatedControl
    {
        private static readonly Lazy<IDictionary<string, string>> _dataSetInstance = new(IconsDataSet.CreateDataSet);
        
        static MaterialInternalIcon() {
            KindProperty.Changed.Subscribe(args => (args.Sender as MaterialInternalIcon)?.UpdateData());
        }

        public static readonly AvaloniaProperty<string> KindProperty = AvaloniaProperty.Register<MaterialIcon, string>(nameof(Kind));

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public string Kind {
            get => (string) GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        private static readonly AvaloniaProperty<Geometry?>
            DataProperty = AvaloniaProperty.RegisterDirect<MaterialInternalIcon, Geometry?>(nameof(Data), icon => icon.Data);

        private Geometry? _data;

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary>
        public Geometry? Data {
            get
            {
                _data = _data switch
                {
                    null => Geometry.Parse(IconsDataSet.UnknownIconData),
                    _ => _data
                };
                return _data;
            }
            private set => SetAndRaise(DataProperty, ref _data, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
            base.OnApplyTemplate(e);
            UpdateData();
        }

        private void UpdateData()
        {
            if (Kind is null)
                return;

            string data;
            
            if (_dataSetInstance.Value?.TryGetValue(Kind, out data) ?? false)
                Data = Geometry.Parse(data);
            else
                Data = null;
        }
    }
}