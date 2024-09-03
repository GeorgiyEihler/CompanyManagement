using CompanyManagement.Api.Controllers.Users.Requests;
using CompanyManagement.Application.Users.ChangePassword;
using CompanyManagement.Application.Users.Login;
using CompanyManagement.Application.Users.Register;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using ErrorOr;

namespace CompanyManagement.Api.Controllers.Users;

[Authorize]
[Route("api/[controller]")]
public class UserController : ApiController
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginHandler hander,
        [FromServices] IValidator<LoginCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Login, request.Password);

        var validateResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validateResult.IsValid)
        {
            return ValidationProblem(validateResult.Errors);
        }

        var loginResult = await hander.HandleAsync(command, cancellationToken);

        if (loginResult.IsError)
        {
            return Problem(loginResult.Errors);
        }

        return Ok(loginResult.Value);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterHander hander,
        [FromServices] IValidator<RegisterCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(request.Name, request.LastName, request.Patronymic, request.Login, request.Password, request.Email);

        var validateResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validateResult.IsValid)
        {
            return ValidationProblem(validateResult.Errors);
        }

        var registerResult = await hander.HandleAsync(command, cancellationToken);

        if (registerResult.IsError)
        {
            return Problem(registerResult.Errors);
        }

        return Ok(registerResult.Value.UserId);
    }


    [HttpPost("{userId:guid}/change-password")]
    public async Task<IActionResult> ChangePassword(
        [FromRoute] Guid userId,
        [FromBody] ChangePasswordRequest request,
        [FromServices] ChangePasswordHandler handler,
        [FromServices] IValidator<ChangePasswordCommand> validator,
        CancellationToken cancellationToken)
    {
        var claimResult = Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value.ToString() ?? string.Empty, out var id);

        if (!claimResult || id != userId)
        {
            return Problem(Error.Forbidden());
        }

        var changePasswordCommand = new ChangePasswordCommand(userId, request.OldPassword, request.NewPassword);

        var validateResult = await validator.ValidateAsync(changePasswordCommand, cancellationToken);

        if (!validateResult.IsValid)
        {
            return ValidationProblem(validateResult.Errors);
        }
        
        var result = await handler.HandleAsync(changePasswordCommand, cancellationToken);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        await HttpContext.SignOutAsync();

        return Ok(); 
    }
}
