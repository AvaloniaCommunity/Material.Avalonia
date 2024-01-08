using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
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
            if (AppContext.TryGetSwitch("MaterialThemeIncludeDataGrid", out var includeDataGrid) && includeDataGrid) {
                Add(new StyleInclude((Uri?)null) { Source = new Uri("avares://Material.Avalonia.DataGrid/DataGrid.xaml") });
            }
        }
    }
}