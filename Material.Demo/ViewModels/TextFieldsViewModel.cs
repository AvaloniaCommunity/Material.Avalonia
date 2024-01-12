using System.Text.RegularExpressions;
using Avalonia.Data;

namespace Material.Demo.ViewModels {
    public partial class TextFieldsViewModel : ViewModelBase {
        private string _numerics;

        public string Numerics {
            get => _numerics;
            set {
                if (!NumericRegex().IsMatch(value))
                    throw new DataValidationException("Invalid numerics");

                _numerics = value;
                OnPropertyChanged();
            }
        }

        [GeneratedRegex(@"(^[0-9]+$|^$)")]
        private static partial Regex NumericRegex();
    }
}