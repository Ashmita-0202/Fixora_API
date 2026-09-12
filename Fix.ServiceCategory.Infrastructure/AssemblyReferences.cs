using Fix.Authentication.Application.Automapper;
using Fix.Categories.Application.ServiceInterface;
using Fix.Categories.Infrastructure.ServiceImpl;
using Fix.Shared.Domain.IRepository;
using Fix.Shared.Domain.Models.Entities;
using Fix.Shared.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.ServiceCategory.Infrastructure
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddCategoriesInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoriesServices, CategoriesServices>();

            return services;
        }

    }
}
