using NLog;
using System.Web.Http.ExceptionHandling;

namespace WebApi.GlobalFilters
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