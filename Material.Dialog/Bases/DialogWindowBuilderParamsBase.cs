using Avalonia.Controls;
using Material.Dialog.Icons;
using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Layout;

namespace Material.Dialog.Bases
{
    public class DialogWindowBuilderParamsBase
    {
        public string WindowTitle = "Warning";
        public double? MaxWidth = null;
        public double? Width = null;
        public string ContentHeader = null;
        public string SupportingText = null;
        public bool Borderless = false;
        public WindowStartupLocation StartupLocation = WindowStartupLocation.CenterScreen;
        public DialogIconKind? DialogHeaderIcon = null;
        
        /// <summary>
        /// Build dialog buttons stack. 
        /// <br/>You can use <seealso cref="DialogHelper.CreateSimpleDialogButtons(Enums.DialogButtonsEnum)"/> for create buttons stack in easy way.
        /// </summary>
        public DialogResultButton[] DialogButtons;
        
        /// <summary>
        /// Define result after close the dialog not from buttons
        /// <br/> (could be from Alt + F4 or window close button).
        /// </summary>
        public DialogResult NegativeResult = DialogResult.NoResult;// = new DialogResult(DialogHelper.DIALOG_RESULT_CANCEL);

        /// <summary>
        /// Define buttons stack orientation.
        /// </summary>
        public Orientation ButtonsOrientation = Orientation.Horizontal;
    }
}
