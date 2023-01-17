#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Threading;
using Material.Demo.Models;
using Material.Icons;

namespace Material.Demo.ViewModels
{
    public class IconsDemoViewModel : ViewModelBase
    {
        private IEnumerable<MaterialIconKindGroup>? _kinds;
        private readonly Lazy<List<MaterialIconKindGroup>> _materialIconKinds;
        private MaterialIconKindGroup? _selectedGroup;
        private string? _searchText;

        public IconsDemoViewModel()
        {
            _materialIconKinds = new Lazy<List<MaterialIconKindGroup>>(() =>
                Enum.GetNames(typeof(MaterialIconKind))
                    .GroupBy(k => (MaterialIconKind) Enum.Parse(typeof(MaterialIconKind), k))
                    .Select(g => new MaterialIconKindGroup(this, g))
                    .OrderBy(x => x.Kind)
                    .ToList());
            
            CopyToClipboardCommand = new RelayCommand(o =>
                Application.Current?.Clipboard?.SetTextAsync($"<avalonia:MaterialIcon Kind=\"{o}\" />"));
            
            SearchCommand = new RelayCommand(DoSearchAsync);
        }

        public IEnumerable<MaterialIconKindGroup> Kinds
        {
            get => _kinds ?? _materialIconKinds.Value;
            set
            {
                _kinds = value;
                OnPropertyChanged(nameof(Kinds));
            }
        }

        public MaterialIconKindGroup? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }

        public string? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public RelayCommand SearchCommand { get; }

        public RelayCommand CopyToClipboardCommand { get; }

        private async void DoSearchAsync(object args)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                Kinds = _materialIconKinds.Value;
            else
            {
                var list = new ObservableCollection<MaterialIconKindGroup>();

                Kinds = list;

                foreach (var data in _materialIconKinds.Value
                             .Where(x => x.Aliases
                                 .Any(a => a.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase))))
                {
                    await Dispatcher.UIThread.InvokeAsync(delegate
                    {
                        list.Add(data);
                    });
                }
            }
        }
    }
}