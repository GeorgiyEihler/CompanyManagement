namespace CompanyManagement.Domain.Common;

public interface ISoftDeletable
{
    void Delete();

    void Restor();
}
