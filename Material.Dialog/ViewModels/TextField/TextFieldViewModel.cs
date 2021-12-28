using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Avalonia.Data;

namespace Material.Dialog.ViewModels.TextField
{
    public class TextFieldViewModel : DialogViewModelBase
    {
        public event EventHandler<bool> OnValidateRequired;

        public Func<string, Tuple<bool, string>> Validater;

        public TextFieldDialogViewModel Parent;
        
        private string m_PlaceholderText;
        public string PlaceholderText { get => m_PlaceholderText; set { m_PlaceholderText = value; OnPropertyChanged(); } }

        private string m_Text;
        public string Text { get => m_Text; set { m_Text = value; OnPropertyChanged(); OnTextChanged(value); } }

        private string m_Classes;
        public string Classes { get => m_Classes; set { m_Classes = value; OnPropertyChanged(); } }

        private string m_Label;
        public string Label { get => m_Label; set { m_Label = value; OnPropertyChanged(); } }

        private char m_MaskChar;
        public char MaskChar { get => m_MaskChar; set { m_MaskChar = value; OnPropertyChanged(); } }

        private int m_MaxCountChars;
        public int MaxCountChars { get => m_MaxCountChars; set { m_MaxCountChars = value; OnPropertyChanged(); } }

        private bool m_IsValid;
        public bool IsValid { get => m_IsValid; set { m_IsValid = value; OnPropertyChanged(); } }

        private string _assistiveText;
        public string AssistiveText { get => _assistiveText; set { _assistiveText = value; OnPropertyChanged(); } }

        private void OnTextChanged(string text)
        {
            Tuple<bool, string> result = new Tuple<bool, string>(false, null);
            try
            {
                result = Validater?.Invoke(text) ?? new Tuple<bool, string>(true, null);
            }
            catch(Exception e)
            {
                result = new Tuple<bool, string>(false, e.Message);
            }
            IsValid = result.Item1;
            //Error = result.Item2;
            OnValidateRequired?.Invoke(this, true);

            if(Parent.IsReady)
                if (!IsValid)
                    throw new DataValidationException(result.Item2);
        }
    }
}
