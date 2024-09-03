using CompanyManagement.Domain.DomainConstants;

namespace CompanyManagement.Domain.Users;

public sealed class Permission
{
    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; }

    public static readonly Permission GetUser = new(Constants.Permission.GetUser);
    public static readonly Permission CreateAdministator = new(Constants.Permission.CreateAdministator);
    public static readonly Permission UpdateUser = new(Constants.Permission.UpdateUser);
    public static readonly Permission GetCompany = new(Constants.Permission.GetCompany);
    public static readonly Permission UpdateCompany = new(Constants.Permission.UpdateCompany);
    public static readonly Permission DeleteCompany = new(Constants.Permission.DeleteCompany);
}
