using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Material.Styles.Additional;

namespace Material.Styles
{
    public class MaterialToolKit : Avalonia.Styling.Styles
    {
        static MaterialToolKit()
        {
            Animation.RegisterCustomAnimator<RelativePoint, RelativePointAnimator>();
        }

        public MaterialToolKit()
        {
            AvaloniaXamlLoader.Load(this);
            if (AppContext.TryGetSwitch("MaterialThemeIncludeDataGrid", out var includeDataGrid) && includeDataGrid)
            {
                Add(new StyleInclude((Uri?)null) { Source = new Uri("avares://Material.DataGrid/DataGrid.xaml") });
            }
        }
    }
}