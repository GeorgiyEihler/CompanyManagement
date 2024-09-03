namespace CompanyManagement.Application.Profiles.CreateAdminProfile;

public record CreateAdminProfileCommand(Guid UserId, Guid AdminUserId);