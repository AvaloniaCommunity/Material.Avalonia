using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Material.Dialog.Bases;
using Material.Dialog.Enums;
using Material.Dialog.Icons;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;
using Material.Dialog.ViewModels.Elements;
using Material.Dialog.ViewModels.Elements.Header.Icons;
using Material.Dialog.ViewModels.Elements.TextField;
using Material.Dialog.Views;

namespace Material.Dialog
{
    public class DialogHelper
    {
        public const string DIALOG_RESULT_OK = "ok";
        public const string DIALOG_RESULT_CANCEL = "cancel";
        public const string DIALOG_RESULT_YES = "yes";
        public const string DIALOG_RESULT_NO = "no";
        public const string DIALOG_RESULT_ABORT = "abort";

        private static bool _disableTransitions;

        public static bool DisableTransitions
        {
            get => _disableTransitions;
            set => _disableTransitions = value;
        }

        /// <summary>
        /// Create some dialog buttons for use.
        /// </summary>
        /// <param name="cases">Which dialog buttons group case you need.</param> 
        public static DialogButton[] CreateSimpleDialogButtons(DialogButtonsEnum cases)
        {
            switch (cases)
            {
                default:
                case DialogButtonsEnum.Ok:
                    return new[]
                    {
                        new DialogButton {Result = DIALOG_RESULT_OK, Content = "OK"}
                    };
                case DialogButtonsEnum.OkAbort:
                    return new[]
                    {
                        new DialogButton {Result = DIALOG_RESULT_ABORT, Content = "ABORT"},
                        new DialogButton {Result = DIALOG_RESULT_OK, Content = "OK"}
                    };
                case DialogButtonsEnum.OkCancel:
                    return new[]
                    {
                        new DialogButton {Result = DIALOG_RESULT_CANCEL, Content = "CANCEL"},
                        new DialogButton {Result = DIALOG_RESULT_OK, Content = "OK"}
                    };
                case DialogButtonsEnum.YesNo:
                    return new[]
                    {
                        new DialogButton {Result = DIALOG_RESULT_NO, Content = "NO"},
                        new DialogButton {Result = DIALOG_RESULT_YES, Content = "YES"}
                    };
                case DialogButtonsEnum.YesNoAbort:
                    return new[]
                    {
                        new DialogButton {Result = DIALOG_RESULT_ABORT, Content = "ABORT"},
                        new DialogButton {Result = DIALOG_RESULT_NO, Content = "NO"},
                        new DialogButton {Result = DIALOG_RESULT_YES, Content = "YES"}
                    };
                case DialogButtonsEnum.YesNoCancel:
                    return new[]
                    {
                        new DialogButton {Result = DIALOG_RESULT_CANCEL, Content = "CANCEL"},
                        new DialogButton {Result = DIALOG_RESULT_NO, Content = "NO"},
                        new DialogButton {Result = DIALOG_RESULT_YES, Content = "YES"}
                    };
            }
        }

        public static IDialogWindow<DialogResult> CreateAlertDialog(AlertDialogBuilderParams @params)
        {
            var window = new AlertDialog();
            var context = new AlertDialogViewModel(window);

            ApplyBaseParams(context, @params);

            context.DialogButtons ??= new ObservableCollection<DialogButtonViewModel>(
                CreateObsoleteButtonArray(context, CreateSimpleDialogButtons(DialogButtonsEnum.Ok)));

            window.DataContext = context;
            SetupWindowParameters(window, @params);
            return new DialogWindowBase<AlertDialog, DialogResult>(window);
        }

        public static IDialogWindow<TextFieldDialogResult> CreateTextFieldDialog(TextFieldDialogBuilderParams @params)
        {
            var window = new TextFieldDialog();
            var context = new TextFieldDialogViewModel(window);

            context.TextFields =
                new ObservableCollection<TextFieldViewModel>(TextFieldsBuilder(context, @params.TextFields));

            ApplyBaseParams(context, @params);

            var positiveButtonApplied = false;
            var buttons = CreateObsoleteButtonArray(context, @params.DialogButtons);

            foreach (var button in buttons)
            {
                if (!button.IsPositiveButton)
                    continue;

                button.Command = context.SubmitCommand;
                positiveButtonApplied = true;
            }

            context.DialogButtons = new ObservableCollection<DialogButtonViewModel>(buttons);

            // TODO: Remove compatibility API with PositiveButton and NegativeButton on future update.
            if (!positiveButtonApplied)
            {
                var positiveButton = @params.PositiveButton;
                if (positiveButton != null)
                {
                    context.DialogButtons.Add(
                        new ObsoleteDialogButtonViewModel(context, positiveButton.Content, positiveButton.Result)
                        {
                            Command = context.SubmitCommand
                        });
                }
            }

            context.BindValidateHandler();
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
                FirstField = (ushort) @params.ImplicitValue.Hours,
                SecondField = (ushort) @params.ImplicitValue.Minutes,
            };
            ApplyBaseParams(context, @params);

