using CompanyManagement.Api.Controllers.Profiles.Requests;
using CompanyManagement.Application.Profiles.CreateAdminProfile;
using CompanyManagement.Application.Profiles.CreateOwnerProfile;
using CompanyManagement.Application.Profiles.CreateParticipantProfile;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Api.Controllers.Profiles;

[Route("users/{userId:Guid}/profiles")]
public class ProfileController: ApiController
{
    [HttpPost("owner")]
    public async Task<IActionResult> CreateOwnerProfile(
        [FromRoute] Guid userId,
        [FromBody] CreateOwnerRequest request,
        [FromServices] CreateOwnerProfileHandler handler,
        CancellationToken cancellationToken)
    {
        var requestUserId = Guid.Parse(HttpContext.User.Claims.First(claim => claim.Type == "id").Value);

        if (requestUserId != userId)
        {
            return Problem(Error.Forbidden());
        }

        var command = new CreateOwnerProfileCommand(userId);

        var createOwnerResult = await handler.HandleAsync(command, cancellationToken);

        if (createOwnerResult.IsError)
        {
            return Problem(createOwnerResult.Errors);
        }

        return Ok(createOwnerResult.Value);
    }

    [HttpPost("participant")]
    public async Task<IActionResult> CreateParticipantProfile(
       [FromRoute] Guid userId,
       [FromServices] CreateParticipantProfileHandler handler,
       CancellationToken cancellationToken)
    {
        var requestUserId = Guid.Parse(HttpContext.User.Claims.First(claim => claim.Type == "id").Value);

        if (requestUserId != userId)
        {
            return Problem(Error.Forbidden());
        }

        var command = new CreateParticipantProfileCommand(userId);

        var createParticipantResult = await handler.HandleAsync(command, cancellationToken);

        if (createParticipantResult.IsError)
        {
            return Problem(createParticipantResult.Errors);
        }

        return Ok(createParticipantResult.Value);
    }

    [HttpPost("admin")]
    public async Task<IActionResult> CreateAdminProfile(
       [FromBody] CreateAdminRequest request,
       [FromRoute] Guid userId,
       [FromServices] CreateAdminProfileHandler handler,
       CancellationToken cancellationToken)
    {
        var requestUserId = Guid.Parse(HttpContext.User.Claims.First(claim => claim.Type == "id").Value);

        if (requestUserId == Guid.Empty)
        {
            return Problem(Error.Forbidden());
        }

        var command = new CreateAdminProfileCommand(userId, requestUserId);

        var createAdminResult = await handler.HandleAsync(command, cancellationToken);

        if (createAdminResult.IsError)
        {
            return Problem(createAdminResult.Errors);
        }

        return Ok(createAdminResult.Value);
    }
}
