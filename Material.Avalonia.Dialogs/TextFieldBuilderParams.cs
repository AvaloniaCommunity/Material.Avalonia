using System;
using Material.Dialog.Enums;

namespace Material.Dialog {
    public class TextFieldBuilderParams {
        /// <summary>
        /// Constant normal text field.
        /// </summary>
        public static TextFieldBuilderParams[] OneRegularTextField = {
            new() {
                Label = "Text field",
                FieldKind = TextFieldKind.Normal,
            }
        };

        //[Obsolete("Currently AvaloniaUI are not supported to binding classes, do not use this before they fixed this.")]
        public string Classes;
        public string DefaultText = "";

        /// <summary>
        /// Text field kind
        /// </summary>
        public TextFieldKind FieldKind;

        /// <summary>
        /// <p>Helper text conveys additional guidance about the input field, such as how it will be used. It should only take up a single line, being persistently visible or visible only on focus.</p>
        /// Read <a href="https://material.io/components/text-fields#anatomy">Material Design documentations. Text fields anatomy. Assistive elements</a> for more information.
        /// </summary>
        public string HelperText = null;

        /// <summary>
        /// Floating label text
        /// </summary>
        public string Label;

        public char MaskChar = '*';
        public int MaxCountChars;

        public string PlaceholderText = null;

        /// <summary>
        /// Define an data validate function, result using <see cref="Tuple{Boolean,String}"/>
        /// <list>
        /// <br/><seealso cref="Tuple{Boolean,String}.Item1"/> is validation result, should be boolean.
        /// <br/><seealso cref="Tuple{Boolean,String}.Item2"/> is information about invalid field, should be string.
        /// </list>
        /// </summary>
        public Func<string, Tuple<bool, string>> Validater;
    }
}