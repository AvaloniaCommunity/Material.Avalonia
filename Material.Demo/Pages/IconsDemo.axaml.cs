#nullable enable
using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Material.Demo.ViewModels;

namespace Material.Demo.Pages {
    public class IconsDemo : UserControl {
        public IconsDemo() {
            InitializeComponent();
            
            DataContext = new IconsDemoViewModel();
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Search_OnKeyDown(object? sender, KeyEventArgs e) {
            var textBox = (TextBox)sender!;
            if (e.Key == Key.Enter)
                this.Get<Button>("SearchButton").Command.Execute(textBox.Text);
        }

        private void TextBox_OnGotFocus(object? sender, GotFocusEventArgs e) {
            var textBox = (TextBox)sender!;
            Dispatcher.UIThread.Post(textBox.SelectAll);
        }
    }
}