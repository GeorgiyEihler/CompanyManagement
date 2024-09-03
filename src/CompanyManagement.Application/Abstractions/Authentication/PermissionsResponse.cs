namespace CompanyManagement.Application.Abstractions.Authentication;

public record PermissionsResponse(HashSet<string> Permissions);