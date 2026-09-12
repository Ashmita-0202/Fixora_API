using Fix.Categories.Application.AutoMapper;
using Fix.Categories.Application.ServiceInterface;
using Fix.Categories.Infrastructure.ServiceImpl;
using Fix.Shared.Domain.IRepository;
using Fix.Shared.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.Categories.Infrastructure
{
    public static class AssemblyReference
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
