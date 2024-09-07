namespace Material.Dialog
{
    public class TextFieldDialogResult : DialogResult
    {
        internal TextFieldResult[] _fieldsResult;

        internal string? _result;

        public TextFieldDialogResult()
        {
        }

        public TextFieldDialogResult(string result, TextFieldResult[] fieldsResult)
        {
            this._result = result;
            this._fieldsResult = fieldsResult;
        }

        public override string? GetResult => _result;
        public TextFieldResult[] GetFieldsResult() => _fieldsResult;
    }
}