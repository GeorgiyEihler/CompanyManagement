namespace CompanyManagement.Domain.Companies;

public record ImagesCollection
{
    public IReadOnlyList<Image> Images { get; init; }

    private ImagesCollection()
    { }

    public static ImagesCollection Create(IEnumerable<Image> images) => new() { Images = images.ToList() };
}
