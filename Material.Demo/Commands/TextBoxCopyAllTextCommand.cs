using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input.Platform;

namespace Material.Demo.Commands {
    public class TextBoxCopyAllTextCommand : ICommand {
        public bool CanExecute(object? parameter) {
            return parameter is TextBox;
        }

        public void Execute(object? parameter) {
            if (parameter is not TextBox textBox)
                return;

            AvaloniaLocator.Current.GetService<IClipboard>()?.SetTextAsync(textBox.Text);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
