using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Media;

namespace Material.Avalonia.TreeDataGrid;

internal static class TextColumnAlignmentProvider
{
    private static readonly ConcurrentDictionary<Type, Func<object, TextAlignment?>> _cache = new();

    public static TextAlignment? GetTextAlignment(object column)
    {
        ArgumentNullException.ThrowIfNull(column);

        var colType = column.GetType();
        if (!colType.IsGenericType
            || colType.GetGenericTypeDefinition() != typeof(TextColumn<,>))
            return null;

        var getter = _cache.GetOrAdd(colType, BuildGetter);
        return getter(column);
    }

    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    private static Func<object, TextAlignment?> BuildGetter(Type closedColumnType)
    {
        var helperMethod = typeof(TextColumnHelper)
            .GetMethod(nameof(TextColumnHelper.GetTextAlignmentGeneric),
                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)!;

        var genArgs = closedColumnType.GetGenericArguments();
        var closedMethod = helperMethod.MakeGenericMethod(genArgs);

        return (Func<object, TextAlignment?>)closedMethod
            .CreateDelegate(typeof(Func<object, TextAlignment?>));
    }
}
