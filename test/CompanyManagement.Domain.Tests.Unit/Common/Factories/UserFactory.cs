using Bogus;
using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Shared;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Domain.Tests.Unit.Common.Constants;
using CompanyManagement.Domain.Users;

namespace CompanyManagement.Domain.Tests.Unit.Common.Factories;

public static class UserFactory
{
    public static User CreateUser(
        DateTime created,
        string passwordHash,
        Guid? id = null,
        string? firstName = null,
        string? lastName = null,
        string? patronimyc = null,
        string? email = null,
        string? login = null,
        Guid? ownerId = null)
    {
        var user = new User(
            UserId.Create(id ?? Constant.User.Id),
            created,
            FullName.Create(firstName ?? Constant.User.FistName, lastName ?? Constant.User.LastName, patronimyc ?? Constant.User.Patronymic).Value,
            Login.Create(login ?? Constant.User.Login).Value,
            Email.Create(email ?? Constant.User.Email).Value,
            passwordHash,
            ownerId is null ? null : OwnerId.Create(ownerId.Value));

        return user;
    }
}
