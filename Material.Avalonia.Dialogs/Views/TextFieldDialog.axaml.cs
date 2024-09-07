using System;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Threading;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views {
    public partial class TextFieldDialog : Window, IDialogWindowResult<TextFieldDialogResult>, IHasNegativeResult {
        public TextFieldDialog() {
            Result = new TextFieldDialogResult();

            InitializeComponent();

            Closed += TextFieldDialog_Closed;
            Opened += TextFieldDialog_Opened;
        }
        public TextFieldDialogResult Result { get; set; }

        public TextFieldDialogResult GetResult() {
            if (DataContext is not TextFieldDialogViewModel viewModel)
                return null;

            return viewModel.DialogResult switch {
                TextFieldDialogResult vm => vm,
                // ReSharper disable once ConvertTypeCheckPatternToNullCheck
                DialogResult basicViewModel => new TextFieldDialogResult(basicViewModel.GetResult,
                    Array.Empty<TextFieldResult>()),
                _ => null
            };
        }

        public void SetNegativeResult(DialogResult result) => Result._result = result.GetResult;

        private void TextFieldDialog_Closed(object sender, EventArgs e) {
            Opened -= TextFieldDialog_Opened;
            Closed -= TextFieldDialog_Closed;
        }

        private void TextFieldDialog_Opened(object sender, EventArgs e) {
            if (!(DataContext is TextFieldDialogViewModel vm))
                return;

            //vm.ButtonClick.RaiseCanExecute();

            var fields = this.Find<ItemsControl>("PART_Fields");

            Dispatcher.UIThread.InvokeAsync(delegate {
                if (fields is null)
                    return;

                int index = 0;
                foreach (var item in fields.GetRealizedContainers()) {
                    var fieldViewModel = vm.TextFields[index];

                    // TODO: Check if this works fine due to container generator changes
                    if (item is ContentPresenter presenter) {
                        if (presenter.Child is TextBox field) {
                            var classes = fieldViewModel.Classes;
                            if (classes != null) {
                                foreach (var @class in classes.Split(' ')) {
                                    if (@class != "")
                                        field.Classes.Add(@class);
                                }
                            }
                        }
                    }

                    index++;
                }
            });
        }
    }
}