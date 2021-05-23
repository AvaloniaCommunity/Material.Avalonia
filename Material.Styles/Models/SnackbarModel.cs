using System;
using Avalonia.Layout;

namespace Material.Styles.Models
{
    public class SnackbarModel
    {
        public SnackbarModel(object content, Orientation orientation = Orientation.Horizontal)
        {
            _content = content;
            _orientation = orientation;
            //_button = button;
        }
        
        private object _content;
        public object Content => _content;

        private Orientation _orientation;
        public Orientation Orientation => _orientation;

        // Still not sure I should use control instead of method call
        // because I want more flexible button property on snackbar but
        // that means we cant dismiss snackbar after pressed button.
        private object _button;
        public object Button => _button;

        private TimeSpan _duration = TimeSpan.FromSeconds(5);
        public TimeSpan Duration => _duration;
    }
}