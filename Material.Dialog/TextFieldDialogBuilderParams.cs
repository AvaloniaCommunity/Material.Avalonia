using Material.Dialog.Bases;

namespace Material.Dialog {
    public class TextFieldDialogBuilderParams : TwoFeedbackDialogBuilderParamsBase {
        /// <summary>
        /// Build text fields stack.
        /// </summary>
        public TextFieldBuilderParams[] TextFields = TextFieldBuilderParams.OneRegularTextField;
        //public DialogResult NegativeResult = new DialogResult(DialogHelper.DIALOG_RESULT_CANCEL);
    }
}
