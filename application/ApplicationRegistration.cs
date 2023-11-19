using codemazepractice.application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace codemazepractice.application;

public static class ApplicationRegistration
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IBlobManager, BlobManager>();
    }
}