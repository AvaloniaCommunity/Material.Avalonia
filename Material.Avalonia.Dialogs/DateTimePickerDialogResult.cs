using System;
using Material.Dialog.Interfaces;

namespace Material.Dialog {
    public class DateTimePickerDialogResult : IDialogResult {
        internal DateTime _dateTime;

        // ReSharper disable once InconsistentNaming
        internal TimeSpan _timeSpan;

        internal string? _result;
        public DateTimePickerDialogResult() { }

        public DateTimePickerDialogResult(string result, TimeSpan time) {
            _result = result;
            _timeSpan = time;
        }

        public DateTimePickerDialogResult(string result, DateTime date) {
            _result = result;
            _dateTime = date;
        }
        public string? GetResult => _result;

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