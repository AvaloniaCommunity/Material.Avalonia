using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog.ViewModels.TextField
{
    public class TextFieldViewModel : ViewModelBase
    {
        public event EventHandler<bool> OnValidateRequired;

        public Func<string, bool> Validater;

        private string m_PlaceholderText;
        public string PlaceholderText { get => m_PlaceholderText; set { m_PlaceholderText = value; OnPropertyChanged(); } }

        private string m_Text;
        public string Text { get => m_Text; set { m_Text = value; OnTextChanged(value); OnPropertyChanged(); } }

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

        private void OnTextChanged(string text)
        {
            bool result = false;
            try
            {
                result = Validater?.Invoke(text) ?? true;
            }
            catch
            {
                result = false;
            }
            IsValid = result;
            OnValidateRequired?.Invoke(this, result);
        }
    }
}
