using Material.Dialog.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog
{ 
    public class TextFieldDialogResult : IDialogResult
    {
        public TextFieldDialogResult()
        {

        }
        public TextFieldDialogResult(string result, TextFieldResult[] fieldsResult)
        {
            this.result = result;
            this.fieldsResult = fieldsResult;
        }

        internal TextFieldResult[] fieldsResult;
        internal string result;
        public string GetResult => result;
        public TextFieldResult[] GetFieldsResult() => fieldsResult;
    }
}
