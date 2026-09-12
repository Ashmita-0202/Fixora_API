using System.Reflection;
using Fix.Categories.Application.Categories.Commands.AddCategories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.ServiceCategory.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddCategoriesApplication(
          this IServiceCollection services)
        {

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<IValidator<AddCategoriesCommand>, AddCategoriesRequestValidator>();


            return services;
        }
    }
}
