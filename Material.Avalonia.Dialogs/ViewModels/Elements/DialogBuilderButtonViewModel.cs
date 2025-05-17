using System;

namespace Material.Dialog.ViewModels.Elements;

public class DialogBuilderButtonViewModel : DialogViewModelBase {
    public object? Content { get; set; }
    public object ReturnValue { get; set; }
    public Func<bool>? ShouldClose { get; set; }
}