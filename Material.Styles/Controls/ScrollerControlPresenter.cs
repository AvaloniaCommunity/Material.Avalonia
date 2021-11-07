using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.VisualTree;

#nullable enable

namespace Material.Styles.Controls
{
    /// <summary>
    /// Presents a scrolling view of content inside a <see cref="Scroller"/>.
    /// </summary>
    public class ScrollerContentPresenter : ScrollContentPresenter
    {
        protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
        {
            // Scroll Horizontally default when content width greater than viewport width
            // and the height of content is less equal than viewport height
            if (Extent.Width > Viewport.Width)
            {
                var scrollable = Child as ILogicalScrollable;
                bool isLogical = scrollable?.IsLogicalScrollEnabled == true;

                double x = Offset.X;
                double y = Offset.Y;

                double width = isLogical ? scrollable!.ScrollSize.Width : 50;
                x += -e.Delta.Y * width;
                x = Math.Max(x, 0);
                x = Math.Min(x, Extent.Width - Viewport.Width);

                Offset = new Vector(x, y);
                e.Handled = true;
            }
            
            /*if (Extent.Height > Viewport.Height || Extent.Width > Viewport.Width)
            {
                var scrollable = Child as ILogicalScrollable;
                bool isLogical = scrollable?.IsLogicalScrollEnabled == true;

                double x = Offset.X;
                double y = Offset.Y;

                if (Extent.Height > Viewport.Height)
                {
                    double height = isLogical ? scrollable!.ScrollSize.Height : 50;
                    y += -e.Delta.Y * height;
                    y = Math.Max(y, 0);
                    y = Math.Min(y, Extent.Height - Viewport.Height);
                }

                if (Extent.Width > Viewport.Width)
                {
                    double width = isLogical ? scrollable!.ScrollSize.Width : 50;
                    x += -e.Delta.X * width;
                    x = Math.Max(x, 0);
                    x = Math.Min(x, Extent.Width - Viewport.Width);
                }

                Offset = new Vector(x, y);
                e.Handled = true;
            }*/
        }
    }
}
