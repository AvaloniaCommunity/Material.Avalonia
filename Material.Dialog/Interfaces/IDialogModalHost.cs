namespace Material.Dialog.Interfaces {
    /// <summary>
    /// <p>This interface provides an ability to let Material.Dialog to use user-defined
    /// custom dialog host, no matter it is Window, or a random dialog modal host
    /// like DialogHost.Avalonia, user-created own dialog modal host.</p>
    /// <p>The usage of this interface can be found in wiki of Material.Avalonia.</p>
    /// </summary>
    public interface IDialogModalHost {
        bool OnDialogClosing(object? result);
        
        /// <summary>
        /// The final way of the lifecycle of a Material.Dialog modal instance. After this the host should close the window,
        /// or make the dialog disappear and dispose it (not reusable!!!).
        /// </summary>
        /// <param name="result">The dialog answer from user. It can be null due to user closes the window unexpectedly.</param>
        void OnDialogClosed(object? result);
    }
}
