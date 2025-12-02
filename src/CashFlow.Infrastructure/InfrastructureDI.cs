using CashFlow.Domain.Repository;
using CashFlow.Infrastructure.Context;
using CashFlow.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;
public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddData(configuration);
        service.AddRepository();

        return service;
    }

    public static void AddData(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionString");
        service.AddDbContext<CashFlowDbContext>(e => e.UseNpgsql(connectionString));
    }

    public static void AddRepository(this IServiceCollection service)
    {
        service.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepositoryAsync<>));
        service.AddScoped(typeof(IWriteOnlyRepository<>), typeof(WriteOnlyRepositoryAsync<>));
        service.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
