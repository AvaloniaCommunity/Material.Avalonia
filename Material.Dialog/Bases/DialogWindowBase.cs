using Avalonia.Controls;
using Material.Dialog.Interfaces;
using System; 
using System.Threading.Tasks;
using Material.Styles.Assists;

namespace Material.Dialog.Bases
{
    internal class DialogWindowBase<TWindow, TResult> : IDialogWindow<TResult> where TWindow : Window, IDialogWindowResult<TResult>
    {
        private TWindow m_Window;
        public DialogWindowBase(TWindow window)
        {
            m_Window = window;
        }

        /// <summary>
        /// Get window content. It most used for show dialog from other places.
        /// </summary>
        /// <returns>The content of dialog window.</returns>
        public object GetContent() => m_Window.Content;

        /// <summary>
        /// Get window itself.
        /// </summary>
        /// <returns>The window.</returns>
        public Window GetWindow() => m_Window;
        
        /// <summary>
        /// Shows the window.
        /// </summary>
        /// <returns>Result of dialog.</returns>
        public Task<TResult> Show() => Procedure(delegate { m_Window.Show(); });

        /// <summary>
        /// Shows the window as a child of parent window.
        /// </summary>
        /// <param name="window">Window that will be a parent of the shown window.</param>
        /// <returns>Result of dialog.</returns>
        public Task<TResult> Show(Window window) => Procedure(delegate { m_Window.Show(window); });

        /// <summary>
        /// Shows the window as modal dialog.
        /// </summary>
        /// <param name="ownerWindow">The dialog's owner window.</param>
        /// <returns>Result of dialog.</returns>
        public Task<TResult> ShowDialog(Window ownerWindow) => Procedure(delegate { m_Window.ShowDialog(ownerWindow); });

        private Task<TResult> Procedure(Action action)
        {
            var tcs = new TaskCompletionSource<TResult>();

            void OnceHandler (object sender, EventArgs args)
            {
                tcs.TrySetResult(m_Window.GetResult());
                m_Window.Closed -= OnceHandler;
            }

            m_Window.Closed += OnceHandler;
            TransitionAssist.SetDisableTransitions(m_Window as AvaloniaObject, DialogHelper.DisableTransitions);
            action();
            return tcs.Task;
        }
    }
}
