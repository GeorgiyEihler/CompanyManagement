using ErrorOr;

namespace CompanyManagement.Domain.Companies;

public record Image
{
    public string Name { get; init; }
    public string Alt { get; init; }
    public string Url { get; init; }

    private Image(string name, string alt, string url) =>
        (Name, Alt, Url) = (name, alt, url);

    public static ErrorOr<Image> Create(
        string name,
        string alt,
        string url)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(code: "Image.Name.Validation", description: "The Name of the Image is invalid");
        }

        if (string.IsNullOrWhiteSpace(alt))
        {
            return Error.Validation(code: "Image.Alt.Validation", description: "The Alt of the Image is invalid");
        }

        if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out _))
        {
            return Error.Validation(code: "Image.Url.Validation", description: "The Url of the Image is invalid");
        }

        return new Image(name, alt, url);
    }
}