using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Material.Avalonia.TreeDataGrid.Converters;

public sealed class TreeDataGridSourceColumnAlignmentToDockConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is not ITreeDataGridSource source ||
            values[1] is not int columnIndex ||
            columnIndex < 0 ||
            columnIndex >= source.Columns.Count)
        {
            return Dock.Left;
        }

        IColumn column = source.Columns[columnIndex];
        TextAlignment? alignment = TextColumnAlignmentProvider.GetTextAlignment(column);

        return alignment == TextAlignment.Right ? Dock.Right : Dock.Left;
    }

    public object[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
