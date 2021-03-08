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

        /// <summary>
        /// Define an data validate function, result using <seealso cref="Tuple{Boolean,String}"/>
        /// <list>
        /// <br/><seealso cref="Tuple{Boolean,String}.Item1"/> is validation result, should be boolean.
        /// <br/><seealso cref="Tuple{Boolean,String}.Item2"/> is information about invalid field, should be string.
        /// </list>
        /// </summary>
        public Func<string, Tuple<bool, string>> Validater;
        public string PlaceholderText;
        public string DefaultText;

        [Obsolete("Currently AvaloniaUI are not supported to binding classes, do not use this before they fixed this.")]
        public string Classes;
        public string Label;
        public TextFieldKind FieldKind;
        public char MaskChar = '*';
        public int MaxCountChars;
    }
}
