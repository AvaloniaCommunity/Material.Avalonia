using Avalonia;
using Avalonia.Controls;

namespace Material.Demo.Dummy
{
    /// <summary>
    /// Dummy control, used for replacing XamlDisplay temporarily. Only show the controls, no additional xaml shows now.
    /// Require ShowMeXaml for Avalonia 11.0
    /// </summary>
    public class XamlDisplay : ContentControl
    {
        public static DirectProperty<XamlDisplay, string> UniqueIdProperty = 
            AvaloniaProperty.RegisterDirect<XamlDisplay, string>(nameof(UniqueId),
                a => a.UniqueId,
                (a, b) => a.UniqueId = b);

        public string UniqueId
        {
            get => _uniqueId;
            set => SetAndRaise(UniqueIdProperty, ref _uniqueId, value);
        }

        private string _uniqueId = string.Empty;
    }
}