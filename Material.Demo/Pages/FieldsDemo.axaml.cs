using Avalonia.Controls;
using Material.Demo.ViewModels;

namespace Material.Demo.Pages {
    public partial class FieldsDemo : UserControl {
        public FieldsDemo() {
            InitializeComponent();

            DataContext = new TextFieldsViewModel();
        }
    }
}