            context.DialogButtons =
                new ObservableCollection<DialogButtonViewModel>(CreateObsoleteButtonArray(context,
                    @params.NegativeButton,
                    @params.PositiveButton));

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
        //[Obsolete("This feature is still not ready for use! Please come back later!")]
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

            context.DialogButtons =
                new ObservableCollection<DialogButtonViewModel>(CreateObsoleteButtonArray(context,
                    @params.NegativeButton,
                    @params.PositiveButton));

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
                ContentTemplate = @params.ContentTemplate
            };

            ApplyBaseParams(context, @params);

            window.DataContext = context;
            SetupWindowParameters(window, @params);
            return new DialogWindowBase<CustomDialog, DialogResult>(window);
        }

        private static void ApplyBaseParams<T>(T input, DialogWindowBuilderParamsBase @params)
            where T : DialogWindowViewModel
        {
            input.MaxWidth = @params.MaxWidth;
            input.WindowTitle = @params.WindowTitle;
            input.Width = @params.Width;
            input.ContentHeader = @params.ContentHeader;
            input.ContentMessage = @params.SupportingText;
            input.Borderless = @params.Borderless;
            input.WindowStartupLocation = @params.StartupLocation;

            switch (@params.DialogIcon)
            {
                case Bitmap bitmap:
                {
                    input.DialogIcon = new ImageIconViewModel
                    {
                        Bitmap = bitmap
                    };
                }
                    break;

                case Image _:
                    throw new ArgumentException("Do not wrap Bitmap object with Image control for now.");

                case Control _:
                    throw new NotImplementedException("Custom view icon feature is currently unavailable.");

                case DialogIconKind kind:
                {
                    if (@params.DialogHeaderIcon != null)
                    {
                        input.DialogIcon = new DialogIconViewModel
                        {
                            Kind = kind
                        };
                    }
                }
                    break;

                case null:
                    break;

                default:
                    throw new ArgumentException($"{@params.DialogIcon.GetType()} is a unknown or unsupported type.");
            }

            // Rollback API Compatibility
            if (@params.DialogHeaderIcon != null)
            {
                if (input.DialogIcon == null && @params.DialogHeaderIcon != null)
                {
                    input.DialogIcon = new DialogIconViewModel
                    {
                        Kind = @params.DialogHeaderIcon.Value
                    };
                }
            }

            if (@params.DialogButtons != null)
                input.DialogButtons =
                    new ObservableCollection<DialogButtonViewModel>(
                        CreateObsoleteButtonArray(input, @params.DialogButtons));

            if (@params.NeutralDialogButtons != null)
                input.NeutralDialogButton =
                    new ObservableCollection<DialogButtonViewModel>(
                        CreateObsoleteButtonArray(input, @params.NeutralDialogButtons));

            input.ButtonsStackOrientation = @params.ButtonsOrientation;
        }

        private static void SetupWindowParameters(Window window, DialogWindowBuilderParamsBase @params)
        {
            window.SystemDecorations = @params.Borderless ? SystemDecorations.None : SystemDecorations.Full;
            (window as IHasNegativeResult)?.SetNegativeResult(@params.NegativeResult);
        }

        private static DialogButtonViewModel[] CreateObsoleteButtonArray(DialogWindowViewModel parent,
            params DialogButton[] buttons)
        {
            var len = buttons.Length;
            var result = new DialogButtonViewModel[buttons.Length];

            for (var i = 0; i < len; i++)
            {
                var button = buttons[i];

                if (button is null)
                    continue;

                result[i] = new ObsoleteDialogButtonViewModel(parent, button.Content, button.Result)
                {
                    IsPositiveButton = button.IsPositive
                };
            }

            return result;
        }

        private static TextFieldViewModel[] TextFieldsBuilder(TextFieldDialogViewModel parent,
            params TextFieldBuilderParams[] @params)
        {
            var len = @params.Length;
            var result = new TextFieldViewModel[len];
            for (var i = 0; i < len; i++)
            {
                var param = @params[i];

                try
                {
                    var model = new TextFieldViewModel(parent, param.DefaultText, param.Validater)
                    {
                        // but... I implemented an setter to TextFieldDialog for apply classes when showing dialog.
                        // Currently AvaloniaUI are not supported to binding classes.
                        Classes = param.Classes,
                        PlaceholderText = param.PlaceholderText,
                        MaxCountChars = param.MaxCountChars,
                        Label = param.Label,
                        AssistiveText = param.HelperText
                    };

                    switch (param.FieldKind)
                    {
                        case TextFieldKind.Masked:
                            model.MaskChar = param.MaskChar;
                            model.Classes += " revealPasswordButton";
                            break;
                        case TextFieldKind.WithClear:
                            model.Classes += " clearButton";
                            break;
                        case TextFieldKind.Normal:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    result[i] = model;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    // ignored
                }
            }

            return result;
        }
    }
}