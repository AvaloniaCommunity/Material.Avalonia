using System;

namespace Material.Dialog.Bases {
    [Obsolete("Deprecated builder params.")]
    public class TwoFeedbackDialogBuilderParamsBase : DialogWindowBuilderParamsBase {
        /// <summary>
        /// Define a positive action button.
        /// </summary>
        [Obsolete(
            "Please use DialogButton.IsPositive instead. This API will be deprecated and removed on future updates.")]
        public DialogButton PositiveButton = DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum.Ok)[0];

        /// <summary>
        /// Define a negative action button.
        /// </summary>
        [Obsolete(
            "Please use DialogButton.IsPositive instead. This API will be deprecated and removed on future updates.")]
        public DialogButton NegativeButton =
            DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum.OkCancel)[0];
    }
}
