using System;
using Material.Dialog.Enums;

namespace Material.Dialog.Bases {
    [Obsolete("Deprecated builder params.")]
    public class TwoFeedbackDialogBuilderParamsBase : DialogWindowBuilderParamsBase {
        /// <summary>
        /// Define a negative action button.
        /// </summary>
        [Obsolete(
            "Please use DialogButton.IsPositive instead. This API will be deprecated and removed on future updates.")]
        public DialogButton NegativeButton =
            DialogHelper.CreateSimpleDialogButtons(DialogButtonsEnum.OkCancel)[0];
        /// <summary>
        /// Define a positive action button.
        /// </summary>
        [Obsolete(
            "Please use DialogButton.IsPositive instead. This API will be deprecated and removed on future updates.")]
        public DialogButton PositiveButton = DialogHelper.CreateSimpleDialogButtons(DialogButtonsEnum.Ok)[0];
    }
}