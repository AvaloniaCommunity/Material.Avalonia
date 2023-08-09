using System;
using System.Windows.Input;
using Avalonia.Threading;

namespace Material.Dialog.Commands
{
    /// <summary>
    /// Do not use it in your project! It should be used only inside of Material.Dialog.<br/>
    /// If you want to use this one, you should copy all whole code and paste them to your new RelayCommand.cs source file.
    /// </summary>
    public class MaterialDialogRelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;
        public event EventHandler CanExecuteChanged;

        public MaterialDialogRelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            var result = this.canExecute == null || this.canExecute(parameter);
            return result;
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        // Call this method to tell AvaloniaUI about this command can be executed at this moment.
        public void RaiseCanExecute()
        {
            var handler = CanExecuteChanged;

            if (handler != null)
            {
                // Call CanExecute via Dispatcher.UIThread.Post to prevent CanExecute can't be called from other thread.
                Dispatcher.UIThread.Post(delegate { handler?.Invoke(this, new EventArgs()); });
            }
        }
    }
}