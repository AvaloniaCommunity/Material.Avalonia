using System.ComponentModel;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Material.Demo.ViewModels;

namespace Material.Demo.Pages {
    public partial class ProgressIndicatorDemo : UserControl {
        public ProgressIndicatorDemo() {
            InitializeComponent();

            DataContext = new ProgressIndicatorDemoViewModel();
        }
    }
}