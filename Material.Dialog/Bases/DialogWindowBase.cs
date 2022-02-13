using Avalonia.Controls;
using Material.Dialog.Interfaces;
using System; 
using System.Threading.Tasks;
using Material.Styles.Assists;

namespace Material.Dialog.Bases
{
    internal class DialogWindowBase<TWindow, TResult> : IDialogWindow<TResult> where TWindow : Window, IDialogWindowResult<TResult>
    {
        private readonly TWindow _window;
        public DialogWindowBase(TWindow window)
        {
            _window = window;
        }

        /// <summary>
        /// Get window content. It most used for show dialog from other places.
        /// </summary>
        /// <returns>The content of dialog window.</returns>
        public object GetContent() => _window.Content;

        /// <summary>
        /// Get window itself.
        /// </summary>
        /// <returns>The window.</returns>
        public Window GetWindow() => _window;
        
        /// <summary>
        /// Shows the window.
        /// </summary>
        /// <returns>Result of dialog.</returns>
        public Task<TResult> Show() => Procedure(delegate { _window.Show(); });

        /// <summary>
        /// Shows the window as a child of parent window.
        /// </summary>
        /// <param name="window">Window that will be a parent of the shown window.</param>
        /// <returns>Result of dialog.</returns>
        public Task<TResult> Show(Window window) => Procedure(delegate { _window.Show(window); });

        /// <summary>
        /// Shows the window as modal dialog.
        /// </summary>
        /// <param name="ownerWindow">The dialog's owner window.</param>
        /// <returns>Result of dialog.</returns>
        public Task<TResult> ShowDialog(Window ownerWindow) => Procedure(delegate
        {
            _window.ShowDialog(ownerWindow);
        });

        private Task<TResult> Procedure(Action action)
        {
            var tcs = new TaskCompletionSource<TResult>();

            void OnceHandler (object sender, EventArgs args)
            {
                tcs.TrySetResult(_window.GetResult());
                _window.Closed -= OnceHandler;
            }

            _window.Closed += OnceHandler;
            TransitionAssist.SetDisableTransitions(_window, DialogHelper.DisableTransitions);
            action();
            return tcs.Task;
        }
    }
}
