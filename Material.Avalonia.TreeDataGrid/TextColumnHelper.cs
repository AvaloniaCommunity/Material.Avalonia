using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Media;

namespace Material.Avalonia.TreeDataGrid;

internal static class TextColumnHelper
{
    public static TextAlignment? GetTextAlignmentGeneric<TModel, TValue>(object column)
        where TModel : class
    {
        var typed = (TextColumn<TModel, TValue>)column;
        return typed.Options.TextAlignment;
    }
}