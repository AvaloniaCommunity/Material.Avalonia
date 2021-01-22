using Avalonia.Controls;
using Material.Dialog.Bases;
using Material.Dialog.Enums;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;
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
                        new DialogResultButton { Result = DIALOG_RESULT_OK, Content = "OK" },
                        new DialogResultButton { Result = DIALOG_RESULT_ABORT, Content = "ABORT" }
                    };
                case DialogButtonsEnum.OkCancel:
                    return new DialogResultButton[]
                    {
                        new DialogResultButton { Result = DIALOG_RESULT_OK, Content = "OK" },
                        new DialogResultButton { Result = DIALOG_RESULT_CANCEL, Content = "CANCEL" }
                    };
                case DialogButtonsEnum.YesNo:
                    return new DialogResultButton[]
                    {
                        new DialogResultButton { Result = DIALOG_RESULT_YES, Content = "YES" },
                        new DialogResultButton { Result = DIALOG_RESULT_NO, Content = "NO" }
                    };
                case DialogButtonsEnum.YesNoAbort:
                    return new DialogResultButton[]
                    {
                        new DialogResultButton { Result = DIALOG_RESULT_YES, Content = "YES" },
                        new DialogResultButton { Result = DIALOG_RESULT_NO, Content = "NO" },
                        new DialogResultButton { Result = DIALOG_RESULT_ABORT, Content = "ABORT" }
                    };
                case DialogButtonsEnum.YesNoCancel:
                    return new DialogResultButton[]
                    {
                        new DialogResultButton { Result = DIALOG_RESULT_YES, Content = "YES" },
                        new DialogResultButton { Result = DIALOG_RESULT_NO, Content = "NO" },
                        new DialogResultButton { Result = DIALOG_RESULT_CANCEL, Content = "CANCEL" }
                    };
            }
        }
        
        public static IDialogWindow<DialogResult> CreateAlertDialog(AlertDialogBuilderParams @params)
        { 
            var window = new AlertDialog();
            window.DataContext = new AlertDialogViewModel(window)
            {
                WindowTitle = @params.WindowTitle,
                ContentHeader = @params.ContentHeader,
                ContentMessage = @params.SupportingText,
                Borderless = @params.Borderless,
                WindowStartupLocation = @params.StartupLocation,
                DialogButtons = @params.DialogButtons,
                ButtonsStackOrientation = @params.ButtonsOrientation,
                DialogHeaderIcon = @params.DialogHeaderIcon
            };
            window.SystemDecorations = @params.Borderless ? SystemDecorations.None : SystemDecorations.Full;
            window.SetNegativeResult(@params.NegativeResult);
            return new DialogWindowBase<AlertDialog, DialogResult>(window);
        }
    }
}
