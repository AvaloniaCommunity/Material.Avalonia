#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Avalonia;
using Material.Demo.Models;
using Material.Icons;

namespace Material.Demo.ViewModels {
    public class IconsDemoViewModel : ViewModelBase {
        private IEnumerable<MaterialIconKindGroup>? _kinds;
        private Lazy<List<MaterialIconKindGroup>> _materialIconKinds;
        private MaterialIconKindGroup? _selectedGroup;
        private string? _searchText;

        public IconsDemoViewModel() {
            _materialIconKinds = new Lazy<List<MaterialIconKindGroup>>(() =>
                Enum.GetNames(typeof(MaterialIconKind))
                    .GroupBy(k => (MaterialIconKind) Enum.Parse(typeof(MaterialIconKind), k))
                    .Select(g => new MaterialIconKindGroup(g))
                    .OrderBy(x => x.Kind)
                    .ToList());
            SearchCommand = new RelayCommand(async o => {
                if (string.IsNullOrWhiteSpace(SearchText))
                    Kinds = _materialIconKinds.Value;
                else {
                    Kinds = new List<MaterialIconKindGroup>();
                    Kinds = await Task.Run(() =>
                        _materialIconKinds.Value
                                          .Where(x => x.Aliases.Any(a => a.IndexOf(SearchText, StringComparison.CurrentCultureIgnoreCase) >= 0))
                                          .ToList());
                }
            });
        }

        public IEnumerable<MaterialIconKindGroup> Kinds {
            get => _kinds ?? _materialIconKinds.Value;
            set {
                _kinds = value;
                OnPropertyChanged(nameof(Kinds));
            }
        }

        public MaterialIconKindGroup? SelectedGroup {
            get => _selectedGroup;
            set {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }

        public string? SearchText {
            get => _searchText;
            set {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public RelayCommand SearchCommand { get; set; }

        public RelayCommand CopyToClipboardCommand { get; set; } =
            new RelayCommand(o => Application.Current.Clipboard.SetTextAsync($"<avalonia:MaterialIcon Kind=\"{o}\" />"));
    }
}