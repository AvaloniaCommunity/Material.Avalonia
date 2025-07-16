namespace Material.Dialog.ViewModels.Elements;

public class TextBlockElement : DialogViewModelBase {

    private string _text = string.Empty;
    public string Text
    {
        get => _text;
        set {
            _text = value;
            OnPropertyChanged();
        }
    }
}