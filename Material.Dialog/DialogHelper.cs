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

        private static bool disableTransitions;
        public static bool DisableTransitions
        {
            get => disableTransitions;
            set => disableTransitions = value;
        }

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
            var context = new AlertDialogViewModel(window) { };

            ApplyBaseParams(context, @params);
            
            if (context.DialogButtons == null)
            {
                context.DialogButtons = CreateSimpleDialogButtons(DialogButtonsEnum.Ok);
            }
            
            window.DataContext = context;
            SetupWindowParameters(window, @params);
            return new DialogWindowBase<AlertDialog, DialogResult>(window);
        }

        public static IDialogWindow<TextFieldDialogResult> CreateTextFieldDialog(TextFieldDialogBuilderParams @params)
        {
            var window = new TextFieldDialog();
            var context = new TextFieldDialogViewModel(window)
            {
                PositiveButton = @params.PositiveButton,
                NegativeButton = @params.NegativeButton, 
                TextFields = TextFieldsBuilder(@params.TextFields),
            };
            
            ApplyBaseParams(context, @params);
            
            context.DialogButtons = CombineButtons(@params.NegativeButton, @params.PositiveButton);
            
            context.BindValidater();
            window.DataContext = context;
            SetupWindowParameters(window, @params);
            return new DialogWindowBase<TextFieldDialog, TextFieldDialogResult>(window);
        }

        /// <summary>
        /// Create time picker dialog.
        /// </summary>
        /// <param name="params">Parameters of building dialog</param>
        /// <returns>Instance of picker.</returns>
        public static IDialogWindow<DateTimePickerDialogResult> CreateTimePicker(TimePickerDialogBuilderParams @params)
        {
            var window = new TimePickerDialog();
            var context = new TimePickerDialogViewModel(window)
            {
                PositiveButton = @params.PositiveButton,
                NegativeButton = @params.NegativeButton,
                FirstField = (ushort)@params.ImplicitValue.Hours,
                SecondField = (ushort)@params.ImplicitValue.Minutes,
            };
            ApplyBaseParams(context, @params);

            context.DialogButtons = CombineButtons(@params.NegativeButton, @params.PositiveButton);

            if (context.Width is null || context.Width < 320)
                context.Width = 320;
            
            window.AttachViewModel(context);
            SetupWindowParameters(window, @params);
            return new DialogWindowBase<TimePickerDialog, DateTimePickerDialogResult>(window);
        }
        
        /// <summary>
        /// Create date picker dialog.
        /// </summary>
        /// <param name="params">Parameters of building dialog</param>
        /// <returns>Instance of picker.</returns>
        [Obsolete("This feature is still not ready for use! Please come back later!")]
        public static IDialogWindow<DateTimePickerDialogResult> CreateDatePicker(DatePickerDialogBuilderParams @params)
        {
            var window = new DatePickerDialog();
            var context = new DatePickerDialogViewModel(window)
            {
                PositiveButton = @params.PositiveButton,
                NegativeButton = @params.NegativeButton,
                DateTime = @params.ImplicitValue
            };
            ApplyBaseParams(context, @params);

            context.DialogButtons = CombineButtons(@params.NegativeButton, @params.PositiveButton);

            if (context.Width is null || context.Width < 320)
                context.Width = 320;
            
            window.AttachViewModel(context);
            SetupWindowParameters(window, @params);
            return new DialogWindowBase<DatePickerDialog, DateTimePickerDialogResult>(window);
        }

        /// <summary>
        /// Create an dialog with custom content or dummy dialog.
        /// </summary>
        /// <param name="params">Parameters of building dialog</param>
        /// <returns>Instance of dialog.</returns>
        public static IDialogWindow<DialogResult> CreateCustomDialog(CustomDialogBuilderParams @params)
        {
            var window = new CustomDialog();
            var context = new CustomDialogViewModel(window)
            {
                Content = @params.Content,
            };
            
            ApplyBaseParams(context, @params);
            
            window.DataContext = context;
            SetupWindowParameters(window, @params);
            return new DialogWindowBase<CustomDialog, DialogResult>(window);
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

            input.DialogButtons = @params.DialogButtons;
            input.ButtonsStackOrientation = @params.ButtonsOrientation;
        }

        private static void SetupWindowParameters(Window window, DialogWindowBuilderParamsBase @params)
        {
            window.SystemDecorations = @params.Borderless ? SystemDecorations.None : SystemDecorations.Full;
            (window as IHasNegativeResult)?.SetNegativeResult(@params.NegativeResult);
        }

        private static DialogResultButton[] CombineButtons(params DialogResultButton[] buttons) 
        {
            var result = new List<DialogResultButton>();
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
            var result = new List<TextFieldViewModel>();
            foreach(var param in @params)
            {
                try
                {
                    var model = new TextFieldViewModel();

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
