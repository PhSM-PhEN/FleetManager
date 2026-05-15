using FleetManager.communication.Resposnes;
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
            if (context.Exception is FleetManagerException)
            {
                HandleErrorOnValidationException(context);
            }
            else if(context.Exception is DomainException)
            {
                HandleDomainRuleException(context);
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
        private static void HandleDomainRuleException(ExceptionContext context)
        {
            var domainException = context.Exception as DomainException;
            var errorResponse = new ResponseErrorJson([domainException!.Message]);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
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
