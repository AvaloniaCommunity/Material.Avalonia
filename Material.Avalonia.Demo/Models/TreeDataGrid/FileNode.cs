using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Material.Avalonia.Demo.Models.TreeDataGrid;

public sealed class FileNode
{
    public FileNode(string name, string kind, long size = 0, IEnumerable<FileNode>? children = null)
    {
        Name = name;
        Kind = kind;
        Size = size;

        if (children is not null)
        {
            foreach (FileNode child in children)
            {
                Children.Add(child);
            }
        }
    }

    public string Name { get; }

    public string Kind { get; }

    public long Size { get; }

    public ObservableCollection<FileNode> Children { get; } = new();
}
