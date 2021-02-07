using Avalonia.Layout;
using Material.Dialog.Bases;
using System;

namespace Material.Dialog
{
    public class DateTimePickerDialogBuilderParams : DialogWindowBuilderParamsBase
    {
        /// <summary>
        /// Build dialog buttons stack. 
        /// <br/>You can use <seealso cref="DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum)"/> for create buttons stack in easy way.
        /// </summary>
        [Obsolete("Use PositiveButton and NegativeButton instead before we fix this sh**.")]
        public DialogResultButton[] DialogButtons = DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum.OkCancel);

        /// <summary>
        /// Define positive action button. This button will be freezed when any fields are invalid.
        /// </summary>
        public DialogResultButton PositiveButton = DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum.Ok)[0];

        /// <summary>
        /// Define negative action button.
        /// <br/>You still have a way go back lmao.
        /// </summary>
        public DialogResultButton NegativeButton = DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum.OkCancel)[1];

        /// <summary>
        /// Define result after close the dialog not from buttons
        /// <br/> (could be from Alt + F4 or window close button).
        /// </summary>
        public DialogResult NegativeResult = new DialogResult(DialogHelper.DIALOG_RESULT_CANCEL);
    }
}
