using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Material.Avalonia.Demo.ViewModels;
internal class FieldsLineUpDemoViewModel : ViewModelBase {
    private string? _assistLabel = "My assist label";
    public string? AssistLabel {
        get => _assistLabel;
        set {
            if (_assistLabel != value) {
                _assistLabel = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _watermark = "My watermark";
    public string? Watermark {
        get => _watermark;
        set {
            if (_watermark != value) {
                _watermark = value;
                OnPropertyChanged();
            }
        }
    }



    public IList<string> ComboBoxItems { get; } = [
        "Item 1",
        "Item 2",
        "Item 3",
        "Item 4",
        "Item 5"
    ];
}
