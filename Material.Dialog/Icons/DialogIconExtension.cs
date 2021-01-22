using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog.Icons
{
    [MarkupExtensionReturnType(typeof(PackIcon))]
    public class DialogIconExtension : MarkupExtension
    {
        public DialogIconKind Kind { get; set; }  
        public double? Size { get; set; }

        public DialogIconExtension()
        {
        } 
        public DialogIconExtension(DialogIconKind kind, double size = 16)
        {
            Kind = kind;
            Size = size;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var result = new DialogIcon { Kind = Kind };

            if (Size.HasValue)
            {
                result.Height = Size.Value;
                result.Width = Size.Value;
            }

            return result;
        }
    }
}
