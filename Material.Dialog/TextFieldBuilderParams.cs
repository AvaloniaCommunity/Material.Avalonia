using Material.Dialog.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog
{
    public class TextFieldBuilderParams
    {
        /// <summary>
        /// Constant normal text field.
        /// </summary>
        public static TextFieldBuilderParams[] OneRegularTextField = 
            new TextFieldBuilderParams[] { new TextFieldBuilderParams 
            { 
                Label = "Text field",
                FieldKind = TextFieldKind.Normal,
            } 
        };

        public Func<string, bool> Validater;
        public string PlaceholderText;
        public string DefaultText;

        [Obsolete("Currently AvaloniaUI are not supported to binding classes, do not use this before they solve this issue.")]
        public string Classes;
        public string Label;
        public TextFieldKind FieldKind;
        public char MaskChar = '*';
        public int MaxCountChars;
    }
}
