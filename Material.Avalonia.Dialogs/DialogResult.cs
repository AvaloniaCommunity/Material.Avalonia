using Material.Dialog.Interfaces;

namespace Material.Dialog {
    public class DialogResult : IDialogResult {
        private string result;


        public DialogResult() { }

        public DialogResult(string result) {
            this.result = result;
        }

        /// <summary>
        /// Constant none result.
        /// </summary>
        public static DialogResult NoResult { get; private set; } = new() { result = "none" };

        public virtual string GetResult => result;
    }
}