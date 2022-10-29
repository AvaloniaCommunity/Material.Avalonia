using System;
using System.Windows.Input;

namespace Material.Styles.Models
{
    public class SnackbarModel
    {
        public SnackbarModel(object content)
        {
            _content = content;
        }

        public SnackbarModel(object content, TimeSpan? duration) :
            this(content)
        {
            if(duration.HasValue)
                _duration = duration.Value;
        }
        
        public SnackbarModel(object content, TimeSpan? duration, SnackbarButtonModel button) :
            this(content, duration)
        {
            _button = button;
        }

        private ICommand? _buttonCommand;

        private readonly object _content;
        public object Content => _content;

        private readonly SnackbarButtonModel? _button;
        public SnackbarButtonModel? Button => _button;

        public ICommand? Command
        {
            get => _buttonCommand;
            internal set => _buttonCommand = value;
        }

        // Setting duration to TimeSpan.Zero, will make it stay forever/til you manually delete it 
        private readonly TimeSpan _duration = TimeSpan.FromSeconds(5);
        public TimeSpan Duration => _duration;
    }
}