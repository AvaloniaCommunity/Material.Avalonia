using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Selection;
using Avalonia.Media;
using Material.Avalonia.Demo.Models.TreeDataGrid;
using Material.Icons;

namespace Material.Avalonia.Demo.ViewModels;

public class TreeDataGridsDemoViewModel : ViewModelBase 
{
    private readonly ObservableCollection<Country> _data;
    private bool _cellSelection;
    public FlatTreeDataGridSource<Country> CountriesSource { get; }
    public HierarchicalTreeDataGridSource<OsItem> OsTreeSource { get; }

    public TreeDataGridsDemoViewModel() {
        var linuxChildren = new[] {
            new OsItem { Name = "Android", Icon = MaterialIconKind.Android },
            new OsItem { Name = "Arch Linux", Icon = MaterialIconKind.Arch },
            new OsItem { Name = "Fedora", Icon = MaterialIconKind.Fedora },
            new OsItem {
                Name = "Debian", Icon = MaterialIconKind.Debian,
                Children = [
                    new OsItem {
                        Name = "Ubuntu", Icon = MaterialIconKind.Ubuntu, Children = [
                            new OsItem { Name = "Mint Linux", Icon = MaterialIconKind.LinuxMint }
                        ]
                    },
                    new OsItem { Name = "Pop!_OS" },
                    new OsItem { Name = "Lubuntu" },
                    new OsItem { Name = "Zorin OS" }
                ]
            },
        };

        var roots = new[] {
            new OsItem { Name = "Apple MacOS", Icon = MaterialIconKind.AppleFinder },
            new OsItem { Name = "Apple iOS", Icon = MaterialIconKind.AppleIos },
            new OsItem { Name = "Windows", Icon = MaterialIconKind.MicrosoftWindows },
            new OsItem {
                Name = "Linux", Icon = MaterialIconKind.Linux,
                Children = linuxChildren
            }
        };

        OsTreeSource = new HierarchicalTreeDataGridSource<OsItem>(roots) {
            Columns = {
                new HierarchicalExpanderColumn<OsItem>(
                    new TemplateColumn<OsItem>(
                        "Name",
                        "OsCell",
                        null,
                        new GridLength(1, GridUnitType.Star),
                        null),
                    x => x.Children)
            }
        };
        
        _data = new ObservableCollection<Country>(Countries.All);

        CountriesSource = new FlatTreeDataGridSource<Country>(_data) {
            Columns = {
                new TextColumn<Country, string>("Country", x => x.Name, (r, v) => r.Name = v,
                    new GridLength(6, GridUnitType.Star), new TextColumnOptions<Country> {
                        IsTextSearchEnabled = true,
                    }),
                new TemplateColumn<Country>("Region", "RegionCell", "RegionEditCell"),
                new TextColumn<Country, int>("Population", x => x.Population, new GridLength(3, GridUnitType.Star)),
                new TextColumn<Country, int>("Area", x => x.Area, new GridLength(3, GridUnitType.Star)),
                new TextColumn<Country, int>("GDP", x => x.GDP, new GridLength(3, GridUnitType.Star), new TextColumnOptions<Country> {
                    TextAlignment = TextAlignment.Right,
                    MaxWidth = new GridLength(150)
                }),
            }
        };
        CountriesSource.RowSelection!.SingleSelect = false;
    }
    
    public bool CellSelection {
        get => _cellSelection;
        set {
            if (_cellSelection != value) {
                _cellSelection = value;
                if (_cellSelection)
                    CountriesSource.Selection = new TreeDataGridCellSelectionModel<Country>(CountriesSource) { SingleSelect = false };
                else
                    CountriesSource.Selection = new TreeDataGridRowSelectionModel<Country>(CountriesSource) { SingleSelect = false };
            }
        }
    }
    
    public void AddCountry(Country country) => _data.Add(country);

    public void RemoveSelected()
    {
        var selection = ((ITreeSelectionModel)CountriesSource.Selection!).SelectedIndexes.ToList();

        for (var i = selection.Count - 1; i >= 0; --i)
        {
            _data.RemoveAt(selection[i][0]);
        }
    }
}