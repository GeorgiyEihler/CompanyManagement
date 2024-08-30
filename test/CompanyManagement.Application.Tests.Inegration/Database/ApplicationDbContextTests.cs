using CompanyManagement.Application.Tests.Inegration.Infrastructure;
using CompanyManagement.Domain.Shared.Ids;
using CompanyManagement.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Xunit.Abstractions;

namespace CompanyManagement.Application.Tests.Inegration.Database;

public class ApplicationDbContextTests : IntegrationTestBase
{
    public ApplicationDbContextTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }
}
