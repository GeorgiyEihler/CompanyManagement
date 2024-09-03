namespace CompanyManagement.Application.Users.Register;

public record RegisterCommand(string Name, string LastName, string Patronymic, string Login, string Password, string Email);