using FleetManager.Communication.Response;
using FleetManager.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FleetManager.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is FleetManagerException fleetEx)
            {
                HandleException(context, fleetEx.StatusCode, fleetEx.GetErrors());
            }
            
            else
            {
                ThrowUnknownError(context);
            }
        }
        private static void HandleException(ExceptionContext context, int statusCode, List<string> errors)
        {
            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new ObjectResult(new ResponseErrorJson(errors));
        }
        private void ThrowUnknownError(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Erro não tratado ao processar {Method} {Path}",
                context.HttpContext.Request.Method, context.HttpContext.Request.Path);

            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
