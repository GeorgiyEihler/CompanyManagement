namespace CompanyManagement.Application.Users.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword);