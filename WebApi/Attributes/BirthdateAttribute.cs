using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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