namespace CompanyManagement.Domain.Tests.Unit.Common.Constants;

public static partial class Constant
{
    public static class User
    {
        public static readonly Guid Id = Guid.NewGuid();
        public const string FistName = "John";
        public const string LastName = "Doe";
        public const string Patronymic = "Ivanovich";
        public const string Login = "Exterminatus3000";
        public const string ValidPassword = "123qweQWE!@#";
        public const string Email = "someemail@domen.com";
    }
}
