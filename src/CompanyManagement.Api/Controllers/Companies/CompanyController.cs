using CompanyManagement.Api.Controllers.Companies.Requests;
using CompanyManagement.Application.Comanies.CreateCompany;
using CompanyManagement.Application.Comanies.GetCompany;
using CompanyManagement.Application.Comanies.RemoveCompany;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Api.Controllers.Companies;

[Route("api/[controller]")]
public class CompanyController : ApiController
{
    public CompanyController()
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        [FromServices] GetCompanyHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCompanyQuery(id);

        var result = await handler.HandleAsync(query, cancellationToken);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remove(
        Guid id,
        [FromServices] RemoveCompanyHandler hander,
        CancellationToken cancelToken = default)
    {
        var command = new RemoveCompayCommand(id);
        
        var result = await hander.HandleAsync(command, cancelToken);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCompanyRequest request,
        [FromServices] CreateCompanyHandler handler,
        [FromServices] IValidator<CreateCompanyCommand> validator,
        CancellationToken cancelToken)
    {
        var command = new CreateCompanyCommand(
            request.Name, 
            request.Number,
            request.Images?.Select(i => new CreateImageCommand(i.Name, i.Alt, i.Url)).ToList() ?? []);

        var validationResult = await validator.ValidateAsync(command, cancelToken);

        if (!validationResult.IsValid)
        {
            return ValidationProblem(validationResult.Errors);
        }

        var result = await handler.HandleAsync(command, cancelToken);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value },
            result.Value);
    }
}
