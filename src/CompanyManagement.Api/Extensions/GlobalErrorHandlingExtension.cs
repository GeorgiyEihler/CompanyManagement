using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace CompanyManagement.Api.Extensions;

public static class GlobalErrorHandlingExtension
{
    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");

        app.Map("/error", (HttpContext httpContext) =>
        {
            var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception is null)
            {
                return Results.Problem();
            }

            return Results.Problem();
        });

        return app;
    }
}
