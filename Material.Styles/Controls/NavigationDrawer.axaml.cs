using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Metadata;

namespace Material.Styles.Controls
{
    [PseudoClasses(":open", ":closed", ":left", ":right", ":left-expand", ":right-expand")]
    public class NavigationDrawer : ContentControl
    {
        /// <summary>
        /// <b>Internal use!</b>
        /// This property is used to binding the margin of inner content.
        /// </summary>
        public static readonly StyledProperty<Thickness> ContentMarginProperty =
            AvaloniaProperty.Register<NavigationDrawer, Thickness>(nameof(ContentMargin));

        public static readonly StyledProperty<object> LeftDrawerContentProperty =
            AvaloniaProperty.Register<NavigationDrawer, object>(nameof(LeftDrawerContent));

        public static readonly StyledProperty<IDataTemplate> LeftDrawerContentTemplateProperty =
            AvaloniaProperty.Register<NavigationDrawer, IDataTemplate>(nameof(LeftDrawerContentTemplate));

        public static readonly StyledProperty<bool> LeftDrawerOpenedProperty =
            AvaloniaProperty.Register<NavigationDrawer, bool>(nameof(LeftDrawerOpened));

        public static readonly StyledProperty<double> LeftDrawerWidthProperty =
            AvaloniaProperty.Register<NavigationDrawer, double>(nameof(LeftDrawerWidth));

        public static readonly StyledProperty<double?> LeftDrawerExpandThresholdWidthProperty =
            AvaloniaProperty.Register<NavigationDrawer, double?>(nameof(LeftDrawerExpandThresholdWidth));


        public static readonly StyledProperty<object> RightDrawerContentProperty =
            AvaloniaProperty.Register<NavigationDrawer, object>(nameof(RightDrawerContent));

        public static readonly StyledProperty<IDataTemplate> RightDrawerContentTemplateProperty =
            AvaloniaProperty.Register<NavigationDrawer, IDataTemplate>(nameof(RightDrawerContentTemplate));

        public static readonly StyledProperty<bool> RightDrawerOpenedProperty =
            AvaloniaProperty.Register<NavigationDrawer, bool>(nameof(RightDrawerOpened));

        public static readonly StyledProperty<double> RightDrawerWidthProperty =
            AvaloniaProperty.Register<NavigationDrawer, double>(nameof(RightDrawerWidth));

        public static readonly StyledProperty<double?> RightDrawerExpandThresholdWidthProperty =
            AvaloniaProperty.Register<NavigationDrawer, double?>(nameof(RightDrawerExpandThresholdWidth));

        /// <summary>
        /// <b>Internal use!</b>
        /// This property is used to binding the margin of inner content.
        /// </summary>
        public Thickness ContentMargin
        {
            get => GetValue(ContentMarginProperty);
            set => SetValue(ContentMarginProperty, value);
        }

