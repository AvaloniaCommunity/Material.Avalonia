using Avalonia.Controls;
using Material.Dialog.Icons;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
