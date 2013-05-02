using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RnD.KendoUISample.Helpers
{
    public sealed class BirthDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateStart = (DateTime)value;
            // Meeting must start in the future time.
            return (dateStart < DateTime.Now);
        }
    }

    public sealed class LessDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateStart = (DateTime)value;
            // Meeting must start in the future time.
            return (dateStart < DateTime.Now);
        }
    }

    public sealed class GraterDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateStart = (DateTime)value;
            // Meeting must start in the future time.
            return (dateStart > DateTime.Now);
        }
    }

    public sealed class DateStartAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateStart = (DateTime)value;
            // Meeting must start in the future time.
            return (dateStart > DateTime.Now);
        }
    }

    public sealed class DateEndAttribute : ValidationAttribute
    {
        public string DateStartProperty { get; set; }
        public override bool IsValid(object value)
        {
            // Get Value of the DateStart property
            string dateStartString = HttpContext.Current.Request[DateStartProperty];
            DateTime dateEnd = (DateTime)value;
            DateTime dateStart = DateTime.Parse(dateStartString);

            // Meeting start time must be before the end time
            return dateStart <= dateEnd;
        }


    }

    public sealed class TimeStartAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            TimeSpan timeStart = (TimeSpan)value;
            // Meeting must start in the future time.
            return (timeStart.Ticks > DateTime.Now.Ticks);
        }
    }

    public sealed class TimeEndAttribute : ValidationAttribute
    {
        public string TimeStartProperty { get; set; }
        public override bool IsValid(object value)
        {
            // Get Value of the DateStart property
            string dateStartString = HttpContext.Current.Request[TimeStartProperty];
            TimeSpan timeEnd = (TimeSpan)value;
            TimeSpan timeStart = TimeSpan.Parse(dateStartString);

            // Meeting start time must be before the end time
            return timeStart < timeEnd;
        }


    }

}