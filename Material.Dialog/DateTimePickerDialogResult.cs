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
        
        public DateTimePickerDialogResult(string result, DateTime date)
        {
            this.Result = result;
            this._dateTime = date;
        }
        
        internal string Result;
        public string GetResult => Result;

        // ReSharper disable once InconsistentNaming
        internal TimeSpan _timeSpan;
        
        /// <summary>
        /// Get results of TimePicker.
        /// </summary>
        public TimeSpan GetTimeSpan() => _timeSpan;
        
        internal DateTime _dateTime;
        
        /// <summary>
        /// Get result of DatePicker.
        /// </summary>
        public DateTime GetDate() => _dateTime;
    }
}