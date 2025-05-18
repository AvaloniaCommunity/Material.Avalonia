using System.Threading.Tasks;
using Avalonia.Controls;
using Material.Dialog.Interfaces;

namespace Material.Dialog;

public partial class DialogObject {
    private class Compat(DialogObject dialog) : IDialogWindow<IDialogResult> 
    {
        
        public Window GetWindow() {
            throw new System.NotSupportedException();
        }

        public async Task<IDialogResult> ShowDialog(Window ownerWindow) {
            var result = await dialog.ShowDialogAsync(ownerWindow);

            if (result is IDialogResult r)
                return r;
            
            return DialogResult.NoResult;
        }

        public async Task<IDialogResult> Show() {
            var taskCompletion = new TaskCompletionSource<object?>();
            var window = dialog.ShowDialogPreparePrivate();
            
            var result = await dialog.ShowCustomAsync(_ => {
                window.Show();
                return taskCompletion.Task;
            }, a => {
                window.Close();
                taskCompletion.SetResult(a);
            });
            
            if (result is IDialogResult r)
                return r;
            
            return DialogResult.NoResult;
        }

        public async Task<IDialogResult> Show(Window owner) {
            var taskCompletion = new TaskCompletionSource<object?>();
            var window = dialog.ShowDialogPreparePrivate();
            
            var result = await dialog.ShowCustomAsync(_ => {
                window.Show(owner);
                return taskCompletion.Task;
            }, a => {
                window.Close();
                taskCompletion.SetResult(a);
            });
            
            if (result is IDialogResult r)
                return r;
            
            return DialogResult.NoResult;
        }
    }
    
    public IDialogWindow<IDialogResult> GetCompatObject() {
        return new Compat (this);
    }
}