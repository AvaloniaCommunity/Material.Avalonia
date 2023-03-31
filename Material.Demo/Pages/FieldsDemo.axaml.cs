using Avalonia.Controls;
using Material.Demo.ViewModels;

namespace Material.Demo.Pages {
    public partial class FieldsDemo : UserControl {
        public FieldsDemo() {
            this.InitializeComponent();

            DataContext = new TextFieldsViewModel();
        }
    }
}
