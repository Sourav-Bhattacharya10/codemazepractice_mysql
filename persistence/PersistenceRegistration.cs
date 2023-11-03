using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using codemazepractice.persistence.Contracts;
using System.Reflection;

namespace codemazepractice.persistence;

public static class PersistenceRegistration
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddDbContext<CodeMazeContext>(options =>
        {
            options.UseMySQL(configuration["MySqlSettings:ConnectionString"]);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}