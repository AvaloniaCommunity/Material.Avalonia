using Avalonia.Controls;
using Material.Dialog.Interfaces;
using System; 
using System.Threading.Tasks;

namespace Material.Dialog.Bases
{
    internal class DialogWindowBase<TWindow, TResult> : IDialogWindow<TResult> where TWindow : Window, IDialogWindowResult<TResult>
    {
        private TWindow m_Window;
        public DialogWindowBase(TWindow window)
        {
            m_Window = window;
        }
        
        public Task<TResult> Show() => Procedure(() => m_Window.Show());

        public Task<TResult> Show(Window window) => Procedure(() => m_Window.Show(window));

        public Task<TResult> ShowDialog(Window ownerWindow) => Procedure(() => m_Window.ShowDialog(ownerWindow));

        private Task<TResult> Procedure(Action action)
        {
            var tcs = new TaskCompletionSource<TResult>();

            void OnceHandler (object sender, EventArgs args)
            {
                tcs.TrySetResult(m_Window.GetResult());
                m_Window.Closed -= OnceHandler;
            }

            m_Window.Closed += OnceHandler;
            action();
            return tcs.Task;
        }
    }
}
