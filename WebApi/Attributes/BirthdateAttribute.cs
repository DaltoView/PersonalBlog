using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Attributes
{
    public class BirthdateAttribute : RangeAttribute
    {
        public BirthdateAttribute()
            : base(typeof(DateTime),
            DateTime.UtcNow.AddYears(-100).ToShortDateString(),
            DateTime.UtcNow.AddYears(-6).ToShortDateString())
        {
        }
    }
}