using System;
using Material.Dialog.Interfaces;

namespace Material.Dialog
{
    public class DateTimePickerDialogResult : IDialogResult
    {
        public DateTimePickerDialogResult()
        {
            
        }

        public DateTimePickerDialogResult(string result, DateTime pickerResult)
        {
            this._result = result;
            this._dateTime = pickerResult;
        }
        
        internal string _result;
        public string GetResult => _result;

        // ReSharper disable once InconsistentNaming
        internal DateTime _dateTime;
        public DateTime GetDateTime() => _dateTime;
    }
}