using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using DialogHostAvalonia;
using Material.Avalonia.Demo.Models;
using Material.Avalonia.Demo.ViewModels;
using Material.Dialog;
using Material.Dialog.Views;
using Material.Icons;
using Material.Icons.Avalonia;

namespace Material.Avalonia.Demo.Pages;

public partial class DialogDemo : UserControl {
    

    
    public DialogDemo() {
        InitializeComponent();
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
        // Lazy Initialize view model
        DataContext ??= new DialogDemoViewModel();

        base.OnApplyTemplate(e);
    }

    private void OpenDialogWithView(object? sender, RoutedEventArgs e) {
        DialogHost.Show(Resources["Sample2View"]!, "MainDialogHost");
    }

    private void OpenDialogWithModel(object? sender, RoutedEventArgs e) {
        // View that associated with this model defined at DialogContentTemplate in DialogDemo.axaml
        DialogHost.Show(new Sample2Model(new Random().Next(0, 100)), "MainDialogHost");
    }

    private void OpenMoreDialogHostExamples(object? sender, RoutedEventArgs e) {
        Process.Start(new ProcessStartInfo { FileName = "https://github.com/AvaloniaUtils/DialogHost.Avalonia", UseShellExecute = true });
    }

    private async void FilePickerExampleButton_OnClick(object? sender, RoutedEventArgs e) {

        var folders = await GetToplevel()
            .StorageProvider
            .OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Folder file",
            SuggestedFileName = "FileName",
        });
    }
    
    private readonly ItemsPanelTemplate _itemsPanelTemplate = new() {
        Content = new Func<IServiceProvider, object> (_ => new TemplateResult<Control>(new StackPanel { Spacing = 8 }, new NameScope()))
    };
    
    private async void DialogBuilderExample1Button_OnClick(object? sender, RoutedEventArgs e) {
        
        var dialogBuilder = new DialogBuilder()
            .SetTitle("Dialog builder API demo")
            //.SetTitleIcon(DialogIconKind.Success)
            .SetTitleIcon(new MaterialIcon {
                Kind = MaterialIconKind.CogBox,
                Width = 32,
                Height = 32
            })
            .Text("Appended one line text")
            .Text("Appended second line text")
            .PositiveButton("Yay!", "good")
            .NeutralButton("avaloniaUI is awesome!", "promo")
            .Control(new Button {
                Content = "Appended a button"
            });

        var lorem = new SideSheetData().ImportantInfos;

        for (int i = 0; i < 5; i++) {
            dialogBuilder.Text(lorem);
        }
        
        dialogBuilder.Style(new Style(a => 
            a.OfType(typeof(ItemsControl)).Name("PART_ElementHolder"))
        {
            Setters = {
                new Setter(ItemsControl.ItemsPanelProperty, _itemsPanelTemplate)
            }
        });

        dialogBuilder.Style(new Style(a => a.OfType(typeof(DialogControlView))) {
            Setters = {
                new Setter(MaxWidthProperty, 1200.0)
            }
        });
        
        await DialogBuilderProcedurePrivate(dialogBuilder);
    }

    // responsible for common procedure
    private async Task DialogBuilderProcedurePrivate(DialogBuilder dialogBuilder)
    {
        DialogBuilderResultText.Text = "awaiting result..";
        DialogBuilderResultText.Foreground = Brushes.Violet;
        var result = await ShowDialogBuilderObjectPrivate(dialogBuilder);

        DialogBuilderResultText.ClearValue(TextBlock.ForegroundProperty);
        DialogBuilderResultText.Text = result?.ToString() ?? "NULL";
    }

    // responsible for show dialog builder object via standalone window API or dialog host
    private async Task<object?> ShowDialogBuilderObjectPrivate(DialogBuilder dialogBuilder)
    {
        var useDialogHost = UseDialogHostSwitch.IsChecked ?? false;

        if (useDialogHost) {
            var dialog = dialogBuilder.Build();
            
            // Somehow the dialog would non-closeable, this trick will make it able to close anyway
            var wrapper = new HeaderedContentControl
            {
                Header = new Button {
                    Command = DialogHost.CloseDialogCommandProperty.Getter.Invoke(
                        this.FindLogicalAncestorOfType<DialogHost>() ?? throw new InvalidOperationException()),
                    Content = "Force close"
                },
                Content = dialog
            };
            
            return await DialogHost.Show(wrapper, "MainDialogHost");
        }
        
        var window = default(Window);
        
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime app) {
            if (app.MainWindow is not MainWindow w)
                return null;

            window = w;
        }

        var owner = window ?? throw new InvalidOperationException();
        var lastState = default(object);

        await foreach (var state in dialogBuilder.BuildAndShowDialogAsync(owner,
                           modifier: dialog => dialog.AttachDevTools())) {
            DialogBuilderResultText.Text = state?.ToString();
            lastState = state;
        }

        return lastState;
    }

    private TopLevel GetToplevel() => TopLevel.GetTopLevel(this) ?? throw new InvalidOperationException();
}