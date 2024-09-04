namespace CompanyManagement.Infrastructure.InfrastructureConstants;

public static partial class Constants
{
    public static class Authorization
    {
        public const string OwnerPolicyName = "Owner";
        public const string OwnerCompanyReadClam = Authentication.Permission.GetCompany;
        public const string OwnerCompanyDeleteClam = Authentication.Permission.DeleteCompany;

        public const string AdministratorPolicyName = "Administrator";
    }
}
