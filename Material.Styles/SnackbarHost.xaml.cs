using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Threading;
using Material.Styles.Models;

namespace Material.Styles
{
    public class SnackbarHost : ContentControl
    {
        private static HashSet<SnackbarHost> _snackbarHosts;

        private ObservableCollection<SnackbarModel> _snackbars;
        public ObservableCollection<SnackbarModel> SnackbarModels => _snackbars;
        
        /// <summary>
        /// Get the name of host. The name of host can be set only one time.
        /// </summary>
        public string HostName
        {
            get => GetValue(HostNameProperty);
            set
            {
                if (HostName == null)
                    SetValue(HostNameProperty, value);
                else
                    throw new InvalidOperationException("The name of host can be set only one time.");
            }
        }

        public static readonly StyledProperty<string> HostNameProperty =
            AvaloniaProperty.Register<SnackbarHost, string>(nameof(HostName));

        public HorizontalAlignment SnackbarHorizontalAlignment
        {
            get => GetValue(SnackbarHorizontalAlignmentProperty);
            set => SetValue(SnackbarHorizontalAlignmentProperty, value);
        }

        public static readonly StyledProperty<HorizontalAlignment> SnackbarHorizontalAlignmentProperty =
            AvaloniaProperty.Register<SnackbarHost, HorizontalAlignment>(nameof(SnackbarHorizontalAlignment), HorizontalAlignment.Left);

        public VerticalAlignment SnackbarVerticalAlignment
        {
            get => GetValue(SnackbarVerticalAlignmentProperty);
            set => SetValue(SnackbarVerticalAlignmentProperty, value);
        }

        public static readonly StyledProperty<VerticalAlignment> SnackbarVerticalAlignmentProperty =
            AvaloniaProperty.Register<SnackbarHost, VerticalAlignment>(nameof(SnackbarVerticalAlignment), VerticalAlignment.Bottom);

        static SnackbarHost()
        {
            _snackbarHosts = new HashSet<SnackbarHost>();
        }
        
        public SnackbarHost()
        {
            // Initialize model collection
            this._snackbars = new ObservableCollection<SnackbarModel>();
            
            this.TemplateApplied += OnTemplateApplied;
            this.AttachedToLogicalTree += OnAttachedToLogicalTree;
            this.DetachedFromLogicalTree += OnDetachedFromLogicalTree;
        }

        private static string GetFirstHostName()
        {
            if (_snackbarHosts is null)
                // THIS IS IMPOSSIBLE TO HAPPEN! But I kept this for any reasons.
                throw new NullReferenceException("Snackbar hosts pool is not initialized!");

            return _snackbarHosts.First().HostName;
        }

        private static SnackbarHost GetHost(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            var result = _snackbarHosts.Where(
                // Predicate
                // And do not asking me, why I'm using delegate here.
                // Performance are important too.
                delegate(SnackbarHost host)
                {
                    return host.HostName == name;
                });

            // If exists any matched results.
            if (result.Any())
            {
                return result.First();
            }

            // or just return null if no any results.
            return null;
        }

        /// <summary>
        /// Post an snackbar with message text.
        /// </summary>
        /// <param name="text">message text.</param>
        /// <param name="targetHost">the snackbar host that you wanted to use.</param>
        public static void Post(string text, string targetHost = null) => Post(new SnackbarModel(text), targetHost);
        
        /// <summary>
        /// Post an snackbar with custom content and button (only one).
        /// </summary>
        /// <param name="model">snackbar data model.</param>
        /// <param name="targetHost">the snackbar host that you wanted to use.</param>
        public static void Post(SnackbarModel model, string targetHost = null)
        {
            if (targetHost is null)
                targetHost = GetFirstHostName();
            
            var host = GetHost(targetHost);

            if (host is null)
                throw new ArgumentNullException(nameof(targetHost), $"The target host named \"{targetHost}\" is not exist.");

            ElapsedEventHandler onExpired = null;
            onExpired = delegate(object sender, ElapsedEventArgs args)
            {
                if (sender is Timer timer)
                {
                    // Remove timer.
                    timer.Stop();
                    timer.Elapsed -= onExpired;
                    timer.Dispose();
                    
                    OnSnackbarDurationExpired(host, model);
                }
            };
            
            var timer = new Timer(model.Duration.TotalMilliseconds);
            timer.Elapsed += onExpired;
            timer.Start();
            
            Dispatcher.UIThread.Post(delegate
            {
                host.SnackbarModels.Add(model);
            });
        }

        private static void OnSnackbarDurationExpired(SnackbarHost host, SnackbarModel model)
        {
            Dispatcher.UIThread.Post(delegate
            {
                host.SnackbarModels.Remove(model);
            });
        }

        private void OnAttachedToLogicalTree(object sender, LogicalTreeAttachmentEventArgs e)
        {
            if (sender is SnackbarHost host)
            {
                _snackbarHosts.Add(host);
            }
        }

        private void OnDetachedFromLogicalTree(object sender, LogicalTreeAttachmentEventArgs e)
        {
            if (sender is SnackbarHost host)
            {
                if (host.HostName is null)
                    throw new ArgumentNullException(nameof(HostName));
                
                _snackbarHosts.Remove(host);
            }
        }

        private void OnTemplateApplied(object sender, TemplateAppliedEventArgs e)
        {
            if (sender is SnackbarHost host)
            {
                host.TemplateApplied -= OnTemplateApplied;
                // Initialize snackbar host
                
                if (host.HostName is null)
                    throw new ArgumentNullException(nameof(HostName));
            }
        }
    }
}
