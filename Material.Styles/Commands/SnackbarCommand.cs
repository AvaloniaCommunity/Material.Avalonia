using System;
using System.Windows.Input;
using Material.Styles.Controls;
using Material.Styles.Models;

namespace Material.Styles.Commands {
    /// <summary>
    /// This class used for snackbar button. 
    /// </summary>
    internal class SnackbarCommand : ICommand {
        private readonly SnackbarHost _host;
        private readonly SnackbarModel _model;

        public SnackbarCommand(SnackbarHost host, SnackbarModel model) {
            _host = host;
            _model = model;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) {
            try {
                _model.Button?.Action?.Invoke();
                _host.SnackbarModels.Remove(_model);
            }
            catch {
                // ignored
            }
        }

        public event EventHandler? CanExecuteChanged;
        protected virtual void OnCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}