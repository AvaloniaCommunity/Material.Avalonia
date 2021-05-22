using Avalonia.Layout;
using Avalonia.Threading;
using Material.Dialog.Commands;
using Material.Dialog.ViewModels.TextField;
using Material.Dialog.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog.ViewModels
{
    public class TextFieldDialogViewModel : DialogWindowViewModel
    {
        private TextFieldDialog _window;

        public bool IsReady;

        private TextFieldViewModel[] m_TextFields;
        public TextFieldViewModel[] TextFields { get => m_TextFields; internal set => m_TextFields = value; }

        private DialogResultButton m_PositiveButton;
        public DialogResultButton PositiveButton { get => m_PositiveButton; internal set => m_PositiveButton = value; }

        private DialogResultButton m_NegativeButton;
        public DialogResultButton NegativeButton { get => m_NegativeButton; internal set => m_NegativeButton = value; }
        
        public TextFieldDialogViewModel(TextFieldDialog dialog)
        {
            _window = dialog;
            ButtonClick = new MaterialDialogRelayCommand(OnPressButton, CanPressButton);
        }

        public void BindValidater()
        {
            foreach (var item in TextFields)
                item.OnValidateRequired += Field_OnValidateRequired;
        }

        private void Field_OnValidateRequired(object sender, bool e)
        {
            ButtonClick.RaiseCanExecute();
        }

        public void UnbindValidater()
        {
            foreach (var item in TextFields)
                item.OnValidateRequired -= Field_OnValidateRequired;
        }

        public bool ValidateFields()
        {
            foreach (var field in TextFields)
            {
                if (!field.IsValid)
                    return false;
            }
            return true;
        }

        public bool CanPressButton(object args)
        {
            if(args == PositiveButton)
            {
                return ValidateFields();
            }
            else if(args == NegativeButton)
            {
                return true;
            }
            return false;
        }
        public async void OnPressButton(object args)
        {
            var button = args as DialogResultButton;
            if (button is null)
                return; 

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var result = new TextFieldDialogResult() { result = button.Result };
                var fields = new List<TextFieldResult>();
                foreach (var item in TextFields)
                    fields.Add(new TextFieldResult { Text = item.Text });
                result.fieldsResult = fields.ToArray();
                _window.Result = result;
                _window.Close();
                UnbindValidater();
            });
        }

        public MaterialDialogRelayCommand ButtonClick { get; private set; }
    }
}
