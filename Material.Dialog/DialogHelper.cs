using System;
using Avalonia.Controls;
using Material.Dialog.Bases;
using Material.Dialog.Enums;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;
using Material.Dialog.ViewModels.TextField;
using Material.Dialog.Views;
using System.Collections.Generic;

namespace Material.Dialog
{
    public class DialogHelper
    {
        public const string DIALOG_RESULT_OK = "ok";
        public const string DIALOG_RESULT_CANCEL = "cancel";
        public const string DIALOG_RESULT_YES = "yes";
        public const string DIALOG_RESULT_NO = "no";
        public const string DIALOG_RESULT_ABORT = "abort";

        /// <summary>
        /// Create some dialog buttons for use.
        /// </summary>
        /// <param name="cases">Which dialog buttons group case you need.</param> 
        public static DialogResultButton[] CreateSimpleDialogButtons(DialogButtonsEnum cases)
        {
            switch (cases)
            {
                default:
                case DialogButtonsEnum.Ok:
                    return new DialogResultButton[]
                    {
                        new DialogResultButton { Result = DIALOG_RESULT_OK, Content = "OK" }
                    };
                case DialogButtonsEnum.OkAbort:
                    return new DialogResultButton[]
                    {
                        new DialogResultButton { Result = DIALOG_RESULT_ABORT, Content = "ABORT" },
                        new DialogResultButton { Result = DIALOG_RESULT_OK, Content = "OK" },
                    };
                case DialogButtonsEnum.OkCancel:
                    return new DialogResultButton[]
                    {
                        new DialogResultButton { Result = DIALOG_RESULT_CANCEL, Content = "CANCEL" },
                        new DialogResultButton { Result = DIALOG_RESULT_OK, Content = "OK" },
                    };
                case DialogButtonsEnum.YesNo:
                    return new DialogResultButton[]
                    {
                        new DialogResultButton { Result = DIALOG_RESULT_NO, Content = "NO" },
                        new DialogResultButton { Result = DIALOG_RESULT_YES, Content = "YES" },
                    };
                case DialogButtonsEnum.YesNoAbort:
                    return new DialogResultButton[]
                    {
                        new DialogResultButton { Result = DIALOG_RESULT_ABORT, Content = "ABORT" },
                        new DialogResultButton { Result = DIALOG_RESULT_NO, Content = "NO" },
                        new DialogResultButton { Result = DIALOG_RESULT_YES, Content = "YES" },
                    };
                case DialogButtonsEnum.YesNoCancel:
                    return new DialogResultButton[]
                    {
                        new DialogResultButton { Result = DIALOG_RESULT_CANCEL, Content = "CANCEL" },
                        new DialogResultButton { Result = DIALOG_RESULT_NO, Content = "NO" },
                        new DialogResultButton { Result = DIALOG_RESULT_YES, Content = "YES" },
                    };
            }
        }
        
        public static IDialogWindow<DialogResult> CreateAlertDialog(AlertDialogBuilderParams @params)
        { 
            var window = new AlertDialog();
            var context = new AlertDialogViewModel(window)
            {
                DialogButtons = @params.DialogButtons,
                ButtonsStackOrientation = @params.ButtonsOrientation,
            };
            ApplyBaseParams(context, @params);
            window.DataContext = context;
            window.SystemDecorations = @params.Borderless ? SystemDecorations.None : SystemDecorations.Full;
            window.SetNegativeResult(@params.NegativeResult);
            return new DialogWindowBase<AlertDialog, DialogResult>(window);
        }

        public static IDialogWindow<TextFieldDialogResult> CreateTextFieldDialog(TextFieldDialogBuilderParams @params)
        {
            var window = new TextFieldDialog();
            var context = new TextFieldDialogViewModel(window)
            {
                PositiveButton = @params.PositiveButton,
                NegativeButton = @params.NegativeButton, 
                ButtonsStackOrientation = @params.ButtonsOrientation,
                TextFields = TextFieldsBuilder(@params.TextFields),
                DialogButtons = CombineButtons(@params.NegativeButton, @params.PositiveButton),
            };
            ApplyBaseParams(context, @params);
            context.BindValidater();
            window.DataContext = context;
            window.SystemDecorations = @params.Borderless ? SystemDecorations.None : SystemDecorations.Full;
            window.SetNegativeResult(@params.NegativeResult);
            return new DialogWindowBase<TextFieldDialog, TextFieldDialogResult>(window);
        }

        [Obsolete("This feature is still preparing, do not use it early!")]
        public static IDialogWindow<DateTimePickerDialogResult> CreateTimePicker(DateTimePickerDialogBuilderParams @params)
        {
            var window = new TimePickerDialog();
            var context = new TimePickerDialogViewModel(window)
            {
                PositiveButton = @params.PositiveButton,
                NegativeButton = @params.NegativeButton,
                DialogButtons = CombineButtons(@params.NegativeButton, @params.PositiveButton),
            };
            ApplyBaseParams(context, @params);
            window.DataContext = context;
            window.SystemDecorations = @params.Borderless ? SystemDecorations.None : SystemDecorations.Full;
            window.SetNegativeResult(@params.NegativeResult);
            return new DialogWindowBase<TimePickerDialog, DateTimePickerDialogResult>(window);
        }

        private static void ApplyBaseParams<T> (T input, DialogWindowBuilderParamsBase @params) where T : DialogWindowViewModel
        {
            input.MaxWidth = @params.MaxWidth;
            input.WindowTitle = @params.WindowTitle;
            input.Width = @params.Width;
            input.ContentHeader = @params.ContentHeader;
            input.ContentMessage = @params.SupportingText;
            input.Borderless = @params.Borderless;
            input.WindowStartupLocation = @params.StartupLocation;
            input.DialogHeaderIcon = @params.DialogHeaderIcon;
        }

        private static DialogResultButton[] CombineButtons(params DialogResultButton[] buttons) 
        {
            List<DialogResultButton> result = new List<DialogResultButton>();
            foreach(var button in buttons)
            {
                if (button is null)
                    continue;
                result.Add(button);
            }
            return result.ToArray();
        }

        private static TextFieldViewModel[] TextFieldsBuilder(TextFieldBuilderParams[] @params)
        {
            List<TextFieldViewModel> result = new List<TextFieldViewModel>();
            foreach(var param in @params)
            {
                try
                {
                    TextFieldViewModel model = new TextFieldViewModel();

                    // Currently AvaloniaUI are not supported to binding classes.
                    //model.Classes = param.Classes;
                    
                    model.PlaceholderText = param.PlaceholderText;
                    model.MaxCountChars = param.MaxCountChars; 
                    model.Label = param.Label;
                    model.Validater = param.Validater;
                    model.Text = param.DefaultText;
                    switch (param.FieldKind)
                    {
                        case TextFieldKind.Normal: 
                            break;
                        case TextFieldKind.Masked:
                            model.MaskChar = param.MaskChar;
                            model.Classes += " revealPasswordButton";
                            break;
                        case TextFieldKind.WithClear: 
                            model.Classes += " clearButton";
                            break;
                    }
                    result.Add(model);
                }
                catch
                {

                }
            }
            return result.ToArray();
        }
    }
}
