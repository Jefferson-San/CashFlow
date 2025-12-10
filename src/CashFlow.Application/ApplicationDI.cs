using CashFlow.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);

        return services;
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfig));
    }
}
