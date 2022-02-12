using Avalonia.Threading;
using Material.Dialog.Commands;
using Material.Dialog.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Material.Dialog.ViewModels.Elements.TextField;

namespace Material.Dialog.ViewModels
{
    public class TextFieldDialogViewModel : DialogWindowViewModel
    {
        public bool IsReady;

        private ObservableCollection<TextFieldViewModel> _textFields;

        public ObservableCollection<TextFieldViewModel> TextFields
        {
            get => _textFields;
            internal set
            {
                _textFields = value;
                OnPropertyChanged();
            }
        }

        private DialogResultButton _positiveButton;

        public DialogResultButton PositiveButton
        {
            get => _positiveButton;
            internal set => _positiveButton = value;
        }

        private DialogResultButton _negativeButton;

        public DialogResultButton NegativeButton
        {
            get => _negativeButton;
            internal set => _negativeButton = value;
        }

        public TextFieldDialogViewModel(TextFieldDialog dialog)
        {
            _window = dialog;
            //ButtonClick = new MaterialDialogRelayCommand(OnPressButton, CanPressButton);
        }

        public void BindValidater()
        {
            foreach (var item in TextFields)
                item.OnValidateRequired += Field_OnValidateRequired;
        }

        private void Field_OnValidateRequired(object sender, bool e)
        {
            //ButtonClick.RaiseCanExecute();
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
            if (args == PositiveButton)
            {
                return ValidateFields();
            }
            else if (args == NegativeButton)
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
                var result = new TextFieldDialogResult() {result = button.Result};
                var fields = new List<TextFieldResult>();
                foreach (var item in TextFields)
                    fields.Add(new TextFieldResult {Text = item.Text});
                result.fieldsResult = fields.ToArray();
                //Result = result;
                _window.Close();
                UnbindValidater();
            });
        }
    }
}