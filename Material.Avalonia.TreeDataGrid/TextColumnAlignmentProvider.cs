using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Media;

namespace Material.Avalonia.TreeDataGrid;

internal static class TextColumnAlignmentProvider
{
    private static readonly ConcurrentDictionary<Type, Func<object, TextAlignment?>> Cache = new();

    public static TextAlignment? GetTextAlignment(object column)
    {
        ArgumentNullException.ThrowIfNull(column);

        Type columnType = column.GetType();
        if (!columnType.IsGenericType || columnType.GetGenericTypeDefinition() != typeof(TextColumn<,>))
        {
            return null;
        }

        Func<object, TextAlignment?> getter = Cache.GetOrAdd(columnType, BuildGetter);
        return getter(column);
    }

    [UnconditionalSuppressMessage(
        "AOT",
        "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.",
        Justification = "The closed TextColumn type is already present when the getter is built.")]
    private static Func<object, TextAlignment?> BuildGetter(Type closedColumnType)
    {
        MethodInfo helperMethod = typeof(TextColumnHelper)
            .GetMethod(
                nameof(TextColumnHelper.GetTextAlignmentGeneric),
                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)!;

        Type[] genericArguments = closedColumnType.GetGenericArguments();
        MethodInfo closedMethod = helperMethod.MakeGenericMethod(genericArguments);

        return (Func<object, TextAlignment?>)closedMethod
            .CreateDelegate(typeof(Func<object, TextAlignment?>));
    }
}
