using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;

namespace Material.Styles.Themes {
    public class MaterialThemeBase : AvaloniaObject, IStyle, IResourceProvider {
        private readonly IStyle _controlsStyles;
        private bool _isLoading;
        private IStyle? _loaded;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentTheme"/> class.
        /// </summary>
        /// <param name="baseUri">The base URL for the XAML context.</param>
        public MaterialThemeBase(Uri baseUri) {
            _controlsStyles = new StyleInclude(baseUri) {
                Source = new Uri("avares://Material.Avalonia/Material.Avalonia.Templates.xaml")
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentTheme"/> class.
        /// </summary>
        /// <param name="serviceProvider">The XAML service provider.</param>
        public MaterialThemeBase(IServiceProvider serviceProvider)
            : this(((IUriContext)serviceProvider.GetService(typeof(IUriContext))).BaseUri) { }

        private IResourceDictionary LoadedResourceDictionary => (Loaded as Avalonia.Styling.Styles)!.Resources;

        public static readonly DirectProperty<MaterialThemeBase, ITheme> CurrentThemeProperty =
            AvaloniaProperty.RegisterDirect<MaterialThemeBase, ITheme>(
                nameof(CurrentTheme),
                o => o.CurrentTheme,
                (o, v) => o.CurrentTheme = v);

        private ITheme _currentTheme = new ThemeStruct();

        /// <summary>
        /// Get or set current applied theme
        /// </summary>
        /// <returns>
        /// Returns a STRUCT implementing ITheme interface 
        /// </returns>
        /// <remarks>
        /// To avoid application freeze when applying theme call <see cref="ApplyCurrentThemeAsync"/>
        /// </remarks>
        public ITheme CurrentTheme {
            get => _currentTheme;
            set {
                if (SetAndRaise(CurrentThemeProperty, ref _currentTheme, new ThemeStruct(value))) {
                    Task.Run(() => LoadedResourceDictionary.SetThemeInternal(CurrentTheme));
                }
            }
        }

        public IObservable<ITheme> CurrentThemeChanged => this.GetObservable(CurrentThemeProperty);

        public IResourceHost? Owner => (Loaded as IResourceProvider)?.Owner;

        /// <summary>
        /// Gets the loaded style.
        /// </summary>
        public IStyle Loaded {
            get {
                if (_loaded == null) {
                    _isLoading = true;

                    _loaded = new Avalonia.Styling.Styles() { _controlsStyles };

                    _isLoading = false;
                }

                return _loaded!;
            }
        }

        bool IResourceNode.HasResources => (Loaded as IResourceProvider)?.HasResources ?? false;
        IReadOnlyList<IStyle> IStyle.Children => _loaded?.Children ?? Array.Empty<IStyle>();

        public event EventHandler OwnerChanged {
            add {
                if (Loaded is IResourceProvider rp) {
                    rp.OwnerChanged += value;
                }
            }
            remove {
                if (Loaded is IResourceProvider rp) {
                    rp.OwnerChanged -= value;
                }
            }
        }

        public SelectorMatchResult TryAttach(IStyleable target, IStyleHost? host) => Loaded.TryAttach(target, host);
        public bool TryGetResource(object key, out object? value) {
            if (!_isLoading && Loaded is IResourceProvider p) {
                return p.TryGetResource(key, out value);
            }

            value = null;
            return false;
        }
        void IResourceProvider.AddOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.AddOwner(owner);
        void IResourceProvider.RemoveOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.RemoveOwner(owner);
    }
}