using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;

namespace Material.Demo {
    [PseudoClasses("selected")]
    public class SelectionWrapper : UserControl {
        static SelectionWrapper() {
            PointerPressedEvent.Raised.Subscribe(tuple => {
                if (tuple.Item1 is SelectionWrapper selectionWrapper) {
                    selectionWrapper.CurrentSelected = selectionWrapper.DataSource;
                }
            });

            CurrentSelectedProperty.Changed.Subscribe(args => {
                if (args.Sender is SelectionWrapper selectionWrapper) {
                    selectionWrapper.UpdateSelectedNow();
                }
            });

            SelectedNowProperty.Changed.Subscribe(args => {
                if (args.Sender is SelectionWrapper selectionWrapper) {
                    if (args.NewValue.Value) {
                        selectionWrapper.PseudoClasses.Add(":selected");
                    }
                    else {
                        selectionWrapper.PseudoClasses.Remove(":selected");
                    }
                }
            });
        }

        protected override void OnDataContextEndUpdate() {
            base.OnDataContextEndUpdate();
            UpdateSelectedNow();
        }

        public void UpdateSelectedNow() {
            SelectedNow = DataSource == CurrentSelected;
        }

        public static readonly StyledProperty<object> DataSourceProperty =
            AvaloniaProperty.Register<SelectionWrapper, object>(nameof(DataSource));

        public object DataSource {
            get => GetValue(DataSourceProperty);
            set => SetValue(DataSourceProperty, value);
        }

        public static readonly StyledProperty<object> CurrentSelectedProperty =
            AvaloniaProperty.Register<SelectionWrapper, object>(nameof(CurrentSelected));

        public object CurrentSelected {
            get => GetValue(CurrentSelectedProperty);
            set => SetValue(CurrentSelectedProperty, value);
        }

        public static readonly DirectProperty<SelectionWrapper, bool> SelectedNowProperty =
            AvaloniaProperty.RegisterDirect<SelectionWrapper, bool>(
                nameof(SelectedNow),
                wrapper => wrapper.CurrentSelected == wrapper.DataSource);

        private bool _selectedNow;

        public bool SelectedNow {
            get => _selectedNow;
            private set => SetAndRaise(SelectedNowProperty, ref _selectedNow, value);
        }
    }
}
