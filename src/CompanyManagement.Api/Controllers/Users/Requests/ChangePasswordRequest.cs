namespace CompanyManagement.Api.Controllers.Users.Requests;

public record ChangePasswordRequest(string OldPassword, string NewPassword);