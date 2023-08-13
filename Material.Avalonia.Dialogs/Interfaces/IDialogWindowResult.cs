// This is clone of code IMsBoxWindow<T>
// https://github.com/AvaloniaUtils/MessageBox.Avalonia/blob/master/src/MessageBox.Avalonia/BaseWindows/Base/IWindowGetResult.cs

namespace Material.Dialog.Interfaces
{
    public interface IDialogWindowResult<T>
    {
        T GetResult();
    }
}