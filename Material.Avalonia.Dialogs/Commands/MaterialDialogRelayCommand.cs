using System;
using System.Windows.Input;
using Avalonia.Threading;

namespace Material.Dialog.Commands {
    /// <summary>
    /// Do not use it in your project! It should be used only inside of Material.Avalonia.Dialogs.<br/>
    /// If you want to use this one, you should copy all whole code and paste them to your new RelayCommand.cs source file.
    /// </summary>
    public class MaterialDialogRelayCommand : ICommand {
        private readonly Func<object?, bool>? canExecute;
        private readonly Action<object?> execute;

        public MaterialDialogRelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter) {
            execute(parameter);
        }

        // Call this method to tell AvaloniaUI about this command can be executed at this moment.
        public void RaiseCanExecute() {
            var handler = CanExecuteChanged;

            if (handler != null) {
                // Call CanExecute via Dispatcher.UIThread.Post to prevent CanExecute can't be called from other thread.
                Dispatcher.UIThread.Post(delegate { handler?.Invoke(this, EventArgs.Empty); });
            }
        }
    }
}