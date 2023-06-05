using System;
using Material.Dialog.Bases;

namespace Material.Dialog {
    public class DatePickerDialogBuilderParams : TwoFeedbackDialogBuilderParamsBase {
        /// <summary>
        /// Define implicit date.
        /// </summary>
        public DateTime ImplicitValue = DateTime.Now;
    }

    public class TimePickerDialogBuilderParams : TwoFeedbackDialogBuilderParamsBase {
        /// <summary>
        /// Define implicit time.
        /// </summary>
        public TimeSpan ImplicitValue = TimeSpan.Zero;
    }
}
