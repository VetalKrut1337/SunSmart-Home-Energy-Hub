using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.Constants;

namespace API.Controllers;

public class BaseController : ControllerBase
{
    protected ActionResult<T> HandleResult<T>(Result<T> result)
    {
        return result.IsSuccess ? result.ValueOrDefault : Error(result);
    }

    protected ActionResult<T> HandleCreatedResult<T>(Result<T> result)
    {
        return result.IsSuccess ? Created(result.ValueOrDefault) : Error(result);
    }

    protected ActionResult HandleResult(Result result)
    {
        return result.IsSuccess ? Ok() : Error(result);
    }

    private ActionResult Error(IResultBase result)
    {
        return result.Errors[0].Message switch
        {
            Errors.NotFound => NotFound(result.Errors.Skip(1)),
            Errors.Forbidden => Forbid(result.Errors.Skip(1)),
            _ => BadRequest(result.Errors)
        };
    }

    private ActionResult<T> Created<T>(T value)
    {
        return CreatedAtAction(null, value);
    }

    private ActionResult BadRequest(IEnumerable<IError> errors)
    {
        return Error(errors, StatusCodes.Status400BadRequest);
    }

    private ActionResult Forbid(IEnumerable<IError> errors)
    {
        return Error(errors, StatusCodes.Status403Forbidden);
    }

    private ActionResult NotFound(IEnumerable<IError> errors)
    {
        return Error(errors, StatusCodes.Status404NotFound);
    }

    private ActionResult Error(IEnumerable<IError> errors, int statusCode)
    {
        ModelStateDictionary pairs = new();
        foreach (var error in errors)
            pairs.AddModelError(Errors.Common, error.Message);
        return ValidationProblem(statusCode: statusCode, modelStateDictionary: pairs);
    }
}