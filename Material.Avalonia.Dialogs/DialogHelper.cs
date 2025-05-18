using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Material.Dialog.Bases;
using Material.Dialog.Enums;
using Material.Dialog.Extensions;
using Material.Dialog.Icons;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;
using Material.Dialog.ViewModels.Elements;
using Material.Dialog.ViewModels.Elements.Header.Icons;
using Material.Dialog.ViewModels.Elements.TextField;
using Material.Dialog.Views;

namespace Material.Dialog
{
    
    [Obsolete("Please consider to use DialogBuilder API instead. Those API will be deprecated and get removed.", false)]
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
        
        [Obsolete("Please consider to use DialogBuilder API instead. Those API will be deprecated and get removed.", false)]
        public static IDialogWindow<IDialogResult> CreateAlertDialog(AlertDialogBuilderParams @params) {
            var builder = new DialogBuilder();

            ApplyBaseParamsDialogBuilder(@params, builder);

            return builder.Build().GetCompatObject();
            
            /*
            
            var window = new AlertDialog();
            var context = new AlertDialogViewModel(window);

            ApplyBaseParams(context, @params);

            context.DialogButtons ??= new ObservableCollection<DialogButtonViewModel>(
                CreateObsoleteButtonArray(context, CreateSimpleDialogButtons(DialogButtonsEnum.Ok)));

            window.DataContext = context;
            SetupWindowParameters(window, @params);
            return new DialogWindowBase<AlertDialog, DialogResult>(window);*/
        }

        private static void ApplyBaseParamsDialogBuilder(DialogWindowBuilderParamsBase @params, DialogBuilder builder) {
            if (@params.DialogButtons == null || @params.DialogButtons.Length == 0)
                @params.DialogButtons = CreateSimpleDialogButtons(DialogButtonsEnum.Ok);
            
            builder
                .RequireNonNullPrivate(@params.ContentHeader, (b, v) => b.SetTitle(v))
                .RequireNonNullPrivate(@params.DialogHeaderIcon, (b, v) => b.SetTitleIcon(v!.Value))
                .RequireNonNullPrivate(@params.DialogIcon, (b, v) => {
                    switch (v)
                    {
                        case Bitmap bitmap:
                            b.SetTitleIcon(bitmap);
                            break;

                        case Image img:
                            b.SetTitleIcon(img);
                            break;

                        case Control control:
                            b.SetTitleIcon(control);
                            break;

                        case DialogIconKind kind:
                            b.SetTitleIcon(kind);
                            break;

                        case null:
                            break;

                        default:
                            throw new ArgumentException($"{v.GetType()} is a unknown or unsupported type.");
                    }
                })
                .RequireNonNullPrivate(@params.SupportingText, (b, v) => b.Text(v))
                .RequireNonNullPrivate(@params.NeutralDialogButtons, (b, v) => {
                    foreach (var button in v) {
                        b.NeutralButton(button.Content, new DialogResult (button.Result));
                    }
                })
                .RequireNonNullPrivate(@params.DialogButtons, (b, v) => {
                    foreach (var button in v) {
                        if (button.IsPositive) {
                            b.PositiveButton(button.Content, new DialogResult (button.Result));
                            continue;
                        }

                        if (button.IsNegative) {
                            b.NegativeButton(button.Content, new DialogResult (button.Result));
                            continue;
                        }

                        b.PositiveButton(button.Content, new DialogResult (button.Result));
                    }
                })
                .RequireNonNullPrivate(@params.Width, (b, v) => {
                    b.Style(new Style(a => a.OfType(typeof(DialogControlView))) {
                        Setters = {
                            new Setter(Layoutable.WidthProperty, v)
                        }
                    });
                })
                .RequireNonNullPrivate(@params.MaxWidth, (b, v) => {
                    b.Style(new Style(a => a.OfType(typeof(DialogControlView))) {
                        Setters = {
                            new Setter(Layoutable.MaxWidthProperty, v)
                        }
                    });
                });
        }

        [Obsolete("Please consider to use DialogBuilder API instead. Those API will be deprecated and get removed.", false)]
        public static IDialogWindow<IDialogResult> CreateTextFieldDialog(TextFieldDialogBuilderParams @params)
        {
            var builder = new DialogBuilder();

            ApplyBaseParamsDialogBuilder(@params, builder);
            
            DialogObject obj = null!;

            void Trigger() {
                foreach (var vm in obj.ViewModel.Answers) {
                    if(vm == null)
                        continue;
                    
                    vm.OnPropertyChanged();
                }
            }

            foreach (var field in TextFieldsBuilder(Trigger, @params.TextFields)) {
                builder.Control(new TextFieldDialogElement {
                    Content = field
                });
            }
            
            obj = builder.Build();

            return obj.GetCompatObject();
            
            /*
            
            
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
            //return new DialogWindowBase<TextFieldDialog, TextFieldDialogResult>(window);*/
        }

        /// <summary>
        /// Create time picker dialog.
        /// </summary>
        /// <param name="params">Parameters of building dialog</param>
        /// <returns>Instance of picker.</returns>
        [Obsolete("DO NOT USE IT ANYMORE", true)]
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
        [Obsolete("DO NOT USE IT ANYMORE", true)]
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
        [Obsolete("Please consider to use DialogBuilder API instead. Those API will be deprecated and get removed.", false)]
        public static IDialogWindow<IDialogResult> CreateCustomDialog(CustomDialogBuilderParams @params)
        {
            var builder = new DialogBuilder();

            ApplyBaseParamsDialogBuilder(@params, builder);

            builder.Control(new ContentControl {
                Content = @params.Content,
                ContentTemplate = @params.ContentTemplate
            });

            return builder.Build().GetCompatObject();
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

        private static TextFieldViewModel[] TextFieldsBuilder(Action fieldChangedTrigger,
            params TextFieldBuilderParams[] @params)
        {
            var len = @params.Length;
            var result = new TextFieldViewModel[len];
            for (var i = 0; i < len; i++)
            {
                var param = @params[i];

                try
                {
                    var model = new TextFieldViewModel(param.DefaultText, s => {
                        fieldChangedTrigger.Invoke();
                        return param.Validater?.Invoke(s) ?? new Tuple<bool, string>(true, s);
                    })
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