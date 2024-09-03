using CompanyManagement.Domain.Common;
using CompanyManagement.Domain.Owners;
using CompanyManagement.Domain.Shared;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Domain.Users.Events;
using ErrorOr;

namespace CompanyManagement.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
    public FullName FullName { get; private set; }

    public Login Login { get; private set; }

    public Email Email { get; private set; }

    public OwnerId? OwnerId { get; private set; }

    public ParticipantId? ParticipantId { get; private set; }

    public AdministratorId? AdministratorId { get; private set; }

    public bool IsEmailConfirmed { get; private set; }

    private string _passwordHash;

    private User()
    {
    }

    public User(
         UserId userId, 
         DateTime created,
         FullName fullName, 
         Login login,
         Email email,
         string passwordHash,
         OwnerId? ownerId = null,
         ParticipantId? participantId = null,
         AdministratorId? administratorId = null) 
        : base(userId, created)
    {
        FullName = fullName;
        Login = login;
        Email = email;
        _passwordHash = passwordHash;
        OwnerId = ownerId;
        ParticipantId = participantId;
        AdministratorId = administratorId;
    }

    public ErrorOr<Success> ChangePassword(
        string oldPassowrd, 
        string newPassowrd,
        IPasswordHasher passwordHasher)
    {
        if (!CheckPassword(oldPassowrd, passwordHasher))
        {
            return Error.Forbidden(code: "User.OldPasswordIncorrect", description: "The OldPassword is incorrect");
        }

        var hashResult = passwordHasher.HashPassword(newPassowrd);

        if (hashResult.IsError)
        {
            return hashResult.FirstError;
        }

        _passwordHash = hashResult.Value;

        return Result.Success;
    }

    public bool CheckPassword(string password, IPasswordHasher passwordHasher) =>
        passwordHasher.IsCorrectPassword(password, _passwordHash);

    public ErrorOr<AdministratorId> CreateAdminProfile()
    {
        if (AdministratorId is not null)
        {
            return Error.Conflict();
        }

        var id = Guid.NewGuid();

        var administratorId = AdministratorId.Create(id);

        AdministratorId = administratorId;

        RaiseDomainEvent(new AdminCreatedEvent(id));

        return AdministratorId;
    }

    public ErrorOr<ParticipantId> CreatePrticipantProfile()
    {
        if (ParticipantId is not null)
        {
            return Error.Conflict();
        }

        var id = Guid.NewGuid();

        var pariticpatnId = ParticipantId.Create(id);

        ParticipantId = pariticpatnId;

        RaiseDomainEvent(new ParticipantCreatedEvent(id));

        return ParticipantId;
    }

    public ErrorOr<OwnerId> CreateOwnerProfile()
    {
        if (OwnerId is not null)
        {
            return Error.Conflict();
        }

        var id = Guid.NewGuid();

        var ownerId = OwnerId.Create(id);

        OwnerId = ownerId;

        RaiseDomainEvent(new OwnerCreatedEvent(id));

        return OwnerId;
    }
}
