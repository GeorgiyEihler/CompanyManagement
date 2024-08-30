namespace CompanyManagement.Application.Comanies.GetCompany;

public record GetCompanyResponse(Guid Id, string Name, int Number, List<EmployeeResponse> Employees, ImageCollectionResponse Images);

public record ImageCollectionResponse(List<ImageResponse> Images);

public record ImageResponse(string Url, string Name);

public record EmployeeResponse(Guid Id, string FirstName, string LastName, string Email, string Status);
