using Sample.Helper.ExceptionLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Sample.WebApi.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ExceptionHandle.PrintException(actionExecutedContext.Exception);
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            actionExecutedContext.Response = resp;
        }
    }
}