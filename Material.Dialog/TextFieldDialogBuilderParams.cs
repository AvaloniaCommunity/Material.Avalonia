using Avalonia.Layout;
using Material.Dialog.Bases;
using System;

namespace Material.Dialog
{
    public class TextFieldDialogBuilderParams : TwoFeedbackDialogBuilderParamsBase
    {
        /// <summary>
        /// Build text fields stack.
        /// </summary>
        public TextFieldBuilderParams[] TextFields = TextFieldBuilderParams.OneRegularTextField;
        //public DialogResult NegativeResult = new DialogResult(DialogHelper.DIALOG_RESULT_CANCEL);
    }
}
