namespace CompanyManagement.Application.Comanies.CreateCompany;

public record CreateCompanyCommand(string Name, int Number, List<CreateImageCommand> Images);

public record CreateImageCommand(string Name, string Alt, string Url);