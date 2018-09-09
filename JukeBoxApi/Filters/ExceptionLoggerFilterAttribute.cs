using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace JukeBoxApi.Filters
{
    public class ExceptionLoggerFilterAttribute: ExceptionFilterAttribute
    {
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            var controllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var message = $"{actionName}/{controllerName} - {exception.Message}";
            log.Error(message, exception);

            base.OnException(actionExecutedContext);
        }
    }
}