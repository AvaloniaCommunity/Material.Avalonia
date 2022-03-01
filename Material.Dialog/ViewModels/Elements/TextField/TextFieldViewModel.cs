using System;
using Avalonia.Data;

namespace Material.Dialog.ViewModels.Elements.TextField
{
    public class TextFieldViewModel : DialogViewModelBase
    {
        private readonly TextFieldDialogViewModel _parent;

        public TextFieldDialogViewModel Parent => _parent;
        
        public TextFieldViewModel(TextFieldDialogViewModel parent, string defaultText = default, Func<string, Tuple<bool, string>> validateHandler = null)
        {
            _parent = parent;
            _text = defaultText;

            ValidateHandler = validateHandler;
            
            var result = DoValidate(defaultText);
            IsValid = result.Item1;
        }
        
        public event EventHandler<bool> OnValidateRequired;

        public Func<string, Tuple<bool, string>> ValidateHandler;

        private string _placeholderText;
        public string PlaceholderText { get => _placeholderText; set { _placeholderText = value; OnPropertyChanged(); } }

        private string _text;
        public string Text { get => _text; set { _text = value; OnPropertyChanged(); OnTextChanged(value); } }

        private string _classes;
        public string Classes { get => _classes; set { _classes = value; OnPropertyChanged(); } }

        private string _label;
        public string Label { get => _label; set { _label = value; OnPropertyChanged(); } }

        private char _maskChar;
        public char MaskChar { get => _maskChar; set { _maskChar = value; OnPropertyChanged(); } }

        private int _maxCountChars;
        public int MaxCountChars { get => _maxCountChars; set { _maxCountChars = value; OnPropertyChanged(); } }

        private bool _isValid;
        public bool IsValid { get => _isValid; set { _isValid = value; OnPropertyChanged(); } }

        private string _assistiveText;
        public string AssistiveText { get => _assistiveText; set { _assistiveText = value; OnPropertyChanged(); } }

        private void OnTextChanged(string text)
        {
            var result = DoValidate(text);

            var isSuccessful = result.Item1;
            
            IsValid = isSuccessful;
            OnValidateRequired?.Invoke(this, true);

            if (!isSuccessful)
                throw new DataValidationException(result.Item2);
        }

        private Tuple<bool, string> DoValidate(string text)
        {
            try
            {
                return ValidateHandler?.Invoke(text) ?? new Tuple<bool, string>(true, null);
            }
            catch(Exception e)
            {
                return new Tuple<bool, string>(false, e.Message);
            }
        }
    }
}
