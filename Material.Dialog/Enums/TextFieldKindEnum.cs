using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog.Enums
{
    public enum TextFieldKind
    {
        /// <summary>
        /// Regular text field.
        /// </summary>
        Normal,
        
        /// <summary>
        /// Regular text field, but with clear button.
        /// </summary>
        WithClear,
        
        /// <summary>
        /// Masked text field, this kind is most used in password field.
        /// </summary>
        Masked,
    }
}
