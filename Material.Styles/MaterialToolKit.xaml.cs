using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Material.Styles.Additional;
using Material.Styles.Assists.Mixins;

namespace Material.Styles {
    public class MaterialToolKit : Avalonia.Styling.Styles {
        static MaterialToolKit() {
            Animation.RegisterCustomAnimator<RelativePoint, RelativePointAnimator>();
            MaterialBorderBoxShadowMixin.Attach<Border>();
        }


        public MaterialToolKit() {
            AvaloniaXamlLoader.Load(this);
            IncludeDataGridStyles();
        }


#pragma warning disable IL2072

#if NET5_0_OR_GREATER
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor, "MaterialDataGridStyles",
            "Material.Avalonia.DataGrid")]
        [UnconditionalSuppressMessage("Trimming",
            "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code",
            Justification = "Referenced by DynamicDependency")]
#endif
        private void IncludeDataGridStyles() {
            if (!AppContext.TryGetSwitch("MaterialThemeIncludeDataGrid", out var includeDataGrid) ||
                !includeDataGrid) return;
            var dataGridStylesType = Assembly.Load("Material.Avalonia.DataGrid")
                .GetType("Material.Avalonia.DataGrid.MaterialDataGridStyles")!;
            var instance = Activator.CreateInstance(dataGridStylesType)!;
            Add((Avalonia.Styling.Styles)instance);
        }
    }
}