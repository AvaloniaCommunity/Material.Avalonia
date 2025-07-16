using System;

namespace Material.Dialog.Extensions;

public static class ObjectExtensions {
    public static TBuilder RequireNonNullPrivate<TBuilder, TArgs>(this TBuilder builder, TArgs? input, Action<TBuilder, TArgs> action){
        if (input == null)
            return builder;

        action(builder, input);

        return builder;
    }
}