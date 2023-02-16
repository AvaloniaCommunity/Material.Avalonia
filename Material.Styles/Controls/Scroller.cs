using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace Material.Styles.Controls {
    /// <summary>
    /// A customized ScrollViewer that designed for scrolling in TabControl and Breadcrumbs.
    /// Suitable for single orientation scrolls (horizontally or vertically, for standard scroll: <see cref="ScrollViewer"/>)
    /// </summary>
    /// TODO: Complete component modification
    public sealed class Scroller : ContentControl, IScrollable, IScrollAnchorProvider {
        /// <summary>
        /// Defines the <see cref="Orientation"/> property.
        /// </summary>
        public static readonly DirectProperty<Scroller, Orientation> OrientationProperty =
            AvaloniaProperty.RegisterDirect<Scroller, Orientation>(nameof(Orientation),
                o => o.Orientation,
                (o, v) => o.Orientation = v);

        public static readonly DirectProperty<Scroller, bool> HandleMouseWheelProperty =
            AvaloniaProperty.RegisterDirect<Scroller, bool>(
                nameof(CanHorizontallyScroll),
                o => o.HandleMouseWheel,
                (o, v) => o.HandleMouseWheel = v);

        /// <summary>
        /// Defines the <see cref="CanHorizontallyScroll"/> property.
        /// </summary>
        /// <remarks>
        /// There is no public C# accessor for this property as it is intended to be bound to by a 
        /// <see cref="ScrollContentPresenter"/> in the control's template.
        /// </remarks>
        public static readonly DirectProperty<Scroller, bool> CanHorizontallyScrollProperty =
            AvaloniaProperty.RegisterDirect<Scroller, bool>(
                nameof(CanHorizontallyScroll),
                o => o.CanHorizontallyScroll);

        /// <summary>
        /// Defines the <see cref="CanVerticallyScroll"/> property.
        /// </summary>
        /// <remarks>
        /// There is no public C# accessor for this property as it is intended to be bound to by a 
        /// <see cref="ScrollContentPresenter"/> in the control's template.
        /// </remarks>
        public static readonly DirectProperty<Scroller, bool> CanVerticallyScrollProperty =
            AvaloniaProperty.RegisterDirect<Scroller, bool>(
                nameof(CanVerticallyScroll),
                o => o.CanVerticallyScroll);

        /// <summary>
        /// Defines the <see cref="Extent"/> property.
        /// </summary>
        public static readonly DirectProperty<Scroller, Size> ExtentProperty =
            AvaloniaProperty.RegisterDirect<Scroller, Size>(nameof(Extent),
                o => o.Extent,
                (o, v) => o.Extent = v);

        /// <summary>
        /// Defines the <see cref="Offset"/> property.
        /// </summary>
        public static readonly DirectProperty<Scroller, Vector> OffsetProperty =
            AvaloniaProperty.RegisterDirect<Scroller, Vector>(
                nameof(Offset),
                o => o.Offset,
                (o, v) => o.Offset = v);

        /// <summary>
        /// Defines the <see cref="Viewport"/> property.
        /// </summary>
        public static readonly DirectProperty<Scroller, Size> ViewportProperty =
            AvaloniaProperty.RegisterDirect<Scroller, Size>(nameof(Viewport),
                o => o.Viewport,
                (o, v) => o.Viewport = v);

        /// <summary>
        /// Defines the <see cref="ScrollChanged"/> event.
        /// </summary>
        public static readonly RoutedEvent<ScrollChangedEventArgs> ScrollChangedEvent =
            RoutedEvent.Register<Scroller, ScrollChangedEventArgs>(
                nameof(ScrollChanged),
                RoutingStrategies.Bubble);

        /// <summary>
        /// Defines the <see cref="CanScrollToStart"/> property.
        /// </summary>
        public static readonly DirectProperty<Scroller, bool> CanScrollToStartProperty =
            AvaloniaProperty.RegisterDirect<Scroller, bool>(
                nameof(CanScrollToStart),
                o => o.CanScrollToStart);

        /// <summary>
        /// Defines the <see cref="CanScrollToStart"/> property.
        /// </summary>
        public static readonly DirectProperty<Scroller, bool> CanScrollToEndProperty =
            AvaloniaProperty.RegisterDirect<Scroller, bool>(
                nameof(CanScrollToEnd),
                o => o.CanScrollToEnd);

        /// <summary>
        /// Defines the <see cref="SmallScrollMultiplier"/> property.
        /// </summary>
        public static readonly DirectProperty<Scroller, double> SmallScrollMultiplierProperty =
            AvaloniaProperty.RegisterDirect<Scroller, double>(
                nameof(CanScrollToStart),
                o => o.SmallScrollMultiplier,
                (o, v) => o.SmallScrollMultiplier = v);

        /// <summary>
        /// Defines the <see cref="ScrollSpeed"/> property.
        /// </summary>
        public static readonly DirectProperty<Scroller, double> ScrollSpeedProperty =
            AvaloniaProperty.RegisterDirect<Scroller, double>(
                nameof(CanScrollToEnd),
                o => o.ScrollSpeed,
                (o, v) => o.ScrollSpeed = v);
        private bool _canScrollToEnd;
        private bool _canScrollToStart;

        private IDisposable? _childSubscription;
        private Size _extent;
        private bool _handleMouseWheel;
        private Vector _offset;
        private Size _oldExtent;
        private Vector _oldOffset;
        private Size _oldViewport;
        private Orientation _orientation;
        private double _scrollSpeed;
        private double _smallScrollMultiplier;
        private Size _viewport;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scroller"/> class.
        /// </summary>
        public Scroller() {
            LayoutUpdated += OnLayoutUpdated;
        }

        public bool HandleMouseWheel {
            get => _handleMouseWheel;
            set => SetAndRaise(HandleMouseWheelProperty, ref _handleMouseWheel, value);
        }

        public Orientation Orientation {
            get => _orientation;
            set {
                var wasEnabledHScroll = CanHorizontallyScroll;
                var wasEnabledVScroll = CanVerticallyScroll;

                if (!SetAndRaise(OrientationProperty, ref _orientation, value))
                    return;

                var isEnabledHScroll = CanHorizontallyScroll;
                var isEnabledVScroll = CanVerticallyScroll;

                RaisePropertyChanged(CanHorizontallyScrollProperty, wasEnabledHScroll, isEnabledHScroll);
                RaisePropertyChanged(CanVerticallyScrollProperty, wasEnabledVScroll, isEnabledVScroll);
            }
        }

        /// <summary>
        /// Gets or sets the primary scroll speed for the scroller
        /// </summary>
        public double ScrollSpeed {
            get => _scrollSpeed;
            set => SetAndRaise(ScrollSpeedProperty, ref _scrollSpeed, value);
        }

        /// <summary>
        /// Gets or sets the small scroll speed for the scroller. The final speed will multiple <see cref="ScrollSpeed"/> by this property.
        /// </summary>
        public double SmallScrollMultiplier {
            get => _smallScrollMultiplier;
            set => SetAndRaise(SmallScrollMultiplierProperty, ref _smallScrollMultiplier, value);
        }

        public bool CanScrollToStart {
            get => _canScrollToStart;
            private set => SetAndRaise(CanScrollToStartProperty, ref _canScrollToStart, value);
        }

        public bool CanScrollToEnd {
            get => _canScrollToEnd;
            private set => SetAndRaise(CanScrollToEndProperty, ref _canScrollToEnd, value);
        }

        /// <summary>
        /// Gets a value indicating whether the viewer can scroll horizontally.
        /// </summary>
        public bool CanHorizontallyScroll => Orientation == Orientation.Horizontal;

        /// <summary>
        /// Gets a value indicating whether the viewer can scroll vertically.
        /// </summary>
        public bool CanVerticallyScroll => Orientation == Orientation.Vertical;

        /// <summary>
        /// Gets the extent of the scrollable content.
        /// </summary>
        public Size Extent {
            get => _extent;
            private set {
                if (SetAndRaise(ExtentProperty, ref _extent, value))
                    CalculatedPropertiesChanged();
            }
        }

        /// <summary>
        /// Gets or sets the current scroll offset.
        /// </summary>
        public Vector Offset {
            get => _offset;
            set {
                if (SetAndRaise(OffsetProperty, ref _offset, CoerceOffset(Extent, Viewport, value)))
                    CalculatedPropertiesChanged();
            }
        }

        /// <summary>
        /// Gets the size of the viewport on the scrollable content.
        /// </summary>
        public Size Viewport {
            get => _viewport;
            private set {
                if (SetAndRaise(ViewportProperty, ref _viewport, value))
                    CalculatedPropertiesChanged();
            }
        }

        /// <inheritdoc/>
        public Control? CurrentAnchor => Presenter is IScrollAnchorProvider scrollAnchorProvider
            ? scrollAnchorProvider.CurrentAnchor
            : null;

        /// <inheritdoc/>
        public void RegisterAnchorCandidate(Control element) {
            (Presenter as IScrollAnchorProvider)?.RegisterAnchorCandidate(element);
        }

        /// <inheritdoc/>
        public void UnregisterAnchorCandidate(Control element) {
            (Presenter as IScrollAnchorProvider)?.UnregisterAnchorCandidate(element);
        }

        /// <summary>
        /// Occurs when changes are detected to the scroll position, extent, or viewport size.
        /// </summary>
        public event EventHandler<ScrollChangedEventArgs> ScrollChanged {
            add => AddHandler(ScrollChangedEvent, value);
            remove => RemoveHandler(ScrollChangedEvent, value);
        }

        public void ScrollBackOnce() {
            switch (Orientation) {
                case Orientation.Horizontal:
                    LineLeft();
                    return;

                case Orientation.Vertical:
                    LineUp();
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ScrollForwardOnce() {
            switch (Orientation) {
                case Orientation.Horizontal:
                    LineRight();
                    return;

                case Orientation.Vertical:
                    LineDown();
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ScrollPageBackOnce() {
            switch (Orientation) {
                case Orientation.Horizontal:
                    PageLeft();
                    return;

                case Orientation.Vertical:
                    PageUp();
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ScrollPageForwardOnce() {
            switch (Orientation) {
                case Orientation.Horizontal:
                    PageRight();
                    return;

                case Orientation.Vertical:
                    PageDown();
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Scrolls the content up once with <see cref="ScrollSpeed"/>.
        /// </summary>
        public void LineUp() {
            Offset -= new Vector(0, ScrollSpeed);
        }

        /// <summary>
        /// Scrolls the content down once with <see cref="ScrollSpeed"/>.
        /// </summary>
        public void LineDown() {
            Offset += new Vector(0, ScrollSpeed);
        }

        /// <summary>
        /// Scrolls the content left once with <see cref="ScrollSpeed"/>.
        /// </summary>
        public void LineLeft() {
            Offset -= new Vector(ScrollSpeed, 0);
        }

        /// <summary>
        /// Scrolls the content right once with <see cref="ScrollSpeed"/>.
        /// </summary>
        public void LineRight() {
            Offset += new Vector(ScrollSpeed, 0);
        }


        /// <summary>
        /// Scrolls the content up once with <see cref="ScrollSpeed"/>.
        /// </summary>
        public void PageUp() {
            Offset -= new Vector(0, Viewport.Height);
        }

        /// <summary>
        /// Scrolls the content down once with <see cref="ScrollSpeed"/>.
        /// </summary>
        public void PageDown() {
            Offset += new Vector(0, Viewport.Height);
        }

        /// <summary>
        /// Scrolls the content left once with <see cref="ScrollSpeed"/>.
        /// </summary>
        public void PageLeft() {
            Offset -= new Vector(Viewport.Width, 0);
        }

        /// <summary>
        /// Scrolls the content right once with <see cref="ScrollSpeed"/>.
        /// </summary>
        public void PageRight() {
            Offset += new Vector(Viewport.Width, 0);
        }

        /// <summary>
        /// Scrolls to the top-left corner of the content.
        /// </summary>
        public void ScrollToHome() {
            Offset = new Vector(double.NegativeInfinity, double.NegativeInfinity);
        }

        /// <summary>
        /// Scrolls to the bottom-left corner of the content.
        /// </summary>
        public void ScrollToEnd() {
            Offset = new Vector(double.NegativeInfinity, double.PositiveInfinity);
        }

        protected override bool RegisterContentPresenter(IContentPresenter presenter) {
            _childSubscription?.Dispose();
            _childSubscription = null;

            if (!base.RegisterContentPresenter(presenter))
                return false;

            // BUG: Obsolete API?
            // _childSubscription = Presenter?
            //     .GetObservable(ContentPresenter.ChildProperty)
            //     .Subscribe(ChildChanged);
            return true;
        }

        protected override void OnPointerWheelChanged(PointerWheelEventArgs e) {
            var isHandle = HandleMouseWheel;

            if (!isHandle) {
                base.OnPointerWheelChanged(e);
                return;
            }

            var scrollY = e.Delta.Y;

            switch (scrollY) {
                case > 0:
                    ScrollBackOnce();
                    break;
                case < 0:
                    ScrollForwardOnce();
                    break;
            }

            e.Handled = true;
        }

        internal static Vector CoerceOffset(Size extent, Size viewport, Vector offset) {
            var maxX = Math.Max(extent.Width - viewport.Width, 0);
            var maxY = Math.Max(extent.Height - viewport.Height, 0);
            return new Vector(Clamp(offset.X, 0, maxX), Clamp(offset.Y, 0, maxY));
        }

        private static double Clamp(double value, double min, double max) {
            return (value < min) ? min : (value > max) ? max : value;
        }

        /*
        private static double Max(double x, double y)
        {
            var result = Math.Max(x, y);
            return double.IsNaN(result) ? 0 : result;
        }*/

        private void ChildChanged(Control? child) {
            CalculatedPropertiesChanged();
        }

        private void CalculatedPropertiesChanged() {
            switch (_orientation) {
                case Orientation.Horizontal:
                    CanScrollToStart = _offset.X > 0.0;
                    CanScrollToEnd = _offset.X < _extent.Width - _viewport.Width;
                    break;

                case Orientation.Vertical:
                    CanScrollToStart = _offset.Y > 0.0;
                    CanScrollToEnd = _offset.Y < _extent.Height - _viewport.Height;
                    break;
            }
        }

        /// <summary>
        /// Called when a change in scrolling state is detected, such as a change in scroll
        /// position, extent, or viewport size.
        /// </summary>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// If you override this method, call `base.OnScrollChanged(ScrollChangedEventArgs)` to
        /// ensure that this event is raised.
        /// </remarks>
        private void OnScrollChanged(ScrollChangedEventArgs e) {
            RaiseEvent(e);
        }

        private void OnLayoutUpdated(object sender, EventArgs e) => RaiseScrollChanged();

        private void RaiseScrollChanged() {
            var extentDelta = new Vector(Extent.Width - _oldExtent.Width, Extent.Height - _oldExtent.Height);
            var offsetDelta = Offset - _oldOffset;
            var viewportDelta = new Vector(Viewport.Width - _oldViewport.Width, Viewport.Height - _oldViewport.Height);

            if (extentDelta.NearlyEquals(default) &&
                offsetDelta.NearlyEquals(default) &&
                viewportDelta.NearlyEquals(default))
                return;

            var e = new ScrollChangedEventArgs(extentDelta, offsetDelta, viewportDelta);
            OnScrollChanged(e);

            _oldExtent = Extent;
            _oldOffset = Offset;
            _oldViewport = Viewport;
        }
    }
}
