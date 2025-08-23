using System.Collections.Generic;
using Material.Icons;

namespace Material.Avalonia.Demo.Models.TreeDataGrid;

public class OsItem {
    public string Name { get; set; }
    public MaterialIconKind? Icon { get; set; }
    public IList<OsItem> Children { get; set; }
}