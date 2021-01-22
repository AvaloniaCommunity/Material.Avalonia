using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Material.Demo.Models;
using Material.Styles.Assists;
using System.Collections.ObjectModel;
using static Material.Demo.Models.StatusEnum;

namespace Material.Demo.Pages
{
    public class Home : UserControl
    { 
        public Home()
        {
            Features = new ObservableCollection<FeatureStatusModels>() {
            new FeatureStatusModels(){ FeatureName = "Button (Standard)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Button (Floating)", IsReady = Yes, IsAnimated = NotFully},
            new FeatureStatusModels(){ FeatureName = "Button (Tool / Flat)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Toggler", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "CheckBox", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Slider (Standard)", IsReady = Yes, IsAnimated = No},
            new FeatureStatusModels(){ FeatureName = "Slider (Discrete)", IsReady = Yes, IsAnimated = No},
            new FeatureStatusModels(){ FeatureName = "Slider (with bubble)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Popup", IsReady = NotFully, IsAnimated = No},
            new FeatureStatusModels(){ FeatureName = "Dialog (DialogHost)", IsReady = No, IsAnimated = NA},
            new FeatureStatusModels(){ FeatureName = "Dialog (External)", IsReady = NotFully, IsAnimated = No},
            new FeatureStatusModels(){ FeatureName = "DataGrid", IsReady = NotFully, IsAnimated = NA},
            new FeatureStatusModels(){ FeatureName = "Standard Fields (TextBox)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Filled Fields (TextBox)", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Outline Fields (TextBox)", IsReady = NotFully, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Standard ComboBox", IsReady = NotFully, IsAnimated = No},
            new FeatureStatusModels(){ FeatureName = "Filled ComboBox", IsReady = NotFully, IsAnimated = No},
            new FeatureStatusModels(){ FeatureName = "Linear Progress Indicator", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Circular Progress Indicator", IsReady = No, IsAnimated = NA},
            new FeatureStatusModels(){ FeatureName = "Modern ScrollBar", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Mini ScrollBar", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Card", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Navigation Drawer", IsReady = Yes, IsAnimated = Yes},
            new FeatureStatusModels(){ FeatureName = "Context Menu", IsReady = NotFully, IsAnimated = NotFully},
            new FeatureStatusModels(){ FeatureName = "Integration Icons", IsReady = No, IsAnimated = NA},
            new FeatureStatusModels(){ FeatureName = "Appbar (Top)", IsReady = No, IsAnimated = NA},
            new FeatureStatusModels(){ FeatureName = "Appbar (Bottom)", IsReady = No, IsAnimated = NA},
        };

            this.InitializeComponent();
            DataContext = this;


            UseMaterialUIDarkTheme();
        }

        public ObservableCollection<FeatureStatusModels> Features { get; private set; }

        public void UseMaterialUIDarkTheme() => GlobalCommand.UseMaterialUIDarkTheme();

        public void UseMaterialUILightTheme() => GlobalCommand.UseMaterialUILightTheme();

        public void OpenProjectRepoLink() => GlobalCommand.OpenProjectRepoLink();

        public void SwitchTransition() => TransitionAssist.SetDisableTransitions(Program.MainWindow, !TransitionAssist.GetDisableTransitions(Program.MainWindow));

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
