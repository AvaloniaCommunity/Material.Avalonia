using System.Collections.ObjectModel;
using System.Text;

namespace Material.Avalonia.Demo.ViewModels;

public class ExpanderDemoViewModel : ViewModelBase
{
    public ObservableCollection<string> LoremText => _loremText;
    private readonly ObservableCollection<string> _loremText;

    public ExpanderDemoViewModel()
    {
        _loremText = new ObservableCollection<string>();
            
        var builder = new StringBuilder();
            
        builder.Append("Lorem ipsum dolor sit amet, consectetur adipiscing elit. ");
        builder.AppendLine("Suspendisse malesuada lacus ex, sit amet blandit leo lobortis eget.");
            
        for (var i = 0; i < 10; i++)
        {
            LoremText.Add(builder.ToString());
        }

        builder.Clear();
    }
}