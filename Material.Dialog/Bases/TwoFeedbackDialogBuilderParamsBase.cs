using System;
using Material.Dialog.Interfaces;

namespace Material.Dialog.Bases
{
    public class TwoFeedbackDialogBuilderParamsBase : DialogWindowBuilderParamsBase
    {
        /// <summary>
        /// Define positive action button.
        /// </summary>
        public DialogResultButton PositiveButton = DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum.Ok)[0];

        /// <summary>
        /// Define negative action button.
        /// <br/>You still have a way go back lmao.
        /// </summary>
        public DialogResultButton NegativeButton = DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum.OkCancel)[0];
    }
}