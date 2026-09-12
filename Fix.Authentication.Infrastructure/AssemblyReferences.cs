using Fix.Authentication.Application.ServiceInterface;
using Fix.Authentication.Infrastructure.ServiceImpl;
using Microsoft.Extensions.DependencyInjection;
using Fix.Authentication.Application.Automapper;
using Fix.Shared.Domain.IRepository;


namespace Fix.Authentication.Infrastructure
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddAuthenticationInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IServiceCategories, ServiceCategoriesServices>();
            services.AddScoped<IBookingServices, BookingServices>();
            services.AddScoped<ITechnicianServices, TechnicianServices>();
            services.AddScoped<IReviewServices, ReviewServices>();
            services.AddScoped<IAdminServices, AdminServices>();
            services.AddScoped<INotificationServices, NotificationServices>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
