using Material.Dialog.ViewModels.Elements.Header.Icons;

namespace Material.Dialog.ViewModels.Elements.Header;

public class DialogHeaderViewModel : DialogViewModelBase {
    public IconViewModelBase? Icon {
        get => _icon;
        set {
            _icon = value;
            OnPropertyChanged();
        }
    }

    public object? Header {
        get => _header;
        set {
            _header = value;
            OnPropertyChanged();
        }
    }

    private IconViewModelBase? _icon;
    private object? _header;
}