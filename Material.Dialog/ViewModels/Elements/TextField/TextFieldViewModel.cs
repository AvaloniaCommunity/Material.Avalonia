using System;
using Avalonia.Data;

namespace Material.Dialog.ViewModels.Elements.TextField
{
    public class TextFieldViewModel : DialogViewModelBase
    {
        public event EventHandler<bool> OnValidateRequired;

        public Func<string, Tuple<bool, string>> Validater;

        public TextFieldDialogViewModel Parent;
        
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
            var result = new Tuple<bool, string>(false, null);
            try
            {
                result = Validater?.Invoke(text) ?? new Tuple<bool, string>(true, null);
            }
            catch(Exception e)
            {
                result = new Tuple<bool, string>(false, e.Message);
            }
            IsValid = result.Item1;
            OnValidateRequired?.Invoke(this, true);

            if (!Parent.IsReady) 
                return;
            
            if (!IsValid)
                throw new DataValidationException(result.Item2);
        }
    }
}
