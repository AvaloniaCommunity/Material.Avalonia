using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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
        
        private ITheme _currentTheme = new Theme();
        private readonly Subject<ITheme> _currentThemeChanged = new();
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

        public ITheme CurrentTheme {
            get => _currentTheme;
            set {
                (Loaded as Avalonia.Styling.Styles)!.Resources.SetMaterialTheme(CurrentTheme);
                _currentTheme = value;
                _currentThemeChanged.OnNext(value);
            }
        }

        public Task ApplyCurrentThemeAsync(ITheme? newTheme = null) {
            return Task.Run(() => 
                CurrentTheme = newTheme ?? _currentTheme);
        }

        public IObservable<ITheme> CurrentThemeChanged => _currentThemeChanged.AsObservable();

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