using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterManagement.Application.Exceptions;
using ScooterManagement.Domain.Responses;
using System.Net;

namespace ScooterManagement.Controllers;

[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public ErrorResponse HandleError()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationException)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return new ValidationErrorResponse
            {
                ErrorMessage = "One or more fields are not valid.",
                Status = HttpStatusCode.BadRequest,
                Errors = validationException.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage)
            };
        }

        var (errorMessage, status) = exception switch
        {
            ServiceException ex => (ex.ErrorMessage, ex.StatusCode),
            _ => ("Internal server error.", HttpStatusCode.InternalServerError)
        };

        HttpContext.Response.StatusCode = (int)status;

        return new ErrorResponse
        {
            ErrorMessage = errorMessage,
            Status = status
        };
    }
}
