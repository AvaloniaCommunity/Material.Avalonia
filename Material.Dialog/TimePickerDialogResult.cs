using System;
using Material.Dialog.Interfaces;

namespace Material.Dialog
{
    public class DateTimePickerDialogResult : IDialogResult
    {
        public DateTimePickerDialogResult()
        {
            
        }

        public DateTimePickerDialogResult(string result, TimeSpan time)
        {
            this.Result = result;
            this._timeSpan = time;
        }
        
        internal string Result;
        public string GetResult => Result;

        // ReSharper disable once InconsistentNaming
        internal TimeSpan _timeSpan;
        public TimeSpan GetTimeSpan() => _timeSpan;
    }
}