        /// <summary>
        /// Gets or sets the content to display.
        /// </summary> 
        [DependsOn(nameof(LeftDrawerContentTemplate))]
        public object LeftDrawerContent
        {
            get => GetValue(LeftDrawerContentProperty);
            set => SetValue(LeftDrawerContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the data template used to display the content of the control.
        /// </summary>
        public IDataTemplate LeftDrawerContentTemplate
        {
            get => GetValue(LeftDrawerContentTemplateProperty);
            set => SetValue(LeftDrawerContentTemplateProperty, value);
        }

        public bool LeftDrawerOpened
        {
            get => GetValue(LeftDrawerOpenedProperty);
            set => SetValue(LeftDrawerOpenedProperty, value);
        }

        public double LeftDrawerWidth
        {
            get => GetValue(LeftDrawerWidthProperty);
            set => SetValue(LeftDrawerWidthProperty, value);
        }

        /// <summary>
        /// <p>Get or sets the width threshold of the NavigationDrawer for expand left drawer automatically. Most used on desktop application.</p>
        /// <p>For more information, please visit <a href="https://material.io/components/navigation-drawer#standard-drawer">material.io - Standard navigation drawer, Permanently visible</a> page.</p>
        /// <b>Use it on desktop application is recommended!!</b> 
        /// </summary>
        public double? LeftDrawerExpandThresholdWidth
        {
            get => GetValue(LeftDrawerExpandThresholdWidthProperty);
            set => SetValue(LeftDrawerExpandThresholdWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the content to display.
        /// </summary> 
        [DependsOn(nameof(RightDrawerContentTemplate))]
        public object RightDrawerContent
        {
            get => GetValue(RightDrawerContentProperty);
            set => SetValue(RightDrawerContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the data template used to display the content of the control.
        /// </summary>
        public IDataTemplate RightDrawerContentTemplate
        {
            get => GetValue(RightDrawerContentTemplateProperty);
            set => SetValue(RightDrawerContentTemplateProperty, value);
        }

        public bool RightDrawerOpened
        {
            get => GetValue(RightDrawerOpenedProperty);
            set => SetValue(RightDrawerOpenedProperty, value);
        }

        public double RightDrawerWidth
        {
            get => GetValue(RightDrawerWidthProperty);
            set => SetValue(RightDrawerWidthProperty, value);
        }

        /// <summary>
        /// <p>Get or sets the width threshold of the NavigationDrawer for expand right drawer automatically. Most used on desktop application.</p>
        /// <p>For more information, please visit <a href="https://material.io/components/navigation-drawer#standard-drawer">material.io - Standard navigation drawer, Permanently visible</a> page.</p>
        /// <b>This feature is not recommended if your application is Left-to-Right language orientated. Reference: <a href="https://material.io/components/navigation-drawer#anatomy">material.io.</a></b>
        /// </summary>
        public double? RightDrawerExpandThresholdWidth
        {
            get => GetValue(RightDrawerExpandThresholdWidthProperty);
            set => SetValue(RightDrawerExpandThresholdWidthProperty, value);
        }

        /// <summary>
        /// Closes the left or right drawer, it wont be closed if it is permanent visible.  
        /// </summary>
        /// <param name="isLeftDrawer">set it to false for close the right drawer, otherwise it closes the left drawer.</param>
        public void OptionalCloseDrawer(bool isLeftDrawer = true)
        {
            switch (isLeftDrawer)
            {
                case true:
                    OptionalCloseLeftDrawer();
                    break;
                case false:
                    OptionalCloseRightDrawer();
                    break;
            }
        }

        /// <summary>
        /// Close the left drawer, it wont be closed if it is permanent visible.  
        /// </summary>
        public void OptionalCloseLeftDrawer()
        {
            if (_isLeftDrawerDesktopExpanded)
                return;

            LeftDrawerOpened = false;
        }

        /// <summary>
        /// Close the right drawer, it wont be closed if it is permanent visible.  
        /// </summary>
        public void OptionalCloseRightDrawer()
        {
            if (_isRightDrawerDesktopExpanded)
                return;

            RightDrawerOpened = false;
        }

        /// <summary>
        /// Switch visibility of the left drawer.
        /// </summary>
        public void SwitchLeftDrawerOpened()
        {
            LeftDrawerOpened = !LeftDrawerOpened;
        }

        /// <summary>
        /// Switch visibility of the right drawer.
        /// </summary>
        public void SwitchRightDrawerOpened()
        {
            RightDrawerOpened = !RightDrawerOpened;
        }

        private bool _isLeftDrawerDesktopExpanded;
        private bool _isRightDrawerDesktopExpanded;

        static NavigationDrawer()
        {
            BoundsProperty.Changed.AddClassHandler<NavigationDrawer>(OnDrawerResized);

            LeftDrawerWidthProperty.Changed.AddClassHandler<NavigationDrawer>(OnDrawerWidthChanged);
            RightDrawerWidthProperty.Changed.AddClassHandler<NavigationDrawer>(OnDrawerWidthChanged);

            LeftDrawerOpenedProperty.Changed.AddClassHandler<NavigationDrawer>(OnDrawerOpenedChanged);
            RightDrawerOpenedProperty.Changed.AddClassHandler<NavigationDrawer>(OnDrawerOpenedChanged);

            LeftDrawerExpandThresholdWidthProperty.Changed.AddClassHandler<NavigationDrawer>(
                OnDrawerExpandThresholdWidthChanged);
            RightDrawerExpandThresholdWidthProperty.Changed.AddClassHandler<NavigationDrawer>(
                OnDrawerExpandThresholdWidthChanged);
        }

        private static void OnDrawerResized(NavigationDrawer drawer, AvaloniaPropertyChangedEventArgs args)
        {
            drawer.UpdateDesktopExpand(drawer.Bounds.Width);
            drawer.UpdateContentMargin();
        }

        private static void OnDrawerWidthChanged(NavigationDrawer drawer, AvaloniaPropertyChangedEventArgs args)
        {
            if (drawer.Classes.Contains(":closed"))
                return;

            drawer.UpdateContentMargin();
        }

        private static void OnDrawerExpandThresholdWidthChanged(
            NavigationDrawer drawer, AvaloniaPropertyChangedEventArgs args)
        {
            drawer.UpdateDesktopExpand(drawer.Bounds.Width);
        }

        private static void OnDrawerOpenedChanged(NavigationDrawer drawer,
            AvaloniaPropertyChangedEventArgs args)
        {
            drawer.UpdatePseudoClasses();
            drawer.UpdateContentMargin();
        }

        // ReSharper disable once InconsistentNaming
        private Border? PART_Scrim;

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            if (e.NameScope.Find("PART_Scrim") is Border border)
            {
                PART_Scrim = border;

                PART_Scrim.PointerPressed += PART_Scrim_Pressed;
            }

            base.OnApplyTemplate(e);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            if (PART_Scrim != null)
                PART_Scrim.PointerPressed += PART_Scrim_Pressed;

            base.OnAttachedToVisualTree(e);
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            if (PART_Scrim != null)
                PART_Scrim.PointerPressed -= PART_Scrim_Pressed;

            base.OnDetachedFromVisualTree(e);
        }

        private void UpdateDesktopExpand(double w)
        {
            if (LeftDrawerExpandThresholdWidth.HasValue)
            {
                var status = w > LeftDrawerExpandThresholdWidth.Value;
                _isLeftDrawerDesktopExpanded = status;

                if (Classes.Contains(":left-expand") != status)
                {
                    LeftDrawerOpened = status;
                }

                PseudoClasses.Set(":left-expand", status);
            }
            else
            {
                _isLeftDrawerDesktopExpanded = false;
            }

            if (RightDrawerExpandThresholdWidth.HasValue)
            {
                var status = w > RightDrawerExpandThresholdWidth.Value;
                _isRightDrawerDesktopExpanded = status;

                if (Classes.Contains(":right-expand") != status)
                {
                    RightDrawerOpened = status;
                }

                PseudoClasses.Set(":right-expand", status);
            }
            else
            {
                _isRightDrawerDesktopExpanded = false;
            }
        }

        private void UpdateContentMargin()
        {
            var left = _isLeftDrawerDesktopExpanded && LeftDrawerOpened ? LeftDrawerWidth : 0;
            var right = _isRightDrawerDesktopExpanded && RightDrawerOpened ? RightDrawerWidth : 0;

            ContentMargin = new Thickness(left, 0, right, 0);
        }

        private void PART_Scrim_Pressed(object? sender, RoutedEventArgs e)
        {
            LeftDrawerOpened = false;
            RightDrawerOpened = false;
        }

        private void UpdatePseudoClasses()
        {
            var open = LeftDrawerOpened || RightDrawerOpened;
            PseudoClasses.Set(":open", open);
            PseudoClasses.Set(":closed", !open);
            PseudoClasses.Set(":left", LeftDrawerOpened);
            PseudoClasses.Set(":right", RightDrawerOpened);
        }
    }
}