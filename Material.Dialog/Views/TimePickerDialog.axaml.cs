using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.Dialog.Views
{
    public class TimePickerDialog : Window
    {
        public TimePickerDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
