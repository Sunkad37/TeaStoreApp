using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeaStoreApp.Infrastructure.Persistence;

namespace TeaStoreApp.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TeaStoreAppDbContext>(options 
            => options.UseSqlServer(configuration.GetConnectionString("TeaStoreAppDb")));
    }
}