using ErrorOr;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CompanyManagement.Api.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected IActionResult Problem(IEnumerable<Error> errors)
    {
        if (!errors.Any())
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors.First());
    }

    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, detail: error.Description);
    }

    protected IActionResult ValidationProblem(IEnumerable<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }

    protected IActionResult ValidationProblem(IEnumerable<ValidationFailure> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.ErrorCode ?? "Validation",
                error.ErrorMessage);
        }

        return ValidationProblem(modelStateDictionary);
    }
}
