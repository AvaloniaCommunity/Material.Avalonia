using System;
using Material.Dialog.Bases;

namespace Material.Dialog
{
    // TODO Fix DatePicker TimePicker dialogs https://github.com/AvaloniaCommunity/Material.Avalonia/issues/470
    [Obsolete("Currently unsupported - https://github.com/AvaloniaCommunity/Material.Avalonia/issues/470")]
    public class DatePickerDialogBuilderParams : TwoFeedbackDialogBuilderParamsBase
    {
        /// <summary>
        /// Define implicit date.
        /// </summary>
        public DateTime ImplicitValue = DateTime.Now;
    }

    [Obsolete("Currently unsupported - https://github.com/AvaloniaCommunity/Material.Avalonia/issues/470")]
    public class TimePickerDialogBuilderParams : TwoFeedbackDialogBuilderParamsBase
    {
        /// <summary>
        /// Define implicit time.
        /// </summary>
        public TimeSpan ImplicitValue = TimeSpan.Zero;
    }
}