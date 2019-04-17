using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace WebApi.Filters
{
    /// <summary>
    /// Provides logging of exceptions.
    /// </summary>
    public class BlogExceptionLogger : ExceptionLogger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public override void Log(ExceptionLoggerContext context)
        {
            logger.Warn(context.ExceptionContext.Exception.ToString());
        }
    }
}