using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Dialogs;
using Material.Dialog;
using Material.Styles.Assists;

namespace Material.Demo.Pages {
    public partial class Home : UserControl {

        public Home() {
            // Sadly I don't have much time to update this listing
            // and doesn't get updated after any changes
            // I'm gonna disable this listing
            /*
            Features = new ObservableCollection<FeatureStatusModels> {
            new FeatureStatusModels{ FeatureName = "Button (Standard)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Button (Floating)", IsReady = Yes, IsAnimated = NotFully},
            new FeatureStatusModels{ FeatureName = "Button (Tool / Flat)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Toggle button", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "CheckBox", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Slider (Classic)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Slider (Modern)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Slider (Discrete)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Snack bar system", IsReady = NotFully, IsAnimated = No},
            new FeatureStatusModels{ FeatureName = "Popup", IsReady = NotFully, IsAnimated = No},
            new FeatureStatusModels{ FeatureName = "Dialog (DialogHost)", IsReady = No, IsAnimated = NA},
            new FeatureStatusModels{ FeatureName = "Dialog (External, in remaster progress)", IsReady = No, IsAnimated = No},
            new FeatureStatusModels{ FeatureName = "DataGrid", IsReady = NotFully, IsAnimated = NA},
            new FeatureStatusModels{ FeatureName = "Standard Fields (TextBox)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Filled Fields (TextBox)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Outline Fields (TextBox)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Solo Fields (TextBox)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Standard ComboBox", IsReady = NotFully, IsAnimated = No},
            new FeatureStatusModels{ FeatureName = "Filled ComboBox", IsReady = NotFully, IsAnimated = No},
            new FeatureStatusModels{ FeatureName = "Outline ComboBox", IsReady = NotFully, IsAnimated = No},
            new FeatureStatusModels{ FeatureName = "Linear Progress Indicator", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Circular Progress Indicator", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Modern ScrollBar", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Mini ScrollBar", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Card", IsReady = Yes, IsAnimated = No},
            new FeatureStatusModels{ FeatureName = "Navigation Drawer", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels{ FeatureName = "Context Menu", IsReady = NotFully, IsAnimated = NotFully},
            new FeatureStatusModels{ FeatureName = "Icons (Excluded, via Material.Icons.Avalonia)", IsReady = Yes, IsAnimated = NA},
            new FeatureStatusModels{ FeatureName = "Appbar (Top)", IsReady = No, IsAnimated = NA},
            new FeatureStatusModels{ FeatureName = "Appbar (Bottom)", IsReady = No, IsAnimated = NA},
        };*/

            InitializeComponent();
            DataContext = this;
        }

        //public ObservableCollection<FeatureStatusModels> Features { get; private set; }

        public void UseMaterialUIDarkTheme() => GlobalCommand.UseMaterialUIDarkTheme();

        public void UseMaterialUILightTheme() => GlobalCommand.UseMaterialUILightTheme();

        public void OpenProjectRepoLink() => GlobalCommand.OpenProjectRepoLink();

        public void SwitchTransition()
        {
            var window = TopLevel.GetTopLevel(this);
            var state = !TransitionAssist.GetDisableTransitions(window!);
            TransitionAssist.SetDisableTransitions(window!, state);
            DialogHelper.DisableTransitions = state;
        }

        public void ShowAboutAvaloniaUI()
        {
            var window = TopLevel.GetTopLevel(this) as Window;
            new AboutAvaloniaDialog().ShowDialog(window!);
        }
    }
}
