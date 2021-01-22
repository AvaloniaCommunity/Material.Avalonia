using Avalonia.Layout;
using Material.Dialog.Bases; 

namespace Material.Dialog
{
    public class AlertDialogBuilderParams : DialogWindowBuilderParamsBase
    {
        /// <summary>
        /// Build dialog buttons stack. 
        /// <br/>You can use <seealso cref="DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum)"/> for create buttons stack in easy way.
        /// </summary>
        public DialogResultButton[] DialogButtons = DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum.Ok);
        /// <summary>
        /// Define buttons stack orientation.
        /// </summary>
        public Orientation ButtonsOrientation = Orientation.Horizontal;

        /// <summary>
        /// Define result after close the dialog not from buttons
        /// <br/> (could be from Alt + F4 or window close button).
        /// </summary>
        public DialogResult NegativeResult = DialogResult.NoResult;
    }
}
