using Fix.Shared.Domain.IRepository;
using Fix.Shared.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.Shared.Infrastructure
{
    public static class AssemblyReferences
    {

        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
        {
            // Add shared infrastructure services here

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
