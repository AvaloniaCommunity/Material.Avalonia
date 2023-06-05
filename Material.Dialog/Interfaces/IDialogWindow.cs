﻿// This is clone of code IMsBoxWindow<T>
// https://github.com/AvaloniaUtils/MessageBox.Avalonia/blob/master/src/MessageBox.Avalonia/BaseWindows/Base/IMsBoxWindow.cs

using System.Threading.Tasks;
using Avalonia.Controls;

namespace Material.Dialog.Interfaces
{
    public interface IDialogWindow<T>
    {
        Window GetWindow();

        Task<T> ShowDialog(Window ownerWindow);
        Task<T> Show();
        Task<T> Show(Window window);
    }
}