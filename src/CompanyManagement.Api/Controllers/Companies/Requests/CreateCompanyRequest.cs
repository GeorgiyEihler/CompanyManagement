namespace CompanyManagement.Api.Controllers.Companies.Requests;

public record CreateCompanyRequest(string Name, int Number, List<CreateImageRequest> Images);

public record CreateImageRequest(string Name, string Alt, string Url);