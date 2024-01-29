using System;
using Material.Dialog.Interfaces;

namespace Material.Dialog {
    public class DateTimePickerDialogResult : IDialogResult {
        internal DateTime _dateTime;

        // ReSharper disable once InconsistentNaming
        internal TimeSpan _timeSpan;

        internal string Result;
        public DateTimePickerDialogResult() { }

        public DateTimePickerDialogResult(string result, TimeSpan time) {
            Result = result;
            _timeSpan = time;
        }

        public DateTimePickerDialogResult(string result, DateTime date) {
            Result = result;
            _dateTime = date;
        }
        public string GetResult => Result;

        /// <summary>
        /// Get results of TimePicker.
        /// </summary>
        public TimeSpan GetTimeSpan() => _timeSpan;

        /// <summary>
        /// Get result of DatePicker.
        /// </summary>
        public DateTime GetDate() => _dateTime;
    }
}