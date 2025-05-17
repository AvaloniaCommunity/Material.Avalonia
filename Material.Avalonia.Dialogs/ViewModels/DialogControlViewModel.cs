using Avalonia.Collections;
using Material.Dialog.Collections;
using Material.Dialog.ViewModels.Elements;
using Material.Dialog.ViewModels.Elements.Header;

namespace Material.Dialog.ViewModels;

public class DialogControlViewModel : DialogViewModelBase {
    public DialogHeaderViewModel? DialogHeader { get; set; }

    public AvaloniaList<object> Views { get; set; } = [];

    internal BlockingConcurrentQueue<object> State { get; } = new();

    public AvaloniaList<DialogBuilderButtonViewModel>? Answers { get; set; }

    public AvaloniaList<DialogBuilderButtonViewModel>? AssistantButtons { get; set; }
}