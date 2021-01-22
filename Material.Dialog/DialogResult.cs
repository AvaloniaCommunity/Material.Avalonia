using Material.Dialog.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog
{ 
    public class DialogResult : IDialogResult
    { 
        /// <summary>
        /// Constant none result.
        /// </summary>
        public static DialogResult NoResult { get; private set; } = new DialogResult() { result = "none" };

        
        public DialogResult()
        {

        }
        public DialogResult(string result)
        {
            this.result = result;
        }


        private string result;
        public string GetResult => result;
    }
}
