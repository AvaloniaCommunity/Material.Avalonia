namespace Material.Dialog {
    public class TextFieldDialogResult : DialogResult {
        public TextFieldDialogResult() {
        }

        public TextFieldDialogResult(string result, TextFieldResult[] fieldsResult) {
            this.result = result;
            this.fieldsResult = fieldsResult;
        }

        internal string result;
        public string GetResult => result;

        internal TextFieldResult[] fieldsResult;
        public TextFieldResult[] GetFieldsResult() {
            return fieldsResult;
        }
    }
}
