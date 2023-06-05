using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Material.Dialog.Icons {
    public class DialogIcon : TemplatedControl {
        public static readonly StyledProperty<DialogIconKind> KindProperty
            = AvaloniaProperty.Register<DialogIcon, DialogIconKind>(nameof(Kind));

        public static readonly StyledProperty<StreamGeometry> DataProperty
            = AvaloniaProperty.Register<DialogIcon, StreamGeometry>(nameof(Data));

        public static readonly StyledProperty<bool> UseRecommendColorProperty
            = AvaloniaProperty.Register<DialogIcon, bool>(nameof(UseRecommendColor), true);
        static DialogIcon() {
            KindProperty.Changed.AddClassHandler<DialogIcon>(KindPropertyChangedCallback);
            UseRecommendColorProperty.Changed.AddClassHandler<DialogIcon>(UseRecommendColorPropertyChangedCallback);
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public DialogIconKind Kind {
            get => GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary> 
        public StreamGeometry Data {
            get => GetValue(DataProperty);
            private set => SetValue(DataProperty, value);
        }

        public bool UseRecommendColor {
            get => GetValue(UseRecommendColorProperty);
            set => SetValue(UseRecommendColorProperty, value);
        }

        private static void KindPropertyChangedCallback(DialogIcon dialogIcon, AvaloniaPropertyChangedEventArgs avaloniaPropertyChangedEventArgs) {
            dialogIcon.UpdateData();
            dialogIcon.UpdateColor();
        }

        private static void UseRecommendColorPropertyChangedCallback(DialogIcon dialogIcon, AvaloniaPropertyChangedEventArgs avaloniaPropertyChangedEventArgs) {
            dialogIcon.UpdateColor();
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
            base.OnApplyTemplate(e);
            UpdateData();
        }

        private void UpdateData() {
            string data = null;
            DialogIconsDataFactory.DataIndex.Value?.TryGetValue(Kind, out data);
            var g = StreamGeometry.Parse(data);
            this.Data = g;
        }

        private void UpdateColor() {
            if (UseRecommendColor) {
                string color = null;
                DialogIconsDataFactory.RecommendColorIndex.Value?.TryGetValue(Kind, out color);
                Foreground = SolidColorBrush.Parse(color);
            }
        }
    }
}
