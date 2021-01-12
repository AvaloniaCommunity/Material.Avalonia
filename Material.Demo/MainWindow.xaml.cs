using Avalonia;
using Avalonia.Controls; 
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml; 

namespace Material.Demo {
    public class MainWindow : Window {
         
        #region Control fields
        private ToggleButton NavDrawerSwitch;
        private ListBox DrawerList;
        private Carousel PageCarousel;
        private ScrollViewer mainScroller;
        #endregion

        public MainWindow() {
            InitializeComponent();
            this.AttachDevTools(KeyGesture.Parse("Shift+F12"));
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);

            #region Control getter and event binding
            NavDrawerSwitch = this.Get<ToggleButton>(nameof(NavDrawerSwitch));

            DrawerList = this.Get<ListBox>(nameof(DrawerList));
            DrawerList.PointerReleased += DrawerSelectionChanged;

            PageCarousel = this.Get<Carousel>(nameof(PageCarousel));

            mainScroller = this.Get<ScrollViewer>(nameof(mainScroller));
            #endregion
        }
        public void DrawerSelectionChanged(object sender, RoutedEventArgs args)
        {
            var listBox = sender as ListBox;
            try
            { 
                PageCarousel.SelectedIndex = listBox.SelectedIndex;
                mainScroller.Offset = Vector.Zero;
            }
            catch
            {
            }
            NavDrawerSwitch.IsChecked = false;
        } 
    }
}