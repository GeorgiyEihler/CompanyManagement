namespace CompanyManagement.Infrastructure.InfrastructureConstants;

public static partial class Constants
{
    public static class Authentication
    {
        public static class Permission
        {
            public const string GetUser = "users:get";
            public const string CreateAdministator = "administrator:create";
            public const string UpdateUser = "users:update";
            public const string GetCompany = "companies:get";
            public const string UpdateCompany = "companies:update";
            public const string DeleteCompany = "companies:delete";
        }
    }
}

