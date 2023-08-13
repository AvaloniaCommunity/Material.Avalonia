using Avalonia.Controls.Templates;
using Material.Dialog.Bases;

namespace Material.Dialog
{
    public class CustomDialogBuilderParams : DialogWindowBuilderParamsBase
    {
        public object Content = null;
        public IDataTemplate ContentTemplate = null;
    }
}