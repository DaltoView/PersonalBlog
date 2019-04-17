using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace WebApi.Filters
{
    /// <summary>
    /// Provides a global exception handler.
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = HttpStatusCode.InternalServerError.ToString()
            };
        }

        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
            HttpResponseMessage response =
                             new HttpResponseMessage(HttpStatusCode.InternalServerError)
                             {
                                 Content = new StringContent(Content),
                                 RequestMessage = Request
                             };
            return Task.FromResult(response);
            }
        }
    }
}