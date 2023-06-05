using Material.Dialog.Interfaces;

namespace Material.Dialog {
    public class DialogResult : IDialogResult {
        /// <summary>
        /// Constant none result.
        /// </summary>
        public static DialogResult NoResult { get; private set; } = new DialogResult { result = "none" };


        public DialogResult() {
        }

        public DialogResult(string result) {
            this.result = result;
        }


        private string result;
        public virtual string GetResult => result;
    }
}
