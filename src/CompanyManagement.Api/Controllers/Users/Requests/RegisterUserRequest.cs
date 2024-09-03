namespace CompanyManagement.Api.Controllers.Users.Requests;

public record RegisterUserRequest(string Name, string LastName, string Patronymic, string Login, string Password, string Email);