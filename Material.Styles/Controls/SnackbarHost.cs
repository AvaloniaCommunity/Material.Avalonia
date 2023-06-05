using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Threading;
using Material.Styles.Commands;
using Material.Styles.Models;

namespace Material.Styles.Controls {
    public class SnackbarHost : ContentControl {
        private static readonly Dictionary<string, SnackbarHost> SnackbarHostDictionary;

        private readonly ObservableCollection<SnackbarModel> _snackbars;
        public ObservableCollection<SnackbarModel> SnackbarModels => _snackbars;

        /// <summary>
        /// Get the name of host. The name of host can be set only one time.
        /// </summary>
        public string HostName {
            get => GetValue(HostNameProperty);
            set {
                if (string.IsNullOrEmpty(HostName)) {
                    SetValue(HostNameProperty, value);

                    if (!SnackbarHostDictionary.ContainsValue(this))
                        return;

                    KeyValuePair<string, SnackbarHost>? target = null;
                    foreach (var host in SnackbarHostDictionary
                                 .Where(host => ReferenceEquals(host.Value, this))) {
                        target = host;
                        break;
                    }

                    if (target.HasValue) {
                        SnackbarHostDictionary.Remove(target.Value.Key);
                    }
                }
                else
                    throw new InvalidOperationException("The name of host can be set only one time.");
            }
        }

        public static readonly StyledProperty<string> HostNameProperty =
            AvaloniaProperty.Register<SnackbarHost, string>(nameof(HostName));

        public HorizontalAlignment SnackbarHorizontalAlignment {
            get => GetValue(SnackbarHorizontalAlignmentProperty);
            set => SetValue(SnackbarHorizontalAlignmentProperty, value);
        }

        public static readonly StyledProperty<HorizontalAlignment> SnackbarHorizontalAlignmentProperty =
            AvaloniaProperty.Register<SnackbarHost, HorizontalAlignment>(nameof(SnackbarHorizontalAlignment),
                HorizontalAlignment.Left);

        public VerticalAlignment SnackbarVerticalAlignment {
            get => GetValue(SnackbarVerticalAlignmentProperty);
            set => SetValue(SnackbarVerticalAlignmentProperty, value);
        }

        public static readonly StyledProperty<VerticalAlignment> SnackbarVerticalAlignmentProperty =
            AvaloniaProperty.Register<SnackbarHost, VerticalAlignment>(nameof(SnackbarVerticalAlignment),
                VerticalAlignment.Bottom);

        public int SnackbarMaxCounts {
            get => GetValue(SnackbarMaxCountsProperty);
            set => SetValue(SnackbarMaxCountsProperty, value);
        }

        public static readonly StyledProperty<int> SnackbarMaxCountsProperty =
            AvaloniaProperty.Register<SnackbarHost, int>(nameof(SnackbarMaxCounts), 1);

        static SnackbarHost() {
            //_snackbarHosts = new HashSet<SnackbarHost>();
            SnackbarHostDictionary = new Dictionary<string, SnackbarHost>();
        }

        public SnackbarHost() {
            // Initialize model collection
            _snackbars = new ObservableCollection<SnackbarModel>();
        }

        private static string GetFirstHostName() {
            if (SnackbarHostDictionary is null)
                // THIS IS IMPOSSIBLE TO HAPPEN! But I kept this for any reasons.
                throw new NullReferenceException("Snackbar hosts pool is not initialized!");

            return SnackbarHostDictionary.First().Key;
        }

        private static SnackbarHost GetHost(string? name) {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            var result = SnackbarHostDictionary[name];
            return result;
        }

        /// <summary>
        /// Post an snackbar with message text.
        /// </summary>
        /// <param name="text">message text.</param>
        /// <param name="targetHost">the snackbar host that you wanted to use.</param>
        /// <param name="priority">the priority of current task.</param>
        public static void Post(string text, string? targetHost,
            DispatcherPriority priority) {
            Post(new SnackbarModel(text), targetHost, priority);
        }

        /// <summary>
        /// Post an snackbar with custom content and button (only one).
        /// </summary>
        /// <param name="model">snackbar data model.</param>
        /// <param name="targetHost">the snackbar host that you wanted to use.</param>
        /// <param name="priority">the priority of current task.</param>
        public static void Post(SnackbarModel model, string? targetHost,
            DispatcherPriority priority) {
            if (string.IsNullOrEmpty(targetHost))
                targetHost = GetFirstHostName();

            var host = GetHost(targetHost!);

            if (host is null)
                throw new ArgumentNullException(nameof(targetHost),
                    $"The target host named \"{targetHost}\" is not exist.");

            // If duration is TimeSpan.Zero, dont expire it.
            if (model.Duration != TimeSpan.Zero) {
                void OnExpired(object sender, ElapsedEventArgs args) {
                    if (sender is not Timer timer)
                        return;

                    // Remove timer.
                    timer.Stop();
                    timer.Elapsed -= OnExpired;
                    timer.Dispose();

                    OnSnackbarDurationExpired(host, model);
                }

                var timer = new Timer(model.Duration.TotalMilliseconds);
                timer.Elapsed += OnExpired;
                timer.Start();
            }

            if (model.Button != null) {
                model.Command = new SnackbarCommand(host, model);
            }

            Dispatcher.UIThread.Post(delegate {
                var max = host.SnackbarMaxCounts;
                var collection = host.SnackbarModels;

                while (collection.Count >= max) {
                    var m = collection.First();
                    collection.Remove(m);
                }

                host.SnackbarModels.Add(model);
            }, priority);
        }

        /// <summary>
        /// Removes a snackbar manually
        /// </summary>
        /// <param name="model">snackbar data model.</param>
        /// <param name="targetHost">the snackbar host that you wanted to use.</param>
        /// <param name="priority">the priority of current task.</param>
        public static void Remove(SnackbarModel model, string? targetHost,
            DispatcherPriority priority) {
            if (string.IsNullOrEmpty(targetHost))
                targetHost = GetFirstHostName();

            var host = GetHost(targetHost);

            if (host is null)
                throw new ArgumentNullException(nameof(targetHost),
                    $"The target host named \"{targetHost}\" is not exist.");

            host.RemoveSnackbarModel(model, priority);
        }

        private static void OnSnackbarDurationExpired(SnackbarHost host, SnackbarModel model) {
            host.RemoveSnackbarModel(model, DispatcherPriority.Background);
        }

        private void RemoveSnackbarModel(SnackbarModel model,
            DispatcherPriority priority) {
            Dispatcher.UIThread.Post(delegate { SnackbarModels.Remove(model); }, priority);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
            SnackbarHostDictionary.Add(HostName, this);

            base.OnAttachedToVisualTree(e);
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e) {
            SnackbarHostDictionary.Remove(HostName);

            base.OnDetachedFromVisualTree(e);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
            // Initialize snackbar host
            if (HostName is null)
                throw new ArgumentNullException(nameof(HostName),
                    "The name of SnackbarHost is null. Please define it.");

            base.OnApplyTemplate(e);
        }
    }
}
