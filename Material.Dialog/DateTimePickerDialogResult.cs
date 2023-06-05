using System;
using Material.Dialog.Interfaces;

namespace Material.Dialog {
    public class DateTimePickerDialogResult : IDialogResult {
        public DateTimePickerDialogResult() {
        }

        public DateTimePickerDialogResult(string result, TimeSpan time) {
            Result = result;
            _timeSpan = time;
        }

        public DateTimePickerDialogResult(string result, DateTime date) {
            Result = result;
            _dateTime = date;
        }

        internal string Result;
        public string GetResult => Result;

        // ReSharper disable once InconsistentNaming
        internal TimeSpan _timeSpan;

        /// <summary>
        /// Get results of TimePicker.
        /// </summary>
        public TimeSpan GetTimeSpan() {
            return _timeSpan;
        }

        internal DateTime _dateTime;

        /// <summary>
        /// Get result of DatePicker.
        /// </summary>
        public DateTime GetDate() {
            return _dateTime;
        }
    }
}
