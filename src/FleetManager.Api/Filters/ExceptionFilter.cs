using FleetManager.Communication.Response;
using FleetManager.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FleetManager.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
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
        private static void ThrowUnknownError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
