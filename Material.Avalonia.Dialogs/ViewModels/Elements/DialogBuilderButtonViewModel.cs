using System;

namespace Material.Dialog.ViewModels.Elements;

public class DialogBuilderButtonViewModel : DialogViewModelBase {
    public object? Content { get; set; }
    public object ReturnValue { get; set; }
    public Func<object, bool>? ShouldClose { get; set; }

    internal Action<object>? _closeCommandSourceInternal;

    public void ExecuteCommand(object args) 
    {
        if(args is not DialogBuilderButtonViewModel vm)
            return;
        
        if(!ShouldClose?.Invoke(vm.ReturnValue) ?? false)
            return;
        
        _closeCommandSourceInternal!.Invoke(vm.ReturnValue);
    }
}