using FleetManager.Communication.Responses;
using FleetManager.Domain.DomainExceptionBase;
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
                HandleException(context,fleetEx.StatusCode, fleetEx.GetErrors());
            }
            else if(context.Exception is DomainException domainEx)
            {
                HandleException(context, StatusCodes.Status400BadRequest, [domainEx.Message]);
            }
            else
            {
                ThrowUnknownError(context);
            }
        }
        private void HandleException (ExceptionContext context, int statusCode, List<string> errors)
        {
             context.HttpContext.Response.StatusCode = statusCode;
             context.Result = new ObjectResult(new ResponseErrorJson(errors));
        }
        private static void ThrowUnknownError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOW_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
