using FleetManager.communication.Resposnes;
using FleetManager.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FleetManager.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is FleetManagerException)
            {
                HandleErrorOnValidationException(context);
            }
            else
            {
                ThrowUnknownError(context);
            }
        }
        private static void HandleErrorOnValidationException(ExceptionContext context) 
        {
            var fleetManagerException = context.Exception as FleetManagerException;
            var errorResponse = new ResponseErrorJson(fleetManagerException!.GetErrors());

            context.HttpContext.Response.StatusCode = fleetManagerException.StatusCode;
            context.Result = new ObjectResult(errorResponse);

        }
        private static void ThrowUnknownError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOW_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